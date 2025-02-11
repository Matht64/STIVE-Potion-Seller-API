using Microsoft.AspNetCore.Identity;
using STIVE.API.Models.Core;

namespace STIVE.API.Models;

public class GameData : Entity
{
    public int Gold { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
    
    public List<GameDataBonus> GameDataBonuses { get; set; }
    public List<GameDataPotion> GameDataPotions { get; set; }
    public List<GameDataSupplier> GameDataSuppliers { get; set; }

    public GameData()
    {
        GameDataBonuses = new List<GameDataBonus>();
        GameDataPotions = new List<GameDataPotion>();
        GameDataSuppliers = new List<GameDataSupplier>();
    }
}