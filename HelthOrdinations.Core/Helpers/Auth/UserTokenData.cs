using System;
namespace HelthOrdinations.Core.Helpers.Auth
{
	public class UserTokenData: TokenData
	{
        public Guid UserId { get; set; }
        public string Email { get; set; }

        public UserTokenData()
		{
		}
	}
}

