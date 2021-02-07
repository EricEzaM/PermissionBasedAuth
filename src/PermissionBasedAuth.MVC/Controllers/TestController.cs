using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionBasedAuth.MVC.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionBasedAuth.MVC.Controllers
{
	public class TestController : Controller
	{
		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}

		[HasPermission(Permissions.Test.Basic)]
		public IActionResult TestOne()
		{
			return View();
		}

		[HasPermission(Permissions.Test.SuperAdminOnly)]
		public IActionResult TestTwo()
		{
			return View();
		}

		[Authorize]
		public IActionResult TestThree()
		{
			return View();
		}
	}
}
