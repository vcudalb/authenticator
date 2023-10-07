using Authenticator.Application.DTOs.Tokens;
using MediatR;

namespace Authenticator.Application.Tokens.Commands;

public record CreateTokenCommand(TokenDto Token) : IRequest<Guid>
{
    
}