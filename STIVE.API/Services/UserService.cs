using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STIVE.API.DTO.ClientLeger;
using STIVE.API.DTO.ClientLourd;
using STIVE.API.Database;
using STIVE.API.DTO;
using STIVE.API.Models;

namespace STIVE.API.Services;

public class UserService
{
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly stive_potion_seller_context _database;

    public UserService(stive_potion_seller_context database)
    {
        _database = database;
        _passwordHasher = new PasswordHasher<User>();
    }
    
    public int GetUserCount()
    {
        return _database.user.Count();
    }

    public List<UserDetailDTO> GetAllUsers()
    {
        var users = _database.user
            .Include(u => u.UserHasRoles)
            .ThenInclude(u => u.Role)
            .ToList();
        var a = users.Select(x => new UserDetailDTO
        {
            Name = x.Name,
            Email = x.Email,
            Tel = x.Tel,
            UserHasRoles = x.UserHasRoles
                .Select(c => new RoleDTO { Name = c.Role.Name })
                .ToList()
        }).ToList();
        return a;
    }

    public User GetUserById(int id)
    {
        return _database.user.FirstOrDefault(l => l.Id == id);
    }
    
    public List<UserGoldDTO> GetUserGold()
    {
        var userSaves = _database.save
            .Include(s => s.User)
            .Select(s=>new UserGoldDTO
            {
                Name = s.User.Name,
                Gold = s.Gold,
            })
            .OrderByDescending(s => s.Gold)
            .ToList();
        
        return userSaves;
    }
    public User Register(UserRegisterDTO userRegisterDto)
    {
        var hashPassword = _passwordHasher.HashPassword(new User(), userRegisterDto.Password);
        if (hashPassword == null)
        {
            throw new ArgumentNullException(nameof(userRegisterDto), "Password hashing failed");
        }
        var user = new User
        {
            Name = userRegisterDto.Name,
            Email =userRegisterDto.Email,
            Tel = userRegisterDto.Tel,
            Password = hashPassword,
            UserHasRoles = new List<UserHasRole>
            {
                new UserHasRole
                {
                    RoleId = 2
                }
            }
        };
        _database.user.Add(user);
        _database.SaveChanges();
        return user;
    }
    public bool VerifyPassword(User user, string providedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, providedPassword);
        return result == PasswordVerificationResult.Success;
    }

    public async Task<User> Login(string email, string password)
    {
        var user = await _database.user.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null || !VerifyPassword(user, password))
        {
            return null;
        }
        return user;
    }

    public User UpdateUser(User user)
    {
        var a = _database.user.Find(user.Id);
        a.Name = user.Name;
        a.Email = user.Email;
        a.Password = user.Password;
        _database.user.Update(a);
        _database.SaveChanges();
        return a;
    }

    public User DeleteUser(int id)
    {
        var user = _database.user.Find(id);
        _database.user.Remove(user);
        _database.SaveChanges();
        return user;
    }
}