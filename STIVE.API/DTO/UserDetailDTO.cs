using STIVE.API.DTO.Core;
using STIVE.API.DTO;

namespace STIVE.API.DTO
{
    public class UserDetailDTO : UserDTO
    {
        public string Email { get; set; }
        public string Tel { get; set; }
        public UserDetailDTO(){}
    }
}