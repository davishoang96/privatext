using Microsoft.AspNetCore.Mvc;
namespace privatext.Controllers;

public class TestConnectionController : PrivaBaseController
{
    [HttpGet("Ping")]
    public string Ping()
    {
        return "Pong";
    }
}
