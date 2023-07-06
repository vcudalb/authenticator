using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authenticator.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the entity of type <see cref="Address"/>
/// </summary>
[ExcludeFromCodeCoverage]
public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.City).HasMaxLength(200);
        builder.Property(a => a.Street).HasMaxLength(200);
    }
}