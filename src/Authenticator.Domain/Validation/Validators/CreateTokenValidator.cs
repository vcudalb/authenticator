using Authenticator.Domain.Requests.Authenticators.Tokens;
using FluentValidation;
using static Authenticator.Domain.Common.Constants.ValidationMessageConstants;

namespace Authenticator.Domain.Validation.Validators;

public class CreateTokenValidator : AbstractValidator<CreateTokenRequest>
{
    private const string RefreshGrantType = "refresh_token";

    public CreateTokenValidator()
    {
        RuleFor(t => t)
            .NotNull()
            .WithMessage(string.Format(NotEmptyValidationMessage, nameof(CreateTokenRequest)));

        RuleFor(t => t.UserName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(string.Format(NotEmptyValidationMessage, nameof(CreateTokenRequest.UserName)))
            .MinimumLength(3)
            .WithMessage(string.Format(MinLenпthValidationMessage, nameof(CreateTokenRequest.UserName), 3))
            .MaximumLength(200)
            .WithMessage(string.Format(MaxLengthValidationMessage, nameof(CreateTokenRequest.UserName), 200))
            .When(t => string.IsNullOrEmpty(t.Refresh_Token));

        RuleFor(t => t.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(string.Format(NotEmptyValidationMessage, nameof(CreateTokenRequest.Password)))
            .MinimumLength(8)
            .WithMessage(string.Format(MinLenпthValidationMessage, nameof(CreateTokenRequest.Password), 8))
            .MaximumLength(100)
            .WithMessage(string.Format(MaxLengthValidationMessage, nameof(CreateTokenRequest.Password), 100))
            .Matches(@"[A-Z]+")
            .WithMessage(string.Format(MatchesValidationMessage, nameof(CreateTokenRequest.Password),
                "uppercase letter"))
            .Matches(@"[a-z]+")
            .WithMessage(string.Format(MatchesValidationMessage, nameof(CreateTokenRequest.Password),
                "lowercase letter"))
            .Matches(@"[0-9]+")
            .WithMessage(string.Format(MatchesValidationMessage, nameof(CreateTokenRequest.Password), "number"))
            .Matches(@"[\!\?\*\.\@\#\$\%\^\&\(\)]+")
            .WithMessage(string.Format(MatchesValidationMessage, nameof(CreateTokenRequest.Password),
                "(!? *.@#$%^&*())"))
            .When(t => string.IsNullOrEmpty(t.Refresh_Token));

        RuleFor(t => t.Client_Secret)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(string.Format(NotEmptyValidationMessage, nameof(CreateTokenRequest.Client_Secret)));

        RuleFor(t => t.Client_Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(string.Format(NotEmptyValidationMessage, nameof(CreateTokenRequest.Client_Id)));

        RuleFor(t => t.Scope)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(string.Format(NotEmptyValidationMessage, nameof(CreateTokenRequest)));

        RuleFor(t => t.Refresh_Token)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(string.Format(NotEmptyValidationMessage, nameof(CreateTokenRequest.Refresh_Token)))
            .When(t => IsRefreshGrantType(t.Grant_Type));
    }

    private static bool IsRefreshGrantType(string grantType) =>
        string.Equals(grantType, RefreshGrantType, StringComparison.InvariantCultureIgnoreCase);
}