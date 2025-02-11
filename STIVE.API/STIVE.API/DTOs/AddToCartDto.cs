namespace STIVE.API.DTOs;

public class AddToCartDto
{
    public string UserId { get; set; }
    public int GameDataId { get; set; }
    public int BonusId { get; set; }
    public int Quantity { get; set; }
}