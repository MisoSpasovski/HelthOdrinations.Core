using System;
namespace HelthOrdinations.Core.Requets
{
	public class ReservationRequest
	{
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public DateTime ReservationFrom { get; set; }
        public DateTime ReservationTo { get; set; }
        public string Description { get; set; }

        public ReservationRequest()
		{
		}
	}
}

