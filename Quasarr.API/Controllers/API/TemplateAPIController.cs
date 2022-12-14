// LFInteractive LLC. - All Rights Reserved
using Microsoft.AspNetCore.Mvc;

namespace Quasarr.API.Controllers.API
{
    [ApiController]
    [Route("/api/template")]
    public class TemplateAPIController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Index()
        {
            return Ok(new
            {
                message = "This is the template",
                time = DateTime.Now.ToString("MM/dd/yyyy - HH:mm:ss.fff")
            });
        }
    }
}