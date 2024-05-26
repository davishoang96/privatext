using Newtonsoft.Json;

namespace privatext.Common.Response;

public class ErrorResponse
{
    [JsonProperty("StatusCode")]
    public int StatusCode { get; set; }

    [JsonProperty("Message")]
    public string Message { get; set; }
}