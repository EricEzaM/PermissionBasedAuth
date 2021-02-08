using PermissionBasedAuth.MVC.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionBasedAuth.MVC.Models
{
	public class UserRolesViewModel
	{
		public PermissionAuthUser User { get; set; }
		public List<UserRoleViewModel> UserRoles { get; set; }
	}

	public class UserRoleViewModel
	{
		public string RoleName { get; set; }
		public bool Active { get; set; }
	}
}
