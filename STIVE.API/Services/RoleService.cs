using STIVE.API.Database;
using STIVE.API.Models;
using STIVE.API.DTO.ClientLeger;
using STIVE.API.DTO.ClientLourd;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using STIVE.API.DTO.ClientLeger;

namespace STIVE.API.Services;

public class RoleService
{
    private readonly stive_potion_seller_context _database;

    public RoleService(stive_potion_seller_context database)
    {
        _database = database;
    }

    public List<RoleDTO> GetAllRoles()
    {
        var roles = _database.role
            .Include(r => r.UsersHasRole)
            .ThenInclude(r => r.User)
            .ToList();
        var a = roles.Select(l => new RoleDTO
        {
            Name = l.Name,
            UsersHasRole = l.UsersHasRole
                .Select(m => new UserDTO { Name = m.User.Name })
                .ToList()
        }).ToList();
        return a;
    }

    public Role GetRoleById(int id)
    {
        return _database.role.FirstOrDefault(l => l.Id == id);
    }

    public Role AddRole(Role role)
    {
        _database.role.Add(role);
        _database.SaveChanges();
        return role;
    }

    public Role UpdateRole(Role role)
    {
        var a = _database.role.Find(role.Id);
        a.Name = role.Name;
        _database.role.Update(a);
        _database.SaveChanges();
        return role;
    }

    public Role DeleteRole(int id)
    {
        var role = _database.role.Find(id);
        _database.role.Remove(role);
        _database.SaveChanges();
        return role;
    }
}