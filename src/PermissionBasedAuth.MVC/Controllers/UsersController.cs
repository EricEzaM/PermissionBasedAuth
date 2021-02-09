using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PermissionBasedAuth.MVC.Areas.Identity.Data;
using PermissionBasedAuth.MVC.Models;
using PermissionBasedAuth.MVC.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionBasedAuth.MVC.Controllers
{
	[HasPermission(Permissions.Test.SuperAdminOnly)]
	[Route("users")]
	public class UsersController : Controller
	{
		private readonly UserManager<PermissionAuthUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UsersController(UserManager<PermissionAuthUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[HttpGet]
		public IActionResult Users()
		{
			var allUsers = _userManager
				.Users
				.ToList()
				.OrderBy(u => u.UserName); // Consistent ordering on refresh

			return View(allUsers);
		}

		[HttpGet("{userId}/roles")]
		public async Task<IActionResult> UserRoles(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			var userRoles = await _userManager.GetRolesAsync(user);
			var roleVmList = _roleManager.Roles.ToList().Select(r =>
			{
				return new UserRoleViewModel
				{
					RoleName = r.Name,
					Active = userRoles.Contains(r.Name)
				};
			}).ToList();

			UserRolesViewModel vm = new()
			{
				User = user,
				UserRoles = roleVmList
			};

			return View(vm);
		}

		[HttpPost("{userId}/roles")]
		public async Task<IActionResult> UserRoles(string userId, UserRolesViewModel model)
		{
			var user = await _userManager.FindByIdAsync(userId);
			var currentRoles = await _userManager.GetRolesAsync(user);
			var newRoles = model.UserRoles.Where(r => r.Active).Select(r => r.RoleName);

			// Do some logic here to avoid removing all roles and then adding them again.
			var rolesToAdd = newRoles.Except(currentRoles).ToList();
			var rolesToRemove = currentRoles.Except(newRoles).ToList();

			await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
			await _userManager.AddToRolesAsync(user, rolesToAdd);

			TempData["SuccessMessage"] = "Roles updated.";
			return RedirectToAction(nameof(UserRoles), new { userId });
		}
	}
}
