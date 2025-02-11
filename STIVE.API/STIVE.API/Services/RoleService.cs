using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STIVE.API.Database;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;

namespace STIVE_API.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RoleService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager,
        StiveDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<List<RoleDto>> GetAllRoles()
    {
        var roles = await _roleManager.Roles
            .Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name
            })
            .ToListAsync();

        return roles;
    }
    
    public async Task<RoleDto> GetById(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
            return null;

        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name
        };
    }

    public async Task<List<string>> GetUserRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new ApplicationException($"User {userId} not found");
        
        var roles = await _userManager.GetRolesAsync(user);
        return roles.ToList();
    }

    public async Task<IdentityResult> CreateRoleAsync(CreateRoleDto createRoleDto)
    {
        var role = new IdentityRole
        {
            Name = createRoleDto.Name
        };
        
        var result = await _roleManager.CreateAsync(role);
        return result;
    }

    public async Task<string> AssignRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new ApplicationException($"User {userId} not found");
        
        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (!result.Succeeded)
            throw new ApplicationException($"Error assigning role {roleName}");

        return "Role Assigned";
    }

    public async Task<string> RemoveRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new ApplicationException($"User {userId} not found");
        
        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        if (!result.Succeeded)
            throw new ApplicationException($"Error deleting role {roleName}");
        
        return "Role Removed";
    }
    
    public async Task<bool> UpdateRoleAsync(RoleDto roleDto)
    {
        var role = await _roleManager.FindByIdAsync(roleDto.Id);
        if (role == null)
            return false;

        role.Name = roleDto.Name;

        var result = await _roleManager.UpdateAsync(role);
        return result.Succeeded;
    }

    public async Task<string> DeleteRole(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
            throw new ApplicationException($"Role {roleId} not found");
        
        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
            throw new ApplicationException($"Error deleting role {roleId}");
        
        return "Role Deleted";
    }
}