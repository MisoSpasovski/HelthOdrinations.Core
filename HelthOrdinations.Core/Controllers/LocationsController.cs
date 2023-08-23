using HelthOrdinations.Core.DB;
using HelthOrdinations.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Locations.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly HODbContext _dbContext;

    public LocationsController(HODbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("GetLocations")]
    public ActionResult<IEnumerable<LocationsInfo>> GetLocations(int typeId)
    {
        var locations = _dbContext.Locations.Where(x=>x.OrdinationsTypeId == typeId).ToList();

        return Ok(locations);
;    }
}
