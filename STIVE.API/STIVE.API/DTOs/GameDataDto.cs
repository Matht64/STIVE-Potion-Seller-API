using STIVE.API.Models;

namespace STIVE.API.DTOs;

public class GameDataDto
{
    public int Id { get; set; }
    public int Gold { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; } 
    public List<GameDataBonus> GameDataBonuses { get; set; }
    public List<GameDataPotion> GameDataPotions { get; set; }
    public List<GameDataSupplier> GameDataSuppliers { get; set; }
}