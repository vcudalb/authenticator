using System.Diagnostics.CodeAnalysis;

namespace Authenticator.Domain.Responses.Authenticators.Tokens;

[ExcludeFromCodeCoverage]
public class CreateTokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public int? ExpiresIn { get; set; }
}