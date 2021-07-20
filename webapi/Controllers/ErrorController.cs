using Microsoft.AspNetCore.Mvc;

namespace Pokemon.Api.Controllers
{
    // referred from Handle errors in ASP.NET Core web APIs on https://docs.microsoft.com/
    /// <summary>
    /// Error controller, used to returns error details in production environments.
    /// TODO: need to be improved to log the errors and return a Error report.
    /// </summary>
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Default error route
        /// </summary>
        /// <returns></returns>
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
