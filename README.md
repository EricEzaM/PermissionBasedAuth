# Permission Based Authorization in .NET 5

A small test project to play with Permission-based authorization in .NET 5 MVC. A combination of ideas and implementations which I have found in various articles and posts on the internet.

- [x] Permissions working at a basic level. Can (un)assign permissions from roles, and assign roles to users.
- [x] Working with Cookies
- [ ] Apply changes to user roles and role permissions immediately - don't require user to log out and back in for changes to be applied to cookie
- [ ] Working with JWT (Separate API endpoints)
- [ ] Override permissions on a per-user basis (?)

### Run it

`docker-compose up` in `./database` to spin up the Postgres database in a docker container. Uses a volume to persist data. Then, run the Application with `dotnet run`. Go to https://localhost:5001/ to explore the app, and https://localhost:8080/ to explore the database in Adminer with login and pass "postgres".

### Features

* Authentication using ASP.NET Identity Users and Roles.
* Permission-based Authorization (via roles)
* Web-based management for creating, deleting and editing permissions of roles, as well as assigning users to roles.

### Architecture for Permission-Based Authorization

Kept very simple as it is only a sample, and to be used for reference.

There are 3 main things which are **required** to set this up:
1. `IAuthorizationRequirement`, in this case `PermissionRequirement`. A basic class which contains data about the requirement (such as the name of the permission the User is required to have).
2. `IAuthorizationPolicyProvider`, in this case `PermissionPolicyProvider`. The policy provider is used when processing the `Authorize` attribute on controllers and actions. We can implement `GetPolicyAsync(string policyName)` to return a policy which includes any `IAuthorizationRequirement`'s which we want to be fulfilled. In this example project, each "Permission" will have it's own policy. The policies will be dynamically generated at runtime, depending on the permission name passed into the authorize attribute, e.g. `[Authorize(Permissions.Roles.Read)]`
3. `AuthorizationHandler<IAuthorizationRequirement>`, in this case `PermissionAuthorizationHandler`. When you set the running policy has a requirement that the handler handles, the `HandleRequirementAsync` method of this class will be called. This method can be overridden to include logic to check if the authenticated user (`context.User`) fulfils the requirement.

There are a few helpful but non-essential classes that have been set up too.
1. `HasPermissionAttribute`: A wrapper around the `Authorize` attribute, which is just for organisation and clarity of intent (`[HasPermission(Permissions.Roles.Read)]`).
2. `Permissions`: A class which contains all permissions - example below. Avoids using raw strings around the codebase for permissions. This can have nested classes for better organisation, for example rather than doing `Permissions.RolesRead`, `Permissions.UsersEdit`, etc, the permissions can be put under a main "Roles" or "Users" class, which cleans up intellisense and makes things easier to read. I have seen this be made an `enum` instead, which has the added benefit of each permission being a number, so it takes up less space when sent to the browser in a cookie. The class-based implementation could be converted to have the same benefits by changing it from strings to integers, and unit tests could be written to ensure there are no duplicate values.

```cs
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
}
```