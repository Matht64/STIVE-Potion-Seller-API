using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using STIVE.API.DTOs;

namespace STIVE.API.Interfaces;

public interface IUserService
{
    int GetUserCount();
    List<UserGoldDto> GetUserGold();
    Task<List<UserDto>> GetAllUsers();
    Task<UserDto> GetById(string userId);
    Task<UserDto> GetUserProfile(ClaimsPrincipal currentUser);
    Task<IdentityUser> GetUserByName(string username);
    Task<IdentityResult> RegisterUser(RegisterUserDto model);
    Task<SignInResult> Login(string username, string password);
    Task<string> GenerateJwtTokenAsync(string username, IdentityUser user);
    Task Logout();
    Task<IdentityResult> UpdateUser(string userId, UpdateUserDto model);//, ClaimsPrincipal currentUser);
    Task<IdentityResult> ChangePassword(string userName, string oldPassword, string newPassword);
    Task<IdentityResult> ResetPassword(string userId);
    Task<IdentityResult> DeleteUser(string userId);
}