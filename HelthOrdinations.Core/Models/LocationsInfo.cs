using System;
namespace HelthOrdinations.Core.Models
{
	public class LocationsInfo
	{
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int OrdinationsTypeId { get; set; }

        public LocationsInfo()
		{
		}
	}
}

