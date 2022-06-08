using Microsoft.AspNetCore.Mvc;
using Tjololo.DI.Interfaces;

namespace simple_webapp.Controllers;

[ApiController]
[Route("/api/v1")]
public class SimpleApi: ControllerBase
{
    private readonly ILogger<SimpleApi> _logger;
    private readonly PercentageCalculator _calc;
    
    public SimpleApi(ILogger<SimpleApi> logger, PercentageCalculator calc)
    {
        _logger = logger;
        _calc = calc;
    }
    
    [HttpGet]
    [Route("percentage")]
    public async Task<IActionResult> Get([FromQuery(Name="prize")] int prize = 0)
    {
        return Ok("Prize with percentages added: " + _calc.AddPercentage(prize));
    }
}