using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Tetris.Models.Users
{
	public class CustomUserIdentity : ClaimsIdentity
	{
		public int Id { get; set; }

		public CustomUserIdentity(User user, string authenticationType = "Cookie") : base(GetUserClaims(user), authenticationType)
		{
			Id = user.Id;
		}

		private static List<Claim> GetUserClaims(User user)
		{
			var result = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Name),
				new Claim(ClaimTypes.Role, "Admin"),
			};

			return result;
		}
	}
}