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

		public static class Roles
		{
			[Display(GroupName = "Roles", Name = "Create", Description = "Create new roles.")]
			public const string Create = "Permissions.Roles.Create";
			
			public static class Edit
			{
				[Display(GroupName = "Roles", Name = "Edit Metadata", Description = "Edit role metdata.")]
				public const string Metadata = "Permissions.Roles.Edit.Medatata";
				[Display(GroupName = "Roles", Name = "Edit Permissions", Description = "Edit which permissions are assigned to each role.")]
				public const string Permissions = "Permissions.Roles.Edit.Permissions";
			}
		}
	}
}
