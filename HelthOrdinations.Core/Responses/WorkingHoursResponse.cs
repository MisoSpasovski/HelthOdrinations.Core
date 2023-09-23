﻿using System;
namespace HelthOrdinations.Core.Responses
{
	public class WorkingHoursResponse
	{
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime WorkingHoursFrom { get; set; }
        public DateTime WorkingHoursTo { get; set; }
        public DateTime PauseFrom { get; set; }
        public DateTime PauseTo { get; set; }

        public WorkingHoursResponse()
		{
		}
	}
}

