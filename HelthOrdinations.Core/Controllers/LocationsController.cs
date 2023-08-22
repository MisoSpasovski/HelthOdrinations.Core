using Microsoft.AspNetCore.Mvc;

namespace Locations.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{    
    [HttpGet("GetLocations")]
    public ActionResult<string> GetLocations()
    {
        return Ok("asd");
;    }
}
