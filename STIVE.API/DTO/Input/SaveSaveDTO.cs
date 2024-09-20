using STIVE.API.DTO.Core;
using STIVE.API.Models;

namespace STIVE.API.DTO.Input
{
    public class SaveSaveDTO : EntityDTO
    {
        public string Name { get; set; }
        public int Gold { get; set; }
        public int idUser { get; set; }
        public List<int> SaveHasBonuses { get; set; }
        public List<int> SaveHasPotions { get; set; }
        public List<int> SaveHasSuppliers { get; set; }
        public SaveSaveDTO() 
        {
            SaveHasBonuses = new List<int>();
            SaveHasPotions = new List<int>();
            SaveHasSuppliers = new List<int>();
        }

    }
}
