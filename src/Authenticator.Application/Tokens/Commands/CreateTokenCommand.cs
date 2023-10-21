using Authenticator.Domain.Requests.Authenticators.Tokens;
using Authenticator.Domain.Responses.Authenticators.Tokens;
using MediatR;

namespace Authenticator.Application.Tokens.Commands;

public record CreateTokenCommand(TokenRequest Request) : IRequest<TokenResponse>
{
    
}