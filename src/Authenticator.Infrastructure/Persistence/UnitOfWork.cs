using Authenticator.Domain.Common;
using Authenticator.Domain.Entities;
using Authenticator.Domain.Repositories.Abstractions;

namespace Authenticator.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    public IGenericRepository<User> UserRepository { get; }
    public IGenericRepository<Address> AddressRepository { get; }
    public IGenericRepository<Country> CountryRepository { get; }

    public UnitOfWork(
        IGenericRepository<User> userRepository,
        IGenericRepository<Address> addressRepository,
        IGenericRepository<Country> countryRepository)
    {
        UserRepository = userRepository;
        AddressRepository = addressRepository;
        CountryRepository = countryRepository;
    }
}