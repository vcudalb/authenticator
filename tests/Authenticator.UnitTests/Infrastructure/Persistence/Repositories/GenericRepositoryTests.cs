using Authenticator.Infrastructure.Persistence.Contexts;
using Authenticator.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Authenticator.Domain.Entities;
using Authenticator.UnitTests.Utilities.Stubs;
using FluentAssertions;

namespace Authenticator.UnitTests.Infrastructure.Persistence.Repositories;

public class GenericRepositoryTests
{
    private readonly DbContextOptions<AuthenticatorDbContext>? _contextOptions =
        new DbContextOptionsBuilder<AuthenticatorDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

    [Fact]
    public async Task GetAsync_WithFilter_ReturnsAllEntities()
    {
        // Arrange

        await using var dbContext = new AuthenticatorDbContext(_contextOptions);
        await EnsureCleanContextAsync(dbContext);

        var repository = new GenericRepository<Address>(dbContext);
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

        var repository = new GenericRepository<Address>(dbContext);
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

        var repository = new GenericRepository<Address>(dbContext);
        var stubEntity = StubProvider.GetStubAddress();

        // Act
        await repository.InsertAsync(stubEntity);
        await dbContext.SaveChangesAsync();

        // Assert
        dbContext.Addresses.Should().HaveCount(1);
    }

    [Fact]
    public async Task InsertAsync_NoSaveChanges_InsertSkipped()
    {
        // Arrange
        await using var dbContext = new AuthenticatorDbContext(_contextOptions);
        await EnsureCleanContextAsync(dbContext);

        var repository = new GenericRepository<Address>(dbContext);
        var stubEntity = StubProvider.GetStubAddress();

        // Act
        await repository.InsertAsync(stubEntity);

        // Assert
        dbContext.Addresses.Should().HaveCount(0);
    }

    [Fact]
    public async Task DeleteAsync_DeletesEntityFromDbSet()
    {
        // Arrange
        await using var dbContext = new AuthenticatorDbContext(_contextOptions);
        await EnsureCleanContextAsync(dbContext);

        var repository = new GenericRepository<Address>(dbContext);
        var stubEntity = StubProvider.GetStubAddress();

        dbContext.AddRange(stubEntity);
        await dbContext.SaveChangesAsync();

        // Act
        await repository.DeleteAsync(a => a.City == stubEntity.City);
        await dbContext.SaveChangesAsync();

        // Assert
        dbContext.Addresses.Should().HaveCount(0);
    }
    
    [Fact]
    public async Task DeleteAsync_NoSaveChanges_SkipRemovalOfTheEntity()
    {
        // Arrange
        await using var dbContext = new AuthenticatorDbContext(_contextOptions);
        await EnsureCleanContextAsync(dbContext);

        var repository = new GenericRepository<Address>(dbContext);
        var stubEntity = StubProvider.GetStubAddress();

        dbContext.AddRange(stubEntity);
        await dbContext.SaveChangesAsync();
        
        // Act
        await repository.DeleteAsync(a => a.City == stubEntity.City);

        // Assert
        dbContext.Addresses.Should().HaveCount(1);
    }

    private async Task EnsureCleanContextAsync(DbContext dbContext)
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
    }
}