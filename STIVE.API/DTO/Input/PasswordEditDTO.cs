using STIVE.API.DTO.Core;

namespace STIVE.API.DTO.Input
{
    public class PasswordEditDTO
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public PasswordEditDTO() { }
    }

}