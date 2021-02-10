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
		public static class Users
		{
			[Display(GroupName = "Users", Name = "Create", Description = "Create new users.")]
			public const string Create = "Permissions.Users.Create";
			[Display(GroupName = "Users", Name = "Read", Description = "Read user data.")]
			public const string Read = "Permissions.Users.Read";
			[Display(GroupName = "Users", Name = "Edit", Description = "Edit user data.")]
			public const string Edit = "Permissions.Users.Edit";
		}

		public static class Roles
		{
			[Display(GroupName = "Roles", Name = "Create", Description = "Create new roles.")]
			public const string Create = "Permissions.Roles.Create";
			[Display(GroupName = "Roles", Name = "Read", Description = "Read role data.")]
			public const string Read = "Permissions.Roles.Read";
			[Display(GroupName = "Roles", Name = "Delete", Description = "Delete roles.")]
			public const string Delete = "Permissions.Roles.Delete";

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
