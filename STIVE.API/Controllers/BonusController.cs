using STIVE.API.Services;
using Microsoft.AspNetCore.Mvc;
using STIVE.API.DTO.Output;
using STIVE.API.DTO.Input;
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
        var user = _bonusService.Get(id);
        return Ok(user);
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
    public IActionResult DeleteUser(int id)
    {
        _bonusService.Delete(id);
        return Ok();
    }
}

