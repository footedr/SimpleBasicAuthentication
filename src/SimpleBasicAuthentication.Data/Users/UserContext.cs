using Microsoft.EntityFrameworkCore;
using SimpleBasicAuthentication.Domain.Users;

namespace SampleBasicAuthentication.Data
{
	public sealed class UserContext
		: DbContext
	{
		public UserContext(DbContextOptions options)
			: base(options)
		{
			// For the InMemory database to test
			Users.Add(new User { Username = "username", Password = "AQAAAAEAACcQAAAAEHecxZr0c7vD8bIhsz/V5lTFDJ9hDTn06J8rqHhYqNcB0lzTrW4gYVVO8kh91acbyw==" });
			SaveChanges();
		}

		public DbSet<User> Users { get; set; }		
	}
}