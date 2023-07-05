using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authenticator.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the entity of type <see cref="User"/>
/// </summary>
[ExcludeFromCodeCoverage]
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(200);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(200);
        builder.Property(u => u.UserName).IsRequired().HasMaxLength(200);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
        builder.Property(u => u.IsActive).IsRequired();
        builder.Property(u => u.Gender).IsRequired();
        builder.HasIndex(u => new { u.UserName }).IsUnique();
        builder.HasIndex(u => new { u.Email }).IsUnique();
        builder.HasIndex(u => new { u.PhoneNumber }).IsUnique();
        builder.HasIndex(u => new { u.IsActive }).IsUnique();
    }
}