using Microsoft.AspNetCore.Mvc;
namespace privatext;

[ApiController]
[Route("[controller]")]
public class TestConnectionController : ControllerBase
{
    [HttpGet("Ping")]
    public string Get()
    {
        return "Pong";
    }
}
