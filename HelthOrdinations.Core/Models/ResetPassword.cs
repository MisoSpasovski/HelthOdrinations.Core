﻿using System;
namespace HelthOrdinations.Core.Models
{
	public class ResetPassword
    {
		public string Email { get; set; }
		public string NewPassword { get; set; }
		public ResetPassword()
		{
		}
	}
}

