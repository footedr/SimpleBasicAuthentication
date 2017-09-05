using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SampleBasicAuthentication.Data;
using SimpleBasicAuthentication.Domain.Users;

namespace SimpleBasicAuthentication.Security.Users
{
	public class UserManager
		: IUserManager
	{
		private readonly IPasswordHasher<User> _passwordHasher;
		private readonly UserContext _userContext;

		public UserManager(UserContext userContext, IPasswordHasher<User> passwordHasher)
		{
			_userContext = userContext;
			_passwordHasher = passwordHasher;
		}

		public bool ValidateUser(User user, string password)
		{
			return VerifyHashedPassword(user, password);
		}

		public Task<int> CreateAsync(User user)
		{
			user.Password = _passwordHasher.HashPassword(user, user.Password);
			_userContext.Users.Add(user);
			return _userContext.SaveChangesAsync();
		}

		public Task<User> GetAsync(string username)
		{
			return _userContext.Users.FirstOrDefaultAsync(u => u.Username == username);
		}

		private bool VerifyHashedPassword(User user, string password)
		{
			var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
			return passwordVerificationResult == PasswordVerificationResult.Success;
		}
	}
}