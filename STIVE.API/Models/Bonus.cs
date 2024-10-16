using STIVE.API.Models.Core;

namespace STIVE.API.Models
{
    public class Bonus : Entity
    {
        public string Name { get; set; }
        public string Duration { get; set; }
        public float Price { get; set; }
        public List<SaveHasBonus> BonusHasSaves { get; set; }
        public Bonus() 
        {
            BonusHasSaves = new List<SaveHasBonus>();
        }
    }
}
