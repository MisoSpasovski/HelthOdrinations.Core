using System;
namespace HelthOrdinations.Core.Models
{
	public class ReservationsInfo
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int ClientId { get; set; }
		public DateTime ReservationFrom { get; set; }
        public DateTime ReservationTo { get; set; }
		public string Description { get; set; }

        public ReservationsInfo()
		{
		}
	}
}

