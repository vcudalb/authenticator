using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Entities;

namespace Authenticator.UnitTests.Utilities.Stubs;

[ExcludeFromCodeCoverage]
public static class StubProvider
{
    public static List<Address> GetStubAddresses() => new List<Address>
    {
        new Address { IsActive = true, City = "Chisinau" },
        new Address { IsActive = false, City = "Iasi" },
    };

    public static Address GetStubAddress(bool isActive = true, string city = "Chisinau") =>
        new Address { City = city, IsActive = isActive };
}