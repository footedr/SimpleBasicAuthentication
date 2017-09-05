using System.Collections.Generic;
using System.Security.Claims;
using SimpleBasicAuthentication.Domain.Users;

namespace SimpleBasicAuthentication.Security.Users
{
	public interface IUserClaimsFactory
	{
		IEnumerable<Claim> Create(User user);
	}
}