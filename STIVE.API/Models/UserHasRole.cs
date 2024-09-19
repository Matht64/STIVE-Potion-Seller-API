using STIVE.API.Models.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace STIVE.API.Models
{
    public class UserHasRole : Entity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }

    }
}
