using Microsoft.AspNetCore.Mvc;
namespace privatext;

public class TestConnectionController : ControllerBase
{
    [HttpGet("Ping")]
    public string Get()
    {
        return "Pong";
    }
}
