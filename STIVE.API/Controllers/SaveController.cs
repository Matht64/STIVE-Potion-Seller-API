using STIVE.API.Services;
using Microsoft.AspNetCore.Mvc;
using STIVE.API.DTO.Both;
using STIVE.API.DTO.ClientLeger;
using STIVE.API.DTO.ClientLourd;
using STIVE.API.Models;

namespace STIVE.API.Controllers;

[ApiController]
[Route("[controller]")]
public partial class SaveController : Controller
{
    private readonly SaveService _saveService;

    public SaveController(SaveService saveService)
    {
        _saveService = saveService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var saves = _saveService.GetAll();
        return Ok(saves);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var save = _saveService.Get(id);
        return Ok(save);
    }

    [HttpPost]
    public IActionResult Add([FromBody] SaveToSaveDTO saveToSave)
    {
        _saveService.Add(saveToSave);
        return Ok(new { message = "Nouvelle sauvegarde effectuée" });
    }

    [HttpPut]
    public IActionResult Update(int id, [FromBody] SaveToSaveDTO saveToSave)
    {
        _saveService.Update(id, saveToSave);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _saveService.Delete(id);
        return Ok();
    }
}

