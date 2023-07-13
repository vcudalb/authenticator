using Authenticator.Domain.Entities;
using Authenticator.Domain.Repositories.Abstractions;

namespace Authenticator.Domain.Common;

public interface IUnitOfWork
{
    IGenericRepository<User> UserRepository { get; }
    IGenericRepository<Address> AddressRepository { get; }
    IGenericRepository<Country> CountryRepository { get; }
}