namespace STIVE.API.Models;

public class GameDataBonus
{
    public int GameDataId { get; set; }
    public GameData GameData { get; set; }
    
    public int BonusId { get; set; }
    public Bonus Bonus { get; set; }
    
    public DateTime? DateEnd { get; set; }
    public int Quantity { get; set; }
}