using STIVE.API.DTO.Core;
using STIVE.API.DTO.ClientLeger;

namespace STIVE.API.DTO.ClientLeger
{
    public class RoleDTO : EntityDTO
    {
        public string? Name { get; set; }
        public List<UserDTO> UsersHasRole { get; set; }
        public RoleDTO()
        {
            UsersHasRole = new List<UserDTO>();
        }

    }
}
