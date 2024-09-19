using STIVE.API.Models.Core;

namespace STIVE.API.Models
{
    public class Role : Entity
    {
        public string? Name { get; set; }
        public List<UserHasRole> UsersHasRole { get; set; }
        public Role() 
        {
            UsersHasRole = new List<UserHasRole>();
        }

    }
}
