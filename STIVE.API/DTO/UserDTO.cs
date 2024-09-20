using STIVE.API.DTO.Core;
using STIVE.API.DTO;

namespace STIVE.API.DTO
{
    public class UserDTO : EntityDTO
    {
        public string Name { get; set; }
        public List<RoleDTO> Roles { get; set; }
        public UserDTO()
        {
            Roles = new List<RoleDTO>();
        }
    }
}