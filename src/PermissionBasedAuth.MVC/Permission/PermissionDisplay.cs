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
		public PermissionDisplay(string groupName, string name, string description)
		{
			GroupName = groupName;
			Name = name;
			Description = description;
		}

		public string GroupName { get; }
		public string Name { get; }
		public string Description { get; }

		public static List<PermissionDisplay> GetPermissionDisplays(Type permissionClass)
		{
			var result = new List<PermissionDisplay>();

			FieldInfo[] fields = permissionClass.GetFields(BindingFlags.Public | BindingFlags.Static);
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

				result.Add(new PermissionDisplay(displayAttribute.GroupName, displayAttribute.Name, displayAttribute.Description));
			}

			return result;
		}
	}
}
