using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PermissionBasedAuth.MVC.Areas.Identity.Data;
using PermissionBasedAuth.MVC.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionBasedAuth.MVC.Controllers
{
	[HasPermission(Permissions.Test.SuperAdminOnly)]
	public class UsersController : Controller
	{
		private readonly UserManager<PermissionAuthUser> _userManager;

		public UsersController(UserManager<PermissionAuthUser> userManager)
		{
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			var allUsers = _userManager
				.Users
				.ToList()
				.OrderBy(u => u.UserName); // Consistent ordering on refresh

			return View(allUsers);
		}
	}
}
