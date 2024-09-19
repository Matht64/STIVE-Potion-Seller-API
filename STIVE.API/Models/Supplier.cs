using STIVE.API.Models.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace STIVE.API.Models
{
    public class Supplier : Entity
    {
        //[ForeignKey("PotionId")]
        public int PotionId { get; set; }
        public Potion Potion { get; set; }
        public string Picture { get; set; }
        public int PriceSupplier { get; set; }
        public List<SaveHasSupplier> SavesHasSupplier { get; set; }
        public Supplier() 
        {
            SavesHasSupplier = new List<SaveHasSupplier>();
        }
        
    }
}
