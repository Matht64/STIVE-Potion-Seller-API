using STIVE.API.Models.Core;

namespace STIVE.API.Models;

public class Bonus : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public float Price { get; set; }
    public List<GameDataBonus> GameDatasBonus { get; set; }
    public Bonus()
    {
        GameDatasBonus = new List<GameDataBonus>();
    }
}