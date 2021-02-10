using Microsoft.AspNetCore.Identity;
using PermissionBasedAuth.MVC.Areas.Identity.Data;
using PermissionBasedAuth.MVC.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionBasedAuth.MVC.Seeds
{
	public class DefaultUsers
	{
		public static async Task SeedBasicUser(UserManager<PermissionAuthUser> userManager)
		{
			var defaultUser = new PermissionAuthUser
			{
				UserName = "basic@email.com",
				Email = "basic@email.com",
				EmailConfirmed = true,
			};

			if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
			{
				var user = await userManager.FindByEmailAsync(defaultUser.Email);
				if (user == null)
				{
					await userManager.CreateAsync(defaultUser, "Password1!");
					await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
				}
			}
		}

		public static async Task SeedSuperAdminUser(UserManager<PermissionAuthUser> userManager)
		{
			var defaultUser = new PermissionAuthUser
			{
				UserName = "super@email.com",
				Email = "super@email.com",
				EmailConfirmed = true
			};

			if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
			{
				if (await userManager.FindByEmailAsync(defaultUser.Email) == null)
				{
					await userManager.CreateAsync(defaultUser, "Password1!");
					await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
					await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
					await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
				}
			}
		}

		public static async Task SeedManagerUser(UserManager<PermissionAuthUser> userManager)
		{
			var defaultUser = new PermissionAuthUser
			{
				UserName = "manager@email.com",
				Email = "manager@email.com",
				EmailConfirmed = true,
			};

			if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
			{
				var user = await userManager.FindByEmailAsync(defaultUser.Email);
				if (user == null)
				{
					await userManager.CreateAsync(defaultUser, "Password1!");
					await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
					await userManager.AddToRoleAsync(defaultUser, Roles.Manager.ToString());
				}
			}
		}
	}
}
