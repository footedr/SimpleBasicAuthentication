using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SimpleBasicAuthentication.Domain.Users;
using SimpleBasicAuthentication.Security.Users;

namespace SimpleBasicAuthentication.Security.Authentication
{
	public class ApiSignInManager
		: IApiSignInManager
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IUserClaimsFactory _userClaimsFactory;

		public ApiSignInManager(IHttpContextAccessor httpContextAccessor, IUserClaimsFactory userClaimsFactory)
		{
			_httpContextAccessor = httpContextAccessor;
			_userClaimsFactory = userClaimsFactory;
		}

		public void SignIn(User user)
		{
			// create authenticated ClaimsPrincipal
			var claims = _userClaimsFactory.Create(user);
			var identity = new ClaimsIdentity(claims, "ApiAuthentication");
			var principal = new ClaimsPrincipal(identity);
			_httpContextAccessor.HttpContext.User = principal;
		}
	}
}