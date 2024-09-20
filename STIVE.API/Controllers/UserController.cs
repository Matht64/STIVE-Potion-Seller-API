using STIVE.API.Services;
using Microsoft.AspNetCore.Mvc;
using STIVE.API.DTO.Output;
using STIVE.API.DTO.Input;
using STIVE.API.Models;

namespace STIVE.API.Controllers;

[ApiController]
[Route("[controller]")]
public partial class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _userService.GetUserById(id);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult AddUser([FromBody] UserSaveDTO user)
    {
        var createdUser = _userService.AddUser(user);
        return Ok(createdUser);
    }

    [HttpPut]
    public IActionResult UpdateUser([FromBody]User user)
    {
        var a = _userService.UpdateUser(user);
        return Ok(a);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _userService.DeleteUser(id);
        return Ok(user);
    }
}

