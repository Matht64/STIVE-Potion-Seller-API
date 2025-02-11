using STIVE.API.Models.Core;

namespace STIVE.API.Models;

public class Potion : Entity
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string Picture { get; set; }
    public List<GameDataPotion> GameDatasPotion { get; set; }

    public Potion()
    {
        GameDatasPotion = new List<GameDataPotion>();
    }
}