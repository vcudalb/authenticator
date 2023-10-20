using System.Diagnostics.CodeAnalysis;

namespace Authenticator.Domain.Requests.Countries;

[ExcludeFromCodeCoverage]
public class CreateCountryRequest
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string PhoneCode { get; set; }
}