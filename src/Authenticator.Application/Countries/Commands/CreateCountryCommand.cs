using Authenticator.Application.DTOs.Countries;
using MediatR;

namespace Authenticator.Application.Countries.Commands;

public record CreateCountryCommand(CountryDto Country) : IRequest<Guid>
{
    
}