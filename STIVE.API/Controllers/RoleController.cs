using STIVE.API.Services;
using Microsoft.AspNetCore.Mvc;
using STIVE.API.Models;

namespace STIVE.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : Controller
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public IActionResult GetAllRoles()
    {
        var roles = _roleService.GetAllRoles();
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public IActionResult GetRoleById(int id)
    {
        var role = _roleService.GetRoleById(id);
        return Ok(role);
    }

    [HttpPost]
    public IActionResult AddRole([FromBody] Role role)
    {
        var createdRole = _roleService.AddRole(role);
        return Ok(createdRole);
    }

    [HttpPut]
    public IActionResult UpdateRole([FromBody] Role role)
    {
        var a = _roleService.UpdateRole(role);
        return Ok(a);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRole(int id)
    {
        var role = _roleService.DeleteRole(id);
        return Ok(role);
    }
}