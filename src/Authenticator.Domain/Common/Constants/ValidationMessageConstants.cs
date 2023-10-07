using System.Diagnostics.CodeAnalysis;

namespace Authenticator.Domain.Common.Constants;

[ExcludeFromCodeCoverage]
public class ValidationMessageConstants
{
        public const string NotEmptyValidationMessage = "{0} cannot be empty.";
        public const string GreaterThanValidationMessage = "{0} should be greater than {1}.";
        public const string LessThanValidationMessage = "{0} should be less than {1}.";
        public const string BetweenValidationMessage = "{0} should be between {1} and {2}.";
        public const string MinLenпthValidationMessage = "The length of {0} must be at least {1} characters.";
        public const string MaxLengthValidationMessage = "The length of {0} must be {1} characters or fewer.";
        public const string MatchesValidationMessage = "{0} must contain at least one {1}.";
        public const string NotExistsValidationMessage = "{0} doesn't exists. Please provide a valid one.";
        public const string InvalidModelValidationMessage = "Invalid {0}.";
}