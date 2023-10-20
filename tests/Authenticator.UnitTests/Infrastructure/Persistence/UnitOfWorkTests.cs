using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Authenticator.Domain.Entities;
using Authenticator.Domain.Repositories.Abstractions;
using Authenticator.Infrastructure.Persistence;
using FluentAssertions;
using NSubstitute;

namespace Authenticator.UnitTests.Infrastructure.Persistence;

[ExcludeFromCodeCoverage]
public class UnitOfWorkTests
{
    [Fact]
    public void UserRepository_ShouldBeInitialized()
    {
        // Arrange
        var userRepository = Substitute.For<IGenericRepository<User>>();
        var addressRepository = Substitute.For<IGenericRepository<Address>>();
        var countryRepository = Substitute.For<IGenericRepository<Country>>();

        // Act
        var unitOfWork = new UnitOfWork(userRepository, addressRepository, countryRepository);

        // Assert
        unitOfWork.UserRepository.Should().NotBeNull();
    }

    [Fact]
    public void AddressRepository_ShouldBeInitialized()
    {
        // Arrange
        var userRepository = Substitute.For<IGenericRepository<User>>();
        var addressRepository = Substitute.For<IGenericRepository<Address>>();
        var countryRepository = Substitute.For<IGenericRepository<Country>>();

        // Act
        var unitOfWork = new UnitOfWork(userRepository, addressRepository, countryRepository);

        // Assert
        unitOfWork.AddressRepository.Should().NotBeNull();
    }

    [Fact]
    public void CountryRepository_ShouldBeInitialized()
    {
        // Arrange
        var userRepository = Substitute.For<IGenericRepository<User>>();
        var addressRepository = Substitute.For<IGenericRepository<Address>>();
        var countryRepository = Substitute.For<IGenericRepository<Country>>();

        // Act
        var unitOfWork = new UnitOfWork(userRepository, addressRepository, countryRepository);

        // Assert
        unitOfWork.CountryRepository.Should().NotBeNull();
    }

    [Fact]
    public async Task UserRepository_Methods_ShouldBeCalled()
    {
        // Arrange
        var userRepository = Substitute.For<IGenericRepository<User>>();
        var addressRepository = Substitute.For<IGenericRepository<Address>>();
        var countryRepository = Substitute.For<IGenericRepository<Country>>();
        var unitOfWork = new UnitOfWork(userRepository, addressRepository, countryRepository);

        // Act
        await unitOfWork.UserRepository.GetAsync();

        // Assert
        await userRepository.Received(1).GetAsync();
    }

    [Fact]
    public async Task AddressRepository_Methods_ShouldBeCalled()
    {
        // Arrange
        var userRepository = Substitute.For<IGenericRepository<User>>();
        var addressRepository = Substitute.For<IGenericRepository<Address>>();
        var countryRepository = Substitute.For<IGenericRepository<Country>>();
        var unitOfWork = new UnitOfWork(userRepository, addressRepository, countryRepository);

        // Act
        await unitOfWork.AddressRepository.InsertAsync(new Address());

        // Assert
        await addressRepository.Received(1).InsertAsync(Arg.Any<Address>());
    }

    [Fact]
    public async Task CountryRepository_Methods_ShouldBeCalled()
    {
        // Arrange
        var userRepository = Substitute.For<IGenericRepository<User>>();
        var addressRepository = Substitute.For<IGenericRepository<Address>>();
        var countryRepository = Substitute.For<IGenericRepository<Country>>();
        var unitOfWork = new UnitOfWork(userRepository, addressRepository, countryRepository);

        // Act
        await unitOfWork.CountryRepository.DeleteAsync(x => x.Id != Guid.Empty);

        // Assert
        await countryRepository.Received(1).DeleteAsync(Arg.Any<Expression<Func<Country, bool>>>());
    }
}