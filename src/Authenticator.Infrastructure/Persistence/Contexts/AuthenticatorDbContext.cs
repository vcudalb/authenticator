using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Entities;
using Authenticator.Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Authenticator.Infrastructure.Persistence.Contexts;

/// <summary>
///     Provides authenticator db context configurations and db sets.
/// </summary>
[ExcludeFromCodeCoverage]
public class AuthenticatorDbContext : IdentityDbContext<User, IdentityRole, string>
{
    /// <summary>
    ///     Constructs a new instance of <see cref="AuthenticatorDbContext" />.
    /// </summary>
    /// <param name="options">
    ///     An <see cref="DbContextOptions{AuthenticatorDbContext}"/> representing the db context options.
    /// </param>
    public AuthenticatorDbContext(DbContextOptions<AuthenticatorDbContext> options) : base(options)
    {
    }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Country> Countries { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CountryConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AddressConfiguration).Assembly);
    }
}