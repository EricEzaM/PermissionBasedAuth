using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionBasedAuth.MVC.Controllers
{
	public class PermissionController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public PermissionController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
