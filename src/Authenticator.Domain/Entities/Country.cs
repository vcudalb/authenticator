using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Common;

namespace Authenticator.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Country : BaseAuditableEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string PhoneCode { get; set; }
    
    public ICollection<Address> Addresses { get; set; }
}