using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using Authenticator.Application.DTOs.Tokens;
using Authenticator.Application.IdentityServer.ServiceProviders.Abstractions;
using Duende.IdentityServer.Validation;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Authenticator.Domain.Responses.Authenticators.Tokens;

namespace Authenticator.Application.IdentityServer.ServiceProviders;

public class TokenService : ITokenService
{
    private readonly ITokenRequestValidator _tokenRequestValidator;
    private readonly IClientSecretValidator _clientSecretValidator;
    private readonly Duende.IdentityServer.ResponseHandling.ITokenResponseGenerator _tokenResponseGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(
        ITokenRequestValidator tokenRequestValidator,
        IClientSecretValidator clientSecretValidator,
        Duende.IdentityServer.ResponseHandling.ITokenResponseGenerator tokenResponseGenerator,
        IHttpContextAccessor httpContextAccessor)
    {
        _tokenRequestValidator = tokenRequestValidator;
        _clientSecretValidator = clientSecretValidator;
        _tokenResponseGenerator = tokenResponseGenerator;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TokenResponse> GetTokenAsync(TokenDto dto)
    {
        var parameters = GetNamedValueRequestParameters(dto);
        return await GetTokenResponseAsync(parameters);
    }

    private async Task<TokenResponse> GetTokenResponseAsync(NameValueCollection requestParameters)
    {
        var clientResult = await _clientSecretValidator.ValidateAsync(_httpContextAccessor.HttpContext);
        if (clientResult.IsError) throw new ValidationException("Invalid client/secret combination");

        var validationResult = await _tokenRequestValidator.ValidateRequestAsync(new TokenRequestValidationContext
        {
            RequestParameters = requestParameters,
            ClientValidationResult = clientResult
        });
        if (validationResult.IsError) throw new ValidationException(validationResult.Error);

        var response = await _tokenResponseGenerator.ProcessAsync(validationResult);
        return GetTokenResponse(response);
    }

    private static NameValueCollection GetNamedValueRequestParameters(TokenDto dto) => new()
    {
        { "username", dto.UserName },
        { "password", dto.Password },
        { "grant_type", dto.Grant_Type },
        { "scope", dto.Scope },
        { "refresh_token", dto.Refresh_Token },
        { "response_type", OidcConstants.ResponseTypes.Token }
    };

    private static TokenResponse GetTokenResponse(Duende.IdentityServer.ResponseHandling.TokenResponse idpTokenResponse) => new()
    {
        AccessToken = idpTokenResponse.AccessToken,
        RefreshToken = idpTokenResponse.RefreshToken,
        ExpiresIn = idpTokenResponse.AccessTokenLifetime
    };
}