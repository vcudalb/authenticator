namespace Authenticator.Api.Requests.Countries;

public class CreateCountryRequest
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string PhoneCode { get; set; }
}