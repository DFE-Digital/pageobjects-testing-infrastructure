using Microsoft.AspNetCore.Mvc;

namespace Dfe.Testing.Pages.TestWebApp.Controllers;
public class ComponentController : Controller
{
    [HttpGet]
    [Route("component/{component}/{type=default}")]
    public IActionResult Index([FromRoute] string component, [FromRoute] string type)
    {
        if (string.IsNullOrEmpty(component))
        {
            return BadRequest("component is empty");
        }
        if (string.IsNullOrEmpty(type))
        {
            return BadRequest("type is empty");
        }
        return View(component + "/" + type);
    }

}
