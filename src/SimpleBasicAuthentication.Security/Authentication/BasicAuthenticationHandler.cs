using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleBasicAuthentication.Security.Users;

namespace SimpleBasicAuthentication.Security.Authentication
{
	public class BasicAuthenticationHandler
		: AuthenticationHandler<BasicAuthenticationOptions>
	{
		private const string EncodingName = "ISO-8859-1";
		private readonly IApiSignInManager _apiSignInManager;
		private readonly IUserManager _userManager;

		public BasicAuthenticationHandler(IApiSignInManager apiSignInManager, IUserManager userManager, IOptionsMonitor<BasicAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
			: base(options, logger, encoder, clock)
		{
			_apiSignInManager = apiSignInManager;
			_userManager = userManager;
		}

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			string authHeader = Context.Request.Headers["Authorization"];

			if (authHeader == null || !authHeader.StartsWith("Basic"))
				return AuthenticateResult.Fail("Authentication information not detected inside the request.");

			var encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
			var encoding = Encoding.GetEncoding(EncodingName);
			var usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

			var usernamePasswordArray = usernamePassword.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
			if (usernamePasswordArray.Length != 2)
				return AuthenticateResult.Fail("Authentication information not detected inside the request.");

			var username = usernamePasswordArray[0];
			var password = usernamePasswordArray[1];

			var user = await _userManager.GetAsync(username);

			if (user == null || !_userManager.ValidateUser(user, password))
				return AuthenticateResult.Fail("User validation failed");

			_apiSignInManager.SignIn(user);
			return AuthenticateResult.Success(new AuthenticationTicket(Context.User, null, BasicAuthenticationOptions.AuthenticationScheme));
		}
	}
}