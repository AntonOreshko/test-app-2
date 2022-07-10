using Microsoft.AspNetCore.Mvc;

namespace TestApp2.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingsController: ControllerBase
{
    private readonly IConfiguration _config;
    
    public SettingsController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet]
    public IActionResult GetSetting(string settingPath)
    {
        var value = _config[settingPath];
        if (value == null)
            return NotFound();

        return Ok(value);
    }
}