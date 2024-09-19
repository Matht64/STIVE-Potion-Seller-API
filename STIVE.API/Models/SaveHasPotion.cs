using STIVE.API.Models.Core;

namespace STIVE.API.Models
{
    public class SaveHasPotion : Entity
    {
        public int SaveId { get; set; }
        public int PotionId { get; set; }
        public int PotionCount { get; set; }
        public Save Save { get; set; }
        public Potion Potion { get; set; }
    }
}
