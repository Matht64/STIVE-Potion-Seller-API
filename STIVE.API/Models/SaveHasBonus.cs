using STIVE.API.Models.Core;

namespace STIVE.API.Models
{
    public class SaveHasBonus: Entity
    {
        public int SaveId { get; set; }
        public int BonusId { get; set; }
        public DateTime? DateEnd { get; set; }
        public Save Save { get; set; }
        public Bonus Bonus { get; set; }
    }
}
