using STIVE.API.DTO.Core;
using STIVE.API.DTO;

namespace STIVE.API.DTO
{
    public class UserDetailSaveDTO : UserDetailDTO
    {
        public string Password { get; set; }
        public UserDetailSaveDTO(){}
    }
}