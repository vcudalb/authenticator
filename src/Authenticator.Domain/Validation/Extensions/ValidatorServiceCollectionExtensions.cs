using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Requests.Authenticators.Tokens;
using Authenticator.Domain.Validation.Abstractions;
using Authenticator.Domain.Validation.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Authenticator.Domain.Validation.Extensions;

[ExcludeFromCodeCoverage]
public static class ValidatorServiceCollectionExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddTransient(typeof(IValidationFactory), typeof(ValidationFactory));
        services.AddTransient<IValidator<CreateTokenRequest>, CreateTokenValidator>();

        return services;
    }
}