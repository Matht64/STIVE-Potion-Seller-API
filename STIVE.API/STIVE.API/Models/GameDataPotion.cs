namespace STIVE.API.Models;

public class GameDataPotion
{
    public int GameDataId { get; set; }
    public GameData GameData { get; set; }
    
    public int PotionId { get; set; }
    public Potion Potion { get; set; }
    
    public int Quantity { get; set; }
}