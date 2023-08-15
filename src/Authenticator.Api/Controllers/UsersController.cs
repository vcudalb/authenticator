using MediatR;
using Microsoft.AspNetCore.Components;

namespace Authenticator.Api.Controllers;

/// <summary>
/// Provides users' operation and process handling.
/// </summary>
[Route("api/v{version:apiVersion}/users/")]
public class UsersController : ApiController
{
    private readonly IMediator _mediator;
    private ILogger<UsersController> _logger;

    /// <summary>
    /// Constructs a new instance of the <see cref="UsersController"/> representing users controller used to manage tokens, users, etc.
    /// </summary>
    /// <param name="mediator">
    /// An instance of the <see cref="IMediator"/> representing the request and response handler.
    /// </param>
    /// <param name="logger">
    /// An instance of the <see cref="ILogger{UsersController}"/> representing the users controller's logger.
    /// </param>
    public UsersController(IMediator mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
}