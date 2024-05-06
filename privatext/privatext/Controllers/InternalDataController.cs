using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace privatext.Controllers;

public class InternalDataController : PrivaBaseController
{
    [Authorize]
    [HttpGet("GetData")]
    public IEnumerable<int> GetData()
    {
        return Enumerable.Range(1, 5).Select(index =>
            Random.Shared.Next(1, 100))
            .ToArray();
    }
}
