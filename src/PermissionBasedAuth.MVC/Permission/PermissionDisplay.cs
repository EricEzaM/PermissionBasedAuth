using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PermissionBasedAuth.MVC.Permission
{
	/// <summary>
	/// Allows for and easy way to access the Permission Enum's UI-friendly data.
	/// </summary>
	public class PermissionDisplay
	{
		public PermissionDisplay(string groupName, string name, string value, string description)
		{
			GroupName = groupName;
			Name = name;
			Value = value;
			Description = description;
		}

		public string GroupName { get; }
		public string Name { get; }
		public string Value { get; }
		public string Description { get; }

		public static List<PermissionDisplay> GetPermissionDisplays(Type permissionClass)
		{
			var results = new List<PermissionDisplay>();
			GetNestedPermissions(permissionClass, ref results);
			return results;
		}

		private static List<PermissionDisplay> GetNestedPermissions(Type type, ref List<PermissionDisplay> results)
		{
			// Get any permissions which reside in the provided class.
			results.AddRange(GetPermissionsInClass(type));

			// And check nested classes too.
			var types = type.GetNestedTypes(BindingFlags.Public | BindingFlags.Static);
			foreach (var nestedType in types)
			{
				GetNestedPermissions(nestedType, ref results);
			}

			return results;
		}

		private static List<PermissionDisplay> GetPermissionsInClass(Type type)
		{
			var results = new List<PermissionDisplay>();
			var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
			foreach (var field in fields)
			{
				// Don't display obsolete permissions.
				if (field.GetCustomAttribute<ObsoleteAttribute>() != null)
				{
					continue;
				}

				// Don't display non-displayable permissions.
				var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();
				if (displayAttribute == null)
				{
					continue;
				}

				results.Add(new PermissionDisplay(displayAttribute.GroupName, displayAttribute.Name, field.GetValue(null).ToString(), displayAttribute.Description));
			}

			return results;
		}
	}
}
