using STIVE.API.Models.Core;

namespace STIVE.API.Models
{
    public class SaveHasSupplier : Entity
    {
        public int SaveId { get; set; }
        public int SupplierId { get; set; }
        public Save Save { get; set; }
        public Supplier Supplier { get; set; }
    }
}
