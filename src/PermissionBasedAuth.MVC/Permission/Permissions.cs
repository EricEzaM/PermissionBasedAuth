using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionBasedAuth.MVC.Permission
{
	/// <summary>
	/// Contains all Permissions for the application.
	/// </summary>
	public static class Permissions
	{
		public static class Test
		{
			[Display(GroupName = "Test", Name = "Test One", Description = "First Test Permission")]
			public const string Basic = "Permissions.Test.Basic";
			[Display(GroupName = "Test", Name = "Test Two", Description = "Second Test Permission")]
			public const string SuperAdminOnly = "Permissions.Test.SuperAdminOnly";
		}
	}
}
