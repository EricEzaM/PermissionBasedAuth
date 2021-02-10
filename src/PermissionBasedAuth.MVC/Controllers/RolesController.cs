using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PermissionBasedAuth.MVC.Areas.Identity.Data;
using PermissionBasedAuth.MVC.Models;
using PermissionBasedAuth.MVC.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PermissionBasedAuth.MVC.Controllers
{
	[Route("roles")]
	public class RolesController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public RolesController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		[HttpGet]
		[HasPermission(Permissions.Roles.Read)]
		public IActionResult Roles()
		{
			return View(_roleManager.Roles.ToList());
		}

		[HttpGet("{roleId}")]
		[HasPermission(Permissions.Roles.Edit.Permissions)]
		public async Task<IActionResult> RolePermissions(string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);
			var roleClaimValues = (await _roleManager.GetClaimsAsync(role))
				.Where(c => c.Type == CustomClaimTypes.Permission)
				.Select(c => c.Value);

			RolePermissionsViewModel vm = new()
			{
				RoleId = role.Id,
				RoleName = role.Name,
				Permissions = PermissionDisplay.GetPermissionDisplays(typeof(Permissions))
					.Select(pd => new RolePermissionViewModel()
					{
						Name = pd.Name,
						GroupName = pd.GroupName,
						Description = pd.Description,
						Value = pd.Value,
						Active = roleClaimValues.Contains(pd.Value)
					}).ToList()
			};

			return View(vm);
		}

		[HttpPost("{roleId}")]
		[HasPermission(Permissions.Roles.Edit.Permissions)]
		public async Task<IActionResult> RolePermissions(string roleId, RolePermissionsViewModel model)
		{
			var role = await _roleManager.FindByIdAsync(roleId);

			// Get Current Permission Claims
			var currentPermissionClaims = (await _roleManager.GetClaimsAsync(role))
				.Where(c => c.Type == CustomClaimTypes.Permission)
				.Select(c => c.Value);
			var newPermissionClaims = model.Permissions
				.Where(p => p.Active)
				.Select(p => p.Value);

			var claimValuesToAdd = newPermissionClaims.Except(currentPermissionClaims);
			var claimValuesToRemove = currentPermissionClaims.Except(newPermissionClaims);

			foreach (var claimValue in claimValuesToRemove)
			{
				await _roleManager.RemoveClaimAsync(role, new Claim(CustomClaimTypes.Permission, claimValue));
			}

			foreach (var claimValue in claimValuesToAdd)
			{
				await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, claimValue));
			}

			TempData["SuccessMessage"] = "Permissions updated.";
			return RedirectToAction(nameof(RolePermissions), new { roleId });
		}

		[HttpPost("create")]
		[HasPermission(Permissions.Roles.Create)]
		public async Task<IActionResult> Create(CreateRoleViewModel model)
		{
			await _roleManager.CreateAsync(new IdentityRole(model.Name));
			return RedirectToAction(nameof(Roles));
		}

		[HttpPost("{roleId}/delete")]
		[HasPermission(Permissions.Roles.Delete)]
		public async Task<IActionResult> Delete(string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);
			await _roleManager.DeleteAsync(role);
			return RedirectToAction(nameof(Roles));
		}
	}
}
