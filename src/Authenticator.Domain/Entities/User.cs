using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Common.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace Authenticator.Domain.Entities;

[ExcludeFromCodeCoverage]
public class User : IdentityUser
{
    public string Code { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Ip { get; set; }
    public DateTimeOffset? LastLogon { get; set; }

    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
    public bool IsActive { get; set; }

    public long AddressId { get; set; }
    public virtual Address Address { get; set; }
}