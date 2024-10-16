using STIVE.API.DTO.Core;

namespace STIVE.API.DTO.ClientLeger
{
    public class UserDetailDTO : UserDTO
    {
        public string Email { get; set; }
        public string Tel { get; set; }
        public UserDetailDTO() { }
    }
}