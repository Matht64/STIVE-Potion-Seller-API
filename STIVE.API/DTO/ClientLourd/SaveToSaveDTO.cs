using STIVE.API.DTO.Core;
using STIVE.API.Models;

namespace STIVE.API.DTO.ClientLourd
{
    public class SaveToSaveDTO : EntityDTO
    {
        public string Name { get; set; }
        public int Gold { get; set; }
        public int UserId { get; set; }
        public List<int> SaveHasBonuses { get; set; }
        public List<int> SaveHasPotions { get; set; }
        public List<int> SaveHasSuppliers { get; set; }
        public SaveToSaveDTO() 
        {
            SaveHasBonuses = new List<int>();
            SaveHasPotions = new List<int>();
            SaveHasSuppliers = new List<int>();
        }

    }
}
