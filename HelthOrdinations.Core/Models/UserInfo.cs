﻿using System;
namespace HelthOrdinations.Core.Models
{
	public class UserInfo
	{
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserStatusId { get; set; }

        public UserInfo()
		{
		}
	}
}

