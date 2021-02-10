using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PermissionBasedAuth.MVC.Areas.Identity.Data;
using PermissionBasedAuth.MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PermissionBasedAuth.MVC.Seeds
{
	public static class DatabaseSeeder
	{
		public static async Task SeedAsync(IServiceProvider services)
		{
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();
			var logger = loggerFactory.CreateLogger("app");

			try
			{
				var userManager = services.GetRequiredService<UserManager<PermissionAuthUser>>();
				var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
				var context = services.GetRequiredService<PermissionAuthDbContext>();

				await DefaultRoles.SeedAsync(roleManager);
				await DefaultUsers.SeedBasicUser(userManager);
				await DefaultUsers.SeedSuperAdminUser(userManager);
				await DefaultUsers.SeedManagerUser(userManager);

				logger.LogInformation("Finished seeding.");
			}
			catch (Exception e)
			{
				logger.LogWarning(e, "An error occurred when seeding the database");
			}
		}
	}
}
