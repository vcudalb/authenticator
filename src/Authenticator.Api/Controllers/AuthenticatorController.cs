using Authenticator.Application.Tokens.Commands;
using Authenticator.Domain.Requests.Authenticators.Tokens;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Authenticator.Api.Controllers;

/// <summary>
/// Provides authenticators' operation and process handling.
/// </summary>
[Route("api/v{version:apiVersion}/authenticators/")]
public class AuthenticatorController : ApiController
{
    private readonly IMediator _mediator;
    private ILogger<AuthenticatorController> _logger;

    /// <summary>
    /// Constructs a new instance of the <see cref="AuthenticatorController"/> representing authenticators controller used to manage tokens, users, etc.
    /// </summary>
    /// <param name="mediator">
    /// An instance of the <see cref="IMediator"/> representing the request and response handler.
    /// </param>
    /// <param name="logger">
    /// An instance of the <see cref="ILogger{AuthenticatorController}"/> representing the authenticators controller's logger.
    /// </param>
    public AuthenticatorController(IMediator mediator, ILogger<AuthenticatorController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Handles token process.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("token")]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<IActionResult> TokenAsync([FromForm] TokenRequest request)
    {
        var token = await _mediator.Send(new CreateTokenCommand(request));
        return Created(nameof(TokenAsync), token);
    }
}