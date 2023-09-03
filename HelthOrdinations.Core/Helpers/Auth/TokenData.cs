using System;
namespace HelthOrdinations.Core.Helpers.Auth
{
	public class TokenData
	{
        public string Value { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public TokenData()
		{
		}
	}
}

