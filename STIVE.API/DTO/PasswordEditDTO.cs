using STIVE.API.DTO.Core;
using STIVE.API.DTO;

namespace STIVE.API.DTO
{
    public class PasswordEditDTO
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public PasswordEditDTO(){}
    }
}