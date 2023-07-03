namespace Authenticator.Domain.Common;

/// <summary>
///     Represents the base auditable entity that provides default properties for all history purpose.
/// </summary>
public class BaseAuditableEntity : BaseEntity
{
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
    public bool IsActive { get; set; }
}