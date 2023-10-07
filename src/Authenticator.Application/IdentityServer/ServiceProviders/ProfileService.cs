using System.Globalization;
using System.Security.Claims;
using Authenticator.Domain.Entities;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authenticator.Application.IdentityServer.ServiceProviders;

public class ProfileService : IProfileService
{
    private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;
    private readonly UserManager<User> _userManager;

    public ProfileService(UserManager<User> userManager, IUserClaimsPrincipalFactory<User> claimsFactory)
    {
        _userManager = userManager;
        _claimsFactory = claimsFactory;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.Users
            .Include(u => u.Address)
            .ThenInclude(c => c.Country)
            .FirstOrDefaultAsync(s => s.Id == sub);

        context.IssuedClaims = GetClaims(user);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        context.IsActive = user is not null;
    }

    private List<Claim> GetClaims(User user) => new()
    {
        new(JwtClaimTypes.Subject, user.Id ?? string.Empty),
        new(JwtClaimTypes.Id, user.Id ?? string.Empty),
        new("firstName", user.FirstName),
        new("lastName", user.LastName),
        new("userName", user.UserName ?? string.Empty),
        new(JwtClaimTypes.Email, user.Email ?? string.Empty),
        new("gsm", user.PhoneNumber ?? string.Empty),
        new("dateOfBirth", user.DateOfBirth.ToString(CultureInfo.InvariantCulture) ?? string.Empty),
        new("country", user.Address.Country.Name),
        new("city", user.Address.City),
        new(JwtClaimTypes.Gender, user.Gender.ToString() ?? string.Empty)
    };
}