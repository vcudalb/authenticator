using System.Diagnostics.CodeAnalysis;
using Authenticator.Infrastructure.Persistence.Contexts;
using Authenticator.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Authenticator.Domain.Entities;
using Authenticator.UnitTests.Utilities.Stubs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Authenticator.UnitTests.Infrastructure.Persistence.Repositories;

[ExcludeFromCodeCoverage]
public class GenericRepositoryTests
{
    private static readonly DbContextOptions<AuthenticatorDbContext>? _contextOptions =
        new DbContextOptionsBuilder<AuthenticatorDbContext>().UseInMemoryDatabase(databaseName: "InMemoryDatabase").Options;

    private readonly IDbContextFactory<AuthenticatorDbContext> _contextFactory =
        new PooledDbContextFactory<AuthenticatorDbContext>(_contextOptions ?? throw new InvalidOperationException());

    [Fact]
    public async Task GetAsync_WithFilter_ReturnsAllEntities()
    {
        // Arrange
        await using var dbContext = new AuthenticatorDbContext(_contextOptions);
        await EnsureCleanContextAsync(dbContext);

        var repository = new GenericRepository<Address>(_contextFactory);
        var stubAddresses = StubProvider.GetStubAddresses();

        dbContext.AddRange(stubAddresses);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetAsync();

        // Assert
        var enumerable = result as Address[] ?? result.ToArray();
        enumerable.Should().NotBeNull();
        enumerable.Should().HaveCount(2);
        enumerable[0].City.Should().BeEquivalentTo(stubAddresses[0].City);
        enumerable[1].City.Should().BeEquivalentTo(stubAddresses[1].City);
    }

    [Fact]
    public async Task GetAsync_WithFilter_ReturnsFilteredEntities()
    {
        // Arrange
        await using var dbContext = new AuthenticatorDbContext(_contextOptions);
        await EnsureCleanContextAsync(dbContext);

        var repository = new GenericRepository<Address>(_contextFactory);
        Expression<Func<Address, bool>> filter = x => x.IsActive;

        var stubAddresses = StubProvider.GetStubAddresses();
        dbContext.AddRange(stubAddresses);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetAsync(filter);

        // Assert
        var enumerable = result as Address[] ?? result.ToArray();
        enumerable.Should().NotBeNull();
        enumerable.Should().HaveCount(1);
        enumerable[0].IsActive.Should().Be(stubAddresses[0].IsActive);
        enumerable[0].City.Should().BeEquivalentTo(stubAddresses[0].City);
    }

    [Fact]
    public async Task InsertAsync_AddsEntityToDbSet()
    {
        // Arrange
        await using var dbContext = new AuthenticatorDbContext(_contextOptions);
        await EnsureCleanContextAsync(dbContext);

        var repository = new GenericRepository<Address>(_contextFactory);
        var stubEntity = StubProvider.GetStubAddress();

        // Act
        await repository.InsertAsync(stubEntity);
        await dbContext.SaveChangesAsync();

        // Assert
        dbContext.Addresses.Should().HaveCount(1);
    }

    [Fact]
    public async Task DeleteAsync_DeletesEntityFromDbSet()
    {
        // Arrange
        await using var dbContext = new AuthenticatorDbContext(_contextOptions);
        await EnsureCleanContextAsync(dbContext);

        var repository = new GenericRepository<Address>(_contextFactory);
        var stubEntity = StubProvider.GetStubAddress();

        dbContext.AddRange(stubEntity);
        await dbContext.SaveChangesAsync();

        // Act
        await repository.DeleteAsync(a => a.City == stubEntity.City);
        await dbContext.SaveChangesAsync();

        // Assert
        dbContext.Addresses.Should().HaveCount(0);
    }

    private static async Task EnsureCleanContextAsync(AuthenticatorDbContext dbContext)
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
    }
}