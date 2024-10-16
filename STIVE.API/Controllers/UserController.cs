using STIVE.API.Services;
using Microsoft.AspNetCore.Mvc;
using STIVE.API.DTO;
using STIVE.API.DTO.Both;
using STIVE.API.DTO.ClientLourd;
using STIVE.API.DTO.ClientLeger;
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
    
    [HttpGet("count")]
    public ActionResult<int> GetUserCount()
    {
        int count = _userService.GetUserCount();
        return Ok(count);
    }

    [HttpGet("classement")]
    public IActionResult GetUserGold()
    {
        var users = _userService.GetUserGold();
        return Ok(users);
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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDTO userRegisterDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var newUser = _userService.Register(userRegisterDto);
            return Ok(new { message = "Nouvel utilisateur créé!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erreur lors de la création de l'utilisateur: {ex.Message}");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = await _userService.Login(userLoginDto);
        if (user == null)
        {
            return Unauthorized("Identifiants invalides");
        }
        return Ok(new { message = "Connexion réussie !" });
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

