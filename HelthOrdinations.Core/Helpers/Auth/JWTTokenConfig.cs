using System;
namespace HelthOrdinations.Core.Helpers.Auth
{
	public class JWTTokenConfig
	{
        public string TokenSecret { get; set; }
        public string TokenIssuer { get; set; }
        public string TokenAudience { get; set; }
        public int TokenExpiration { get; set; }

        public JWTTokenConfig()
		{
		}
	}
}

