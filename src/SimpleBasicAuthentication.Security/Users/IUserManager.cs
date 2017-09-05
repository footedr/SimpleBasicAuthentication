using System.Threading.Tasks;
using SimpleBasicAuthentication.Domain.Users;

namespace SimpleBasicAuthentication.Security.Users
{
	public interface IUserManager
	{
		/// <summary>
		///     Validates a username and password pair to determine whether to log in a user or not
		/// </summary>
		/// <returns>UserVerificationResult that denotes whether the user's credentials are valid, wrong, or if the user is not active</returns>
		bool ValidateUser(User user, string password);

		/// <summary>
		///     Hashes the specified password and creates a new Nucleus TMS user account
		/// </summary>
		/// <param name="user">The Nucleus TMS user to create</param>
		Task<int> CreateAsync(User user);

		/// <summary>
		///     Gets the Nucleus TMS user by the username or email address
		/// </summary>
		/// <param name="username">The Nucleus TMS username or email address</param>
		/// <returns>The Nucleus TMS user object</returns>
		Task<User> GetAsync(string username);
	}
}