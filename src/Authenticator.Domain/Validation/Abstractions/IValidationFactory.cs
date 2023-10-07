using FluentValidation;

namespace Authenticator.Domain.Validation.Abstractions;

public interface IValidationFactory
{
    TValidator GetValidator<TValidator>() where TValidator : IValidator;
}