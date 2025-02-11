namespace STIVE.API.DTOs;

public class CreateBonusDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public float Price { get; set; }
}