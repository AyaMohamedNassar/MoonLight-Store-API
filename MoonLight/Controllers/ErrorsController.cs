using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoonLight.API.Errors;

namespace MoonLight.API.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public ErrorsController() { }

        public IActionResult Error(int code) 
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
