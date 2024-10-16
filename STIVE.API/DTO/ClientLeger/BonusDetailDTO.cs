using STIVE.API.DTO.Core;
using STIVE.API.DTO;
using STIVE.API.Models;

namespace STIVE.API.DTO.ClientLeger
{
    public class BonusDTO : EntityDTO
    {
        public string Name { get; set; }
        public string Duration { get; set; }
        public float Price { get; set; }
        public BonusDTO(){}

    }
}
