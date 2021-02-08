using Microsoft.AspNetCore.Identity;
using PermissionBasedAuth.MVC.Areas.Identity.Data;
using PermissionBasedAuth.MVC.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PermissionBasedAuth.MVC.Seeds
{
	public class DefaultRoles
	{
		public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
		{
			var superAdmin = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
			if (superAdmin == null)
			{
				await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));

				superAdmin = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
				await roleManager.AddClaimAsync(superAdmin, new Claim("Permission", Permissions.Test.Basic));
				await roleManager.AddClaimAsync(superAdmin, new Claim("Permission", Permissions.Test.SuperAdminOnly));
			}

			var basic = await roleManager.FindByNameAsync(Roles.Basic.ToString());
			if (basic == null)
			{
				await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));

				basic = await roleManager.FindByNameAsync(Roles.Basic.ToString());
				await roleManager.AddClaimAsync(basic, new Claim("Permission", Permissions.Test.Basic));
			}

			var admin = await roleManager.FindByNameAsync(Roles.Admin.ToString());
			if (admin == null)
			{
				await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

				admin = await roleManager.FindByNameAsync(Roles.Admin.ToString());
				await roleManager.AddClaimAsync(basic, new Claim("Permission", Permissions.Test.Basic));
			}

			var manager = await roleManager.FindByNameAsync(Roles.Manager.ToString());
			if (manager == null)
			{
				await roleManager.CreateAsync(new IdentityRole(Roles.Manager.ToString()));

				manager = await roleManager.FindByNameAsync(Roles.Manager.ToString());
				await roleManager.AddClaimAsync(basic, new Claim("Permission", Permissions.Test.Basic));
			}

			var contractor = await roleManager.FindByNameAsync(Roles.Contractor.ToString());
			if (contractor == null)
			{
				await roleManager.CreateAsync(new IdentityRole(Roles.Contractor.ToString()));

				contractor = await roleManager.FindByNameAsync(Roles.Contractor.ToString());
				await roleManager.AddClaimAsync(basic, new Claim("Permission", Permissions.Test.Basic));
			}
		}
	}
}
