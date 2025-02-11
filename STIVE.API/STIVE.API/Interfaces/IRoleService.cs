using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using STIVE.API.DTOs;

namespace STIVE.API.Interfaces;

public interface IRoleService
{
    Task<List<RoleDto>> GetAllRoles();
    Task<RoleDto> GetById(string roleId);

    Task<List<string>> GetUserRoles(string userId);
    Task<IdentityResult> CreateRoleAsync(CreateRoleDto createRoleDto);
    Task<string> AssignRole(string userId, string roleName);
    Task<string> RemoveRole(string userId, string roleName);
    Task<bool> UpdateRoleAsync(RoleDto roleDto);
    Task<string> DeleteRole(string roleId);
}