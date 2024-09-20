using STIVE.API.DTO.Core;

namespace STIVE.API.DTO.Output
{
    public class UserDetailDTO : UserDTO
    {
        public string Email { get; set; }
        public string Tel { get; set; }
        public UserDetailDTO() { }
    }
}