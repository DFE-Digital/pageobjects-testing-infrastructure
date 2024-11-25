using Microsoft.AspNetCore.Mvc;

namespace Dfe.Testing.Pages.TestWebApp.Controllers;
public class ComponentController : Controller
{
    [HttpGet]
    [Route("component/{component}/{type=default}")]
    public IActionResult Index([FromRoute] string component, [FromRoute] string type)
    {
        return string.IsNullOrEmpty(component) ? BadRequest() : View(component);
    }

}
