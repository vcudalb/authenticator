using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Common;

namespace Authenticator.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Address : BaseAuditableEntity
{
    public string City { get; set; }
    public string Street { get; set; }
    
    public long CountryId { get; set; }
    public virtual Country Country { get; set; }
    public ICollection<User> Users { get; set; }
}