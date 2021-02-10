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
				await roleManager.AddClaimAsync(superAdmin, new Claim(CustomClaimTypes.Permission, Permissions.Roles.Create));
				await roleManager.AddClaimAsync(superAdmin, new Claim(CustomClaimTypes.Permission, Permissions.Roles.Read));
				await roleManager.AddClaimAsync(superAdmin, new Claim(CustomClaimTypes.Permission, Permissions.Roles.Delete));
				await roleManager.AddClaimAsync(superAdmin, new Claim(CustomClaimTypes.Permission, Permissions.Roles.Edit.Metadata));
				await roleManager.AddClaimAsync(superAdmin, new Claim(CustomClaimTypes.Permission, Permissions.Roles.Edit.Permissions));
				await roleManager.AddClaimAsync(superAdmin, new Claim(CustomClaimTypes.Permission, Permissions.Users.Create));
				await roleManager.AddClaimAsync(superAdmin, new Claim(CustomClaimTypes.Permission, Permissions.Users.Read));
				await roleManager.AddClaimAsync(superAdmin, new Claim(CustomClaimTypes.Permission, Permissions.Users.Edit));
			}

			var basic = await roleManager.FindByNameAsync(Roles.Basic.ToString());
			if (basic == null)
			{
				await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));

				//basic = await roleManager.FindByNameAsync(Roles.Basic.ToString());
			}

			var admin = await roleManager.FindByNameAsync(Roles.Admin.ToString());
			if (admin == null)
			{
				await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

				admin = await roleManager.FindByNameAsync(Roles.Admin.ToString());
				await roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, Permissions.Roles.Create));
				await roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, Permissions.Roles.Read));
				await roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, Permissions.Roles.Delete));
				await roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, Permissions.Roles.Edit.Metadata));
				await roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, Permissions.Roles.Edit.Permissions));
				await roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, Permissions.Users.Create));
				await roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, Permissions.Users.Read));
				await roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, Permissions.Users.Edit));
			}

			var manager = await roleManager.FindByNameAsync(Roles.Manager.ToString());
			if (manager == null)
			{
				await roleManager.CreateAsync(new IdentityRole(Roles.Manager.ToString()));

				//manager = await roleManager.FindByNameAsync(Roles.Manager.ToString());
			}

			var contractor = await roleManager.FindByNameAsync(Roles.Contractor.ToString());
			if (contractor == null)
			{
				await roleManager.CreateAsync(new IdentityRole(Roles.Contractor.ToString()));

				//contractor = await roleManager.FindByNameAsync(Roles.Contractor.ToString());
			}
		}
	}
}
