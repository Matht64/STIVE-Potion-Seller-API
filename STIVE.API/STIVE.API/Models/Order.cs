using STIVE.API.Models.Core;

namespace STIVE.API.Models;

public class Order : Entity
{
    public string UserId { get; set; }
    public int GameDataId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public float TotalPrice { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}