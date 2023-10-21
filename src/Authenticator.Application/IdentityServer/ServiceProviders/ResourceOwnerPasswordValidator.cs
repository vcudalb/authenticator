using Authenticator.Domain.Entities;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Identity;

namespace Authenticator.Application.IdentityServer.ServiceProviders;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public ResourceOwnerPasswordValidator(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _userManager.FindByNameAsync(context.UserName);

        if (user is not null)
        {
            var signinResult = await _signInManager.CheckPasswordSignInAsync(user, context.Password, false);
            if (signinResult.Succeeded)
            {
                context.Result =
                    new GrantValidationResult(user.Id, IdentityModel.OidcConstants.AuthenticationMethods.Password);
                return;
            }
        }

        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The username and password do not match");
    }
}