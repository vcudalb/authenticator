using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authenticator.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the entity of type <see cref="Country"/>
/// </summary>
[ExcludeFromCodeCoverage]
public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Code).HasMaxLength(100);
        builder.Property(c => c.PhoneCode).HasMaxLength(50);
    }
}