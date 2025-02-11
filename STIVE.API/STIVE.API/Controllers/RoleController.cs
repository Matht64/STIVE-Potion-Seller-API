using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STIVE_API.Services;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;

namespace STIVE_API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    
    [HttpGet("getAll")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllRoles()
    {
        try
        {
            var roles = await _roleService.GetAllRoles();
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpGet("getById/{roleId}")]
    public async Task<IActionResult> GetById(string roleId)
    {
        var role = await _roleService.GetById(roleId);
        if (role == null)
        {
            return NotFound(new { message = "Role not found" });
        }
        return Ok(role);
    }

    [HttpGet("getUserRoles/{userId}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserRoles(string userId)
    {
        try
        {
            var roles = await _roleService.GetUserRoles(userId);
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("create")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
    {
        try
        {
            var message = await _roleService.CreateRoleAsync(createRoleDto);
            return Ok(new { message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("assignRoles")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignRoles([FromBody] UserRoleDto ressource)
    {
        try
        {
            var message = await _roleService.AssignRole(ressource.UserId, ressource.RoleName);
            return Ok(new { message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("removeRoles")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveRoles([FromBody] UserRoleDto userRoleDto)
    {
        try
        {
            var message = await _roleService.RemoveRole(userRoleDto.UserId, userRoleDto.RoleName);
            return Ok(new { message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPut("update/{roleId}")]
    public async Task<IActionResult> UpdateRole(string roleId, [FromBody] RoleDto roleDto)
    {
        if (roleId != roleDto.Id)
            return BadRequest("Role Ids do not match");
        
        var result = await _roleService.UpdateRoleAsync(roleDto);
        if (!result)
            return NotFound("Role not found");
        
        return Ok("Role updated");
    }

    [HttpDelete("delete/{roleId}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteRole(string roleId)
    {
        try
        {
            var message = await _roleService.DeleteRole(roleId);
            return Ok(new { message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
