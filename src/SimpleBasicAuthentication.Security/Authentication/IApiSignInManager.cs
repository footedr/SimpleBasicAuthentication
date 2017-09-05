using SimpleBasicAuthentication.Domain.Users;

namespace SimpleBasicAuthentication.Security.Authentication
{
	public interface IApiSignInManager
	{
		void SignIn(User user);
	}
}