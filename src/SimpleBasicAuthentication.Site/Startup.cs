using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleBasicAuthentication.Data;
using SimpleBasicAuthentication.Domain.Users;
using SimpleBasicAuthentication.Security.Authentication;
using SimpleBasicAuthentication.Security.Users;

namespace SimpleBasicAuthentication.Site
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(action =>
			{
				var policy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.AddAuthenticationSchemes("BasicAuthentication")
					.Build();
				action.Filters.Add(new AuthorizeFilter(policy));
			});

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<IUserManager, UserManager>();
			services.AddScoped<IApiSignInManager, ApiSignInManager>();
			services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
			services.AddScoped<IUserClaimsFactory, UserClaimsFactory>();
			services.AddDbContext<UserContext>(options =>
				options.UseInMemoryDatabase("BasicAuthUsers")
			);
			services.AddAuthentication(config => { config.AddScheme("BasicAuthentication", configBuilder => { configBuilder.HandlerType = typeof(BasicAuthenticationHandler); }); });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseAuthentication();
			app.UseMvc();
		}
	}
}