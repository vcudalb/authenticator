using Authenticator.Domain.Validation.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Authenticator.Domain.Validation;

public class ValidationFactory : IValidationFactory
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ValidationFactory(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public TValidator GetValidator<TValidator>() where TValidator : IValidator =>
        _serviceScopeFactory.CreateScope().ServiceProvider.GetService<TValidator>();
}