using STIVE.API.Models.Core;

namespace STIVE.API.Models
{
    public class Potion : Entity
    {
        public string Name { get; set; }
        public int PriceCustomer { get; set; }
        public string Picture { get; set; }
        public List<SaveHasPotion> SavesHasPotion { get; set; }
        public Potion() 
        {
            SavesHasPotion = new List<SaveHasPotion>();
        }
    }
}
