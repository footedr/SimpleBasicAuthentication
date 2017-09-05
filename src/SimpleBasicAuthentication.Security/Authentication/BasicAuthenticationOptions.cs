using Microsoft.AspNetCore.Authentication;

namespace SimpleBasicAuthentication.Security.Authentication
{
	public class BasicAuthenticationOptions : AuthenticationSchemeOptions
	{
		public static string AuthenticationScheme => "BasicAuthentication";
	}
}