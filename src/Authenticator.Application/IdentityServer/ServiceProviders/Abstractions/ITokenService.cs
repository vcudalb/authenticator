using Authenticator.Application.DTOs.Tokens;
using Authenticator.Domain.Responses.Authenticators.Tokens;

namespace Authenticator.Application.IdentityServer.ServiceProviders.Abstractions;

public interface ITokenService
{
    Task<TokenResponse> GetTokenAsync(TokenDto dto);
}