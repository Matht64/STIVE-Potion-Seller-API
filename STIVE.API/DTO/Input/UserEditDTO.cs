using STIVE.API.DTO.Core;

namespace STIVE.API.DTO.Input
{
    public class UserEditDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public UserEditDTO() { }
    }
}