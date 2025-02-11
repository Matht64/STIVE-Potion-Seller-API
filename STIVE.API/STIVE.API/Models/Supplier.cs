using STIVE.API.Models.Core;

namespace STIVE.API.Models;

public class Supplier : Entity
{
    public string Name { get; set; }
    public string Picture { get; set; }

    public int PotionId { get; set; }
    public Potion Potion { get; set; }

    public List<GameDataSupplier> GameDatasSupplier { get; set; }

    public Supplier()
    {
        GameDatasSupplier = new List<GameDataSupplier>();
    }
}