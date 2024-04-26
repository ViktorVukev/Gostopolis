namespace Gostopolis.Controllers;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Abstract controller that is used to mark all application controllers as ApiController and to set their route.
/// </summary>
[ApiController]
[Route("[controller]")]
public abstract class ApiController : ControllerBase
{
    /// <summary>
    /// Used when building the routes.
    /// </summary>
    public const string PathSeparator = "/";

    /// <summary>
    /// Used instead of setting {id} in the route.
    /// </summary>
    public const string Id = "{id}";
}