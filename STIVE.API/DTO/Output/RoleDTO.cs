using STIVE.API.DTO.Core;

namespace STIVE.API.DTO.Output
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
