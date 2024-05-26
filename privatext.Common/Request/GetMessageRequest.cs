using FastEndpoints;

namespace privatext.Common.Request;

public class GetMessageRequest
{
    [FromBody]
    public string MessageId { get; set; }
}
