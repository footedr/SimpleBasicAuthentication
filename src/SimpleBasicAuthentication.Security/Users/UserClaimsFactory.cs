using System;
using System.Collections.Generic;
using System.Security.Claims;
using SimpleBasicAuthentication.Domain.Users;

namespace SimpleBasicAuthentication.Security.Users
{
	public class UserClaimsFactory
		: IUserClaimsFactory
	{
		public IEnumerable<Claim> Create(User user)
		{
			if (string.IsNullOrWhiteSpace(user.Username))
				throw new ArgumentNullException(nameof(user.Username), "Username is null");
			return new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
			};
		}
	}
}