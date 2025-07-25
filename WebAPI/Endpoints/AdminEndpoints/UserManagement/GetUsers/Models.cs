﻿using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.AdminEndpoints.UserManagement.GetUsers;

public sealed class AdminGetUsersRequest : PaginationParams
{
    public string? Name { get; set; }
    public string? Role { get; set; }
}

public sealed class AdminGetUserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ProfilePicture { get; set; } = string.Empty;
    public List<UserRoleObject> Roles { get; set; } = default!;
    public int WarningCount { get; set; }
    public DateTimeOffset CreationDate { get; set; }
}

public sealed class UserRoleObject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}