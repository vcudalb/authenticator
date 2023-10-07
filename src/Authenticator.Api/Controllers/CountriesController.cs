using Authenticator.Application.Countries.Commands;
using Authenticator.Application.DTOs.Countries;
using Authenticator.Domain.Requests.Countries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Authenticator.Api.Controllers;

/// <summary>
/// Provides countries' operation and process handling.
/// </summary>
[Route("api/v{version:apiVersion}/countries/")]
public class CountriesController : ApiController
{
    private readonly IMediator _mediator;
    private ILogger<CountriesController> _logger;

    /// <summary>
    /// Constructs a new instance of the <see cref="CountriesController"/> representing countries controller used to manage tokens, users, etc.
    /// </summary>
    /// <param name="mediator">
    /// An instance of the <see cref="IMediator"/> representing the request and response handler.
    /// </param>
    /// <param name="logger">
    /// An instance of the <see cref="ILogger{CountriesController}"/> representing the countries controller's logger.
    /// </param>
    public CountriesController(IMediator mediator, ILogger<CountriesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Handles creation of the country.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateCountryRequest request)
    {
        var response =  await _mediator.Send(new CreateCountryCommand(new CountryDto()));

        return Created(nameof(Create).ToLower(), response);
    }
}