using Authenticator.Application.DTOs.Tokens;
using Authenticator.Application.IdentityServer.ServiceProviders.Abstractions;
using Authenticator.Domain.Requests.Authenticators.Tokens;
using Authenticator.Domain.Responses.Authenticators.Tokens;
using Authenticator.Domain.Validation.Abstractions;
using FluentValidation;
using MediatR;

namespace Authenticator.Application.Tokens.Commands;

public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, TokenResponse>
{
    private readonly ITokenService _tokenService;
    private readonly IValidationFactory _validationFactory;
    public CreateTokenCommandHandler(ITokenService tokenService, IValidationFactory validationFactory)
    {
        _tokenService = tokenService;
        _validationFactory = validationFactory;
    }

    public async Task<TokenResponse> Handle(CreateTokenCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validationFactory.GetValidator<IValidator<TokenRequest>>().ValidateAsync(command.Request, cancellationToken);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        return await _tokenService.GetTokenAsync(Map(command.Request));
    }

    private TokenDto Map(TokenRequest request) => new()
    {
        Password = request.Password,
        Scope = request.Scope,
        Client_Id = request.Client_Id,
        Client_Secret = request.Client_Secret,
        Grant_Type = request.Grant_Type,
        Refresh_Token = request.Refresh_Token,
        UserName = request.UserName
    };
}