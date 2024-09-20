using STIVE.API.DTO.Core;
using STIVE.API.Models;

namespace STIVE.API.DTO.Output
{
    public class SaveDetailDTO : EntityDTO
    {
        public string Name { get; set; }
        public int Gold { get; set; }
        public int idUser { get; set; }
        public List<SaveHasBonus> SaveHasBonuses { get; set; }
        public List<SaveHasPotion> SaveHasPotions { get; set; }
        public List<SaveHasSupplier> SaveHasSuppliers { get; set; }
        public SaveDetailDTO()
        {
            SaveHasBonuses = new List<SaveHasBonus>();
            SaveHasPotions = new List<SaveHasPotion>();
            SaveHasSuppliers = new List<SaveHasSupplier>();
        }

    }
}
