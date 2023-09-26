using System;
using System.Security.Claims;

namespace HelthOrdinations.Core.Helpers.Auth
{
	public static class ClaimsPrincipalExtensions
	{
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return int.Parse(principal.FindFirstValue(CustomClaimTypes.UserId));
        }
    }
}

