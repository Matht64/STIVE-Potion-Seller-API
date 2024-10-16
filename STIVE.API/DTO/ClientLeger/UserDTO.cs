using STIVE.API.DTO.ClientLeger;
using STIVE.API.DTO.Core;

namespace STIVE.API.DTO.ClientLeger
{
    public class UserDTO : EntityDTO
    {
        public string Name { get; set; }
        public List<RoleDTO> UserHasRoles { get; set; }
        public UserDTO()
        {
            UserHasRoles = new List<RoleDTO>();
        }
    }
}