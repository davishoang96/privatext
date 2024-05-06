using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace privatext;

[ApiController]
[Route("[controller]")]
public class TestConnectionController : ControllerBase
{
    [Authorize]
    [HttpGet("Ping")]
    public string Get()
    {
        return "Pong";
    }
}
