using STIVE.API.Models.Core;

namespace STIVE.API.Models
{
    public class Bonus : Entity
    {
        public string Name { get; set; }
        public string Duration { get; set; }
        public List<SaveHasBonus> SaveHasBonuses { get; set; }
        public Bonus() 
        {
            SaveHasBonuses = new List<SaveHasBonus>();
        }
    }
}
