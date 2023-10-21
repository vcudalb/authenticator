using Authenticator.Domain.Entities;
using Authenticator.Domain.Requests.Authenticators.Tokens;
using Authenticator.Domain.Responses.Authenticators.Tokens;
using Authenticator.Domain.Validation.Abstractions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authenticator.Application.Tokens.Commands;

public class CreateEnlistCommandHandler : IRequestHandler<CreateEnlistCommand, EnlistResponse>
{
    private readonly IValidationFactory _validationFactory;
    private readonly UserManager<User> _userManager;

    public CreateEnlistCommandHandler(UserManager<User> userManager, IValidationFactory validationFactory)
    {
        _userManager = userManager;
        _validationFactory = validationFactory;
    }

    public async Task<EnlistResponse> Handle(CreateEnlistCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validationFactory.GetValidator<IValidator<EnlistRequest>>().ValidateAsync(command.Request, cancellationToken);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var result = await _userManager.CreateAsync(new User(), command.Request.Password);

        if (!result.Succeeded) throw new ValidationException(result.Errors.Select(e => new ValidationFailure("", e.Description) { ErrorCode = e.Code }));

        return new EnlistResponse();
    }
}