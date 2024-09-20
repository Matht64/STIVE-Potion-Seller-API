using STIVE.API.DTO.Core;
using STIVE.API.DTO;

namespace STIVE.API.DTO
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