using STIVE.API.Models.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace STIVE.API.Models
{
    public class Save : Entity
    {
        public int Gold { get; set; }
        //[ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public List<SaveHasBonus> SaveHasBonuses { get; set; }
        public List<SaveHasPotion> SaveHasPotions { get; set; }
        public List<SaveHasSupplier> SaveHasSuppliers { get; set; }
        public Save()
        {
            SaveHasBonuses = new List<SaveHasBonus>();
            SaveHasPotions = new List<SaveHasPotion>();
            SaveHasSuppliers = new List<SaveHasSupplier>();
        }
    }
}
