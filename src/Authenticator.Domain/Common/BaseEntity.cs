namespace Authenticator.Domain.Common;

/// <summary>
///     Represents the base entity that provides default properties for all entities.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
}