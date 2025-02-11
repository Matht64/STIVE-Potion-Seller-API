using STIVE.API.Models.Core;

namespace STIVE.API.Models;

public class Cart : Entity
{
    public string UserId { get; set; }
    public int GameDataId { get; set; }
    public GameData GameData { get; set; }
    
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
}