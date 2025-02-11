using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using STIVE.API.Database;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;

namespace STIVE_API.Services;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly StiveDbContext _context;

    public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, StiveDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _context = context;
    }
    public int GetUserCount() => _userManager.Users.Count();
    
    public List<UserGoldDto> GetUserGold()
    {
        var userGoldData = _context.GameData
            .Include(g => g.User)
            .Select(g => new UserGoldDto
            {
                UserName = g.User != null ? g.User.UserName : "Unknown",
                Gold = g.Gold
            })
            .OrderByDescending(g => g.Gold)
            .ToList();

        return userGoldData;
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = _userManager.Users.ToList();
        var userList = new List<UserDto>();
        
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userList.Add(new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = roles.ToList()
            });
        }

        return userList;
    }

    public async Task<UserDto> GetById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return null;
        }

        var roles = await _userManager.GetRolesAsync(user);
        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Roles = roles.ToList()
        };
    }

    public async Task<UserDto> GetUserProfile(ClaimsPrincipal currentUser)
    {
        var user = await _userManager.GetUserAsync(currentUser);
        if (user == null)
            return null;
        
        var roles = await _userManager.GetRolesAsync(user);

        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Roles = roles.ToList()
        };
    }
    public async Task<IdentityUser> GetUserByName(string username) =>
        await _userManager.FindByNameAsync(username);
    public async Task<IdentityResult> RegisterUser(RegisterUserDto model)
    {
        var user = new IdentityUser
        {
            UserName = model.UserName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
            await _userManager.AddToRoleAsync(user, "User");

        return result;
    }
    public async Task<SignInResult> Login(string username, string password) =>
        await _signInManager.PasswordSignInAsync(username, password, false, false);
    public async Task<string> GenerateJwtTokenAsync(string username, IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ]),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public async Task Logout() =>
        await _signInManager.SignOutAsync();
    private async Task<bool> IsUserAuthorizedAsync(ClaimsPrincipal currentUser, string userId)
    {
        var currentUserId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(currentUser), "Admin");

        return isAdmin || currentUserId == userId;
    }
    public async Task<IdentityResult> UpdateUser(string userId, UpdateUserDto model)//, ClaimsPrincipal currentUser)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return IdentityResult.Failed(new IdentityError { Description = "Utilisateur introuvable !" });

        //if (!await IsUserAuthorizedAsync(currentUser, userId))
            //return IdentityResult.Failed(new IdentityError { Description = "Utilisateur non autorisé !" });

        if (!string.IsNullOrEmpty(model.UserName)) user.UserName = model.UserName;
        if (!string.IsNullOrEmpty(model.Email)) user.Email = model.Email;
        if (!string.IsNullOrEmpty(model.PhoneNumber)) user.PhoneNumber = model.PhoneNumber;

        return await _userManager.UpdateAsync(user);
    }
    public async Task<IdentityResult> ChangePassword(string userName, string oldPassword, string newPassword)
    {
        var currentUser = await _userManager.FindByNameAsync(userName);
        if (currentUser == null)
            return IdentityResult.Failed(new IdentityError { Description = "Utilisateur non trouvé !" });

        return await _userManager.ChangePasswordAsync(currentUser, oldPassword, newPassword);
    }
    private string GenerateRandomPassword()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@$?_-";
        var passwordLength = _userManager.Options.Password.RequiredLength;

        return new string(Enumerable.Repeat(chars, passwordLength)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
    }
    public async Task<IdentityResult> ResetPassword(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return IdentityResult.Failed(new IdentityError { Description = "Utilisateur introuvable !" });

        var password = GenerateRandomPassword();
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        return await _userManager.ResetPasswordAsync(user, resetToken, password);
    }
    public async Task<IdentityResult> DeleteUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "Utilisateur non trouvé !" });

        return await _userManager.DeleteAsync(user);
    }
}