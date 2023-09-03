using HelthOrdinations.Core.DB;
using HelthOrdinations.Core.Helpers.EmailSender;
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
    }

    [HttpGet("GetLocationDetails")]
    public ActionResult<IEnumerable<LocationsInfo>> GetLocationDetails(int locationsid)
    {
        var locationDetails = _dbContext.LocationDetails.Where(x => x.LocationsId == locationsid).FirstOrDefault();

        return Ok(locationDetails);
        ;
    }
}
