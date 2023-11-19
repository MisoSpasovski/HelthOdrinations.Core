using HelthOrdinations.Core.DB;
using HelthOrdinations.Core.Helpers;
using HelthOrdinations.Core.Helpers.Auth;
using HelthOrdinations.Core.Models;
using HelthOrdinations.Core.Requets;
using HelthOrdinations.Core.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Locations.Core.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly HODbContext _dbContext;

    public ReservationsController(HODbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("SaveReservation")]
    public ActionResult<ReservationResponse> SaveReservation(ReservationRequest request)
    {
        var response = new ReservationResponse();

        var newReservation = new ReservationsInfo
        {
            UserId = User.GetUserId(),
            ClientId = request.ClientId,
            ReservationFrom = request.ReservationFrom,
            ReservationTo = request.ReservationTo,
            Description = request.Description
        };

        _dbContext.Reservations.Add(newReservation);

        _dbContext.SaveChanges();
            
        response.IsSuccess = true;
        response.Message = "The slot is successfully reserved !";
        return response;

    }

    [HttpGet("GetReservationsForClient")]
    public ActionResult<IEnumerable<ReservationsForClientResponse>> GetReservationsForClient(int clientId, DateOnly date)
    {
        return GetReservationsForClientInner(clientId, date);
    }

    [HttpGet("GetReservationsForClientFromClient")]
    public ActionResult<IEnumerable<ReservationsForClientResponse>> GetReservationsForClient(DateOnly date)
    {
        return GetReservationsForClientInner(User.GetUserId(), date);
    }


    private ActionResult<IEnumerable<ReservationsForClientResponse>> GetReservationsForClientInner(int clientId, DateOnly date)
    {

        var reservationsForClient = (from r in _dbContext.Reservations
                                    join u in _dbContext.Users on r.UserId equals u.Id
                                    join c in _dbContext.Clients on r.ClientId equals c.Id
                                    join wh in _dbContext.WorkingHours on c.Id equals wh.ClientId
                                    where r.ClientId == clientId && r.ReservationFrom.DayOfYear == date.DayOfYear
                                    select new ReservationsForClientResponse
                                    {
                                        Id = r.Id,
                                        UserId = u.Id,
                                        UserUsername = u.UserName,
                                        UserEmail = u.Email,
                                        ClientId = c.Id,
                                        ClientUsername = c.Name,
                                        ClientEmail = c.Email,
                                        ReservationFrom = r.ReservationFrom,
                                        ReservationTo = r.ReservationTo,
                                        Description = r.Description,
                                        ReservationFromInt = ConverFromDatetimeToInt.ConvertDatetimeToInt(r.ReservationFrom),
                                        ReservationToInt = ConverFromDatetimeToInt.ConvertDatetimeToInt(r.ReservationTo)


                                    }).ToList();



        return Ok(reservationsForClient);
    }

    [HttpGet("GetWorkingHours")]
    public ActionResult<WorkingHoursResponse> GetWorkingHours(int clientId)
    {
        var workingHours = _dbContext.WorkingHours.FirstOrDefault(x => x.ClientId == clientId);
        if (workingHours != null) {
            var response = new WorkingHoursResponse
            {
                Id = workingHours.Id,
                ClientId = workingHours.ClientId,
                WorkingHoursFrom = workingHours.WorkingHoursFrom,
                WorkingHoursTo = workingHours.WorkingHoursTo,
                PauseFrom = workingHours.PauseFrom,
                PauseTo = workingHours.PauseTo,
                WorkingHoursFromInt = ConverFromDatetimeToInt.ConvertDatetimeToInt(workingHours.WorkingHoursFrom),
                WorkingHoursToInt = ConverFromDatetimeToInt.ConvertDatetimeToInt(workingHours.WorkingHoursTo),
                PauseFromInt = ConverFromDatetimeToInt.ConvertDatetimeToInt(workingHours.PauseFrom),
                PauseToInt = ConverFromDatetimeToInt.ConvertDatetimeToInt(workingHours.PauseTo)
            };
            return Ok(response);
        }
        else
        {
            return null;
        }
    }

   
       
}
