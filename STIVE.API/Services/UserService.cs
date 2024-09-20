using Microsoft.EntityFrameworkCore;
using STIVE.API.DTO.Output;
using STIVE.API.DTO.Input;
using STIVE.API.Database;
using STIVE.API.Models;

namespace STIVE.API.Services;

public class UserService
{
    private readonly stive_potion_seller_context _database;

    public UserService(stive_potion_seller_context database)
    {
        _database = database;
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
    
    public User AddUser(UserToSaveDTO UserToSaveDto)
    {
        var role = _database.role.ToList();
        var user = new User
        {
            Name = UserToSaveDto.Name,
            Email = UserToSaveDto.Email,
            Password = UserToSaveDto.Password,
            UserHasRoles = UserToSaveDto.UserHasRoles.Select(id => new UserHasRole
            {
                RoleId = id
            }).ToList(),
        };
        _database.user.Add(user);
        _database.SaveChanges();
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