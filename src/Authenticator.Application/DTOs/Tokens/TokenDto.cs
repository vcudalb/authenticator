namespace Authenticator.Application.DTOs.Tokens;

public class TokenDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Grant_Type { get; set; }
    public string Scope { get; set; }
    public string Refresh_Token { get; set; }
    public string Client_Id { get; set; }
    public string Client_Secret { get; set; }
}