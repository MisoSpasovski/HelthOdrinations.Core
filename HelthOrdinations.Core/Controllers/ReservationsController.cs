using HelthOrdinations.Core.DB;
using HelthOrdinations.Core.Helpers.EmailSender;
using HelthOrdinations.Core.Models;
using HelthOrdinations.Core.Models.Enums;
using HelthOrdinations.Core.Requets;
using HelthOrdinations.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Locations.Core.Controllers;

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
            UserId = request.UserId,
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
    public ActionResult<IEnumerable<ReservationsForClientResponse>> GetReservationsForClient(int clientId)
    {
        var reservationsForClient = (from r in _dbContext.Reservations
                                    join u in _dbContext.Users on r.UserId equals u.Id
                                    join c in _dbContext.Clients on r.ClientId equals c.Id
                                    where r.ClientId == clientId
                                    select new ReservationsForClientResponse
                                    {
                                        ReservationId = r.Id,
                                        UserId = u.Id,
                                        UserUsername = u.UserName,
                                        UserEmail = u.Email,
                                        ClientId = c.Id,
                                        ClientUsername = c.Name,
                                        ClientEmail = c.Email,
                                        ReservationFrom = r.ReservationFrom,
                                        ReservationTo = r.ReservationTo,
                                        Description = r.Description

                                   
                           }).ToList();



        return Ok(reservationsForClient);
    }
}
