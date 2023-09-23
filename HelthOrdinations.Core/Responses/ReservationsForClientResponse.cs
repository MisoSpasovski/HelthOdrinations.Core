using System;
namespace HelthOrdinations.Core.Responses
{
	public class ReservationsForClientResponse
	{
		public int Id {get;set;}
		public int UserId { get; set; }
		public string UserUsername {get;set; }
        public string UserEmail { get; set; }
		public int ClientId { get; set; }
		public string ClientUsername { get; set; }
		public string ClientEmail { get; set; }
		public DateTime ReservationFrom { get; set; }
		public DateTime ReservationTo { get; set; }
		public string Description { get; set; }

        public ReservationsForClientResponse()
		{
		}
	}
}

