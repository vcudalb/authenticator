using Authenticator.Domain.Common.Enumerations;

namespace Authenticator.Domain.Requests.Authenticators.Tokens;

public class EnlistRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Gsm { get; set; }
}