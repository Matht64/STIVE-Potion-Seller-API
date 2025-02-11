using STIVE.API.Models.Core;

namespace STIVE.API.Models;

public class OrderItem : Entity
{
    public int OrderId { get; set; }
    public int BonusId { get; set; }
    public int Quantity { get; set; }
    public float PriceAtPurchase { get; set; }

    public Bonus Bonus { get; set; }
}