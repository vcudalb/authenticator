using Authenticator.Domain.Common.Enumerations;

namespace Authenticator.Domain.Responses.Authenticators.Tokens;

public class EnlistResponse
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Gsm { get; set; }
}