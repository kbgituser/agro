using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlatF.Extension
{
	public static class UserExtension
	{
		public static string GetUserId(this ClaimsPrincipal principal)
		{
			if (principal == null)
				throw new ArgumentNullException(nameof(principal));

			//return long.Parse(principal.FindFirst(ClaimTypes.Sid)?.Value);
			return principal.FindFirst(ClaimTypes.Sid)?.Value;
		}

		public static string GetLogin(this ClaimsPrincipal principal)
		{
			if (principal == null)
				throw new ArgumentNullException(nameof(principal));

			return principal.FindFirst(ClaimTypes.Name)?.Value;
		}
	}
}
