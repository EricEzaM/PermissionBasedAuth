using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PermissionBasedAuth.MVC.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionBasedAuth.MVC.Permission
{
	/// <summary>
	/// Authorization Handler for <see cref="PermissionRequirement"/>
	/// </summary>
	public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
	{
		protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
		{
			if (context.User == null)
			{
				return;
			}

			var permissions = context.User.Claims
				.Where(c => c.Type == CustomClaimTypes.Permission &&
							c.Value == requirement.Permission &&
							c.Issuer == "LOCAL AUTHORITY");

			if (permissions.Any())
			{
				context.Succeed(requirement);
				return;
			}
		}
	}
}
