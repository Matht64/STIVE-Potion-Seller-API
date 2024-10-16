using STIVE.API.Services;
using Microsoft.AspNetCore.Mvc;
using STIVE.API.DTO.Both;
using STIVE.API.DTO.ClientLeger;
using STIVE.API.DTO.ClientLourd;
using STIVE.API.Models;

namespace STIVE.API.Controllers;

[ApiController]
[Route("[controller]")]
public partial class BonusController : Controller
{
    private readonly BonusService _bonusService;

    public BonusController(BonusService bonusService)
    {
        _bonusService = bonusService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var bonuses = _bonusService.GetAll();
        return Ok(bonuses);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var bonus = _bonusService.Get(id);
        return Ok(bonus);
    }

    [HttpPost]
    public IActionResult Add([FromBody] BonusToSaveDTO bonusToSave)
    {
        _bonusService.Add(bonusToSave);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update(int id, [FromBody] BonusToSaveDTO bonusToSave)
    {
        _bonusService.Update(id, bonusToSave);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _bonusService.Delete(id);
        return Ok();
    }
}

