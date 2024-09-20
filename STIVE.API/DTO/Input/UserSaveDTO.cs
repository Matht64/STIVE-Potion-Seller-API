using STIVE.API.DTO.Core;
using STIVE.API.DTO.Output;

namespace STIVE.API.DTO.Input
{
    public class UserSaveDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Password { get; set; }
        public List<int> UserHasRoles { get; set; }
        public UserSaveDTO()
        {
            UserHasRoles = new List<int>();
        }
    }
}