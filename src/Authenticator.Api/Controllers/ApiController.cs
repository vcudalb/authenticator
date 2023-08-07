using Microsoft.AspNetCore.Mvc;

namespace Authenticator.Api.Controllers;

/// <summary>
/// Base controller, used for setting common attributes shared with nested controllers.
/// </summary>
[ApiVersion("1")]
[ApiController]
public class ApiController : ControllerBase
{
    
}