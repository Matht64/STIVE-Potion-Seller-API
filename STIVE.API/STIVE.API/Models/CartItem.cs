using STIVE.API.Models.Core;

namespace STIVE.API.Models;

public class CartItem : Entity
{
    public int CartId { get; set; }
    public Cart Cart { get; set; }
    public int BonusId { get; set; }
    public Bonus Bonus { get; set; }
    public int Quantity { get; set; }
}