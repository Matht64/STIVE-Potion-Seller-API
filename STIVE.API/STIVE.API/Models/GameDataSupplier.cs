namespace STIVE.API.Models;

public class GameDataSupplier
{
    public int GameDataId { get; set; }
    public GameData GameData { get; set; }
    
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
}