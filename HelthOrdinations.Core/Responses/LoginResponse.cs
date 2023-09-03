using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace HelthOrdinations.Core.Responses
{
	public class LoginResponse
	{
        [JsonProperty(PropertyName = "userToken")]
        public string UserToken { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public LoginResponse()
		{
		}
	}
}

