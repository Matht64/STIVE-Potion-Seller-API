using STIVE.API.DTO.Core;
using STIVE.API.DTO;
using STIVE.API.Models;

namespace STIVE.API.DTO.Input
{
    public class BonusToSaveDTO : EntityDTO
    {
        public string Name { get; set; }
        public string Duration { get; set; }
        public List<int> BonusHasSaves { get; set; }
        public BonusToSaveDTO() 
        {
            BonusHasSaves = new List<int>();
        }

    }
}
