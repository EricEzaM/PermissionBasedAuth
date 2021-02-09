using Microsoft.AspNetCore.Identity;
using PermissionBasedAuth.MVC.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionBasedAuth.MVC.Models
{
	public class RolePermissionsViewModel
	{
		public string RoleId { get; set; }
		public string RoleName { get; set; }
		public List<RolePermissionViewModel> Permissions { get; set; }
	}

	public class RolePermissionViewModel
	{
		public string GroupName { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Value { get; set; }
		public bool Active { get; set; }
	}
}
