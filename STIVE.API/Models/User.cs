using STIVE.API.Models.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace STIVE.API.Models
{
    public class User : Entity
    { 
        public string Name { get; set; }    
        public string Email { get; set; }
        public string Password { get; set; }
        public string Tel { get; set; }
        public List<UserHasRole> UserHasRoles { get; set; }
        public User()
        {
            UserHasRoles = new List<UserHasRole>();
        }
    }
}
