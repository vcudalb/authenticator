using Microsoft.AspNetCore.Mvc;

namespace Authenticator.Api.Controllers;

[Microsoft.AspNetCore.Components.Route("api/v{version:apiVersion}/user/")]
public class UserController : ApiController
{
}