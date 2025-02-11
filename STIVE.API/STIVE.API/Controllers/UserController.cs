using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using STIVE_API.Services;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;

namespace STIVE_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("count")]
    public IActionResult GetUserCount()
    {
        var userCount = _userService.GetUserCount();
        return Ok(userCount);
    }
    
    [HttpGet("getUserGold")]
    public IActionResult GetUserGold()
    {
        try
        {
            var userGold = _userService.GetUserGold();
            return Ok(userGold);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur : {ex.Message}");
            return StatusCode(500, "Une erreur est survenue lors de la récupération des données.");
        }
    }
    
    [HttpGet("getAll")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }
    
    [HttpGet("getById/{userId}")]
    public async Task<IActionResult> GetById(string userId)
    {
        var user = await _userService.GetById(userId);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }
        return Ok(user);
    }
    
    [HttpGet("profileUser")]
    //[Authorize]
    public async Task<IActionResult> GetUserProfile()
    {
        var userProfile = await _userService.GetUserProfile(User);
        if (userProfile == null)
            return NotFound(new { message = "Utilisateur non trouvé !" });
        return Ok(userProfile);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.RegisterUser(model);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { message = "Utilisateur créé avec succès !" });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = await _userService.GetUserByName(model.UserName);
        if (user == null)
            return Unauthorized("Utilisateur introuvable !");

        var result = await _userService.Login(model.UserName, model.Password);
        if (!result.Succeeded)
            return Unauthorized("Échec de la connexion !");
        
        var tokenString = await _userService.GenerateJwtTokenAsync(model.UserName, user);

        return Ok(new
        {
            Token = tokenString,
            Expiration = DateTime.UtcNow.AddDays(1)
        });
    }

    [HttpPost("logout")]
    //[Authorize]
    public async Task<IActionResult> Logout()
    {
        await _userService.Logout();

        return Ok(new { message = "Déconnexion réussie !" });
    }

    [HttpPut("update/{userId}")]
    //[Authorize]
    public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.UpdateUser(userId, model);//, User);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { message = "Utilisateur mis à jour avec succès !" });
    }

    [HttpPut("changePassword")]
    //[Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] UpdatePasswordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.ChangePassword(User.Identity.Name, dto.OldPassword, dto.NewPassword);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { message = "Mot de passe modifié avec succès !" });
    }

    [HttpPut("resetPassword")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.ResetPassword(model.UserId);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { message = "Mot de passe réinitialisé avec succès !" });
    }
    
    [HttpDelete("delete/{userId}")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var result = await _userService.DeleteUser(userId);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { message = "Utilisateur supprimé avec succès !" });
    }
}