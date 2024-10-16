using STIVE.API.DTO.Core;
using STIVE.API.DTO.ClientLourd;

namespace STIVE.API.DTO.ClientLeger
{
    public class UserToSaveDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Password { get; set; }
        public List<int> UserHasRoles { get; set; }
        public UserToSaveDTO()
        {
            UserHasRoles = new List<int>();
        }
    }
}