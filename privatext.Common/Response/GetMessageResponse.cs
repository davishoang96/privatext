using privatext.Common.DTO;

namespace privatext.Common.Response
{
    public class GetMessageResponse : BaseEndpointResponse
    {
        public MessageDTO MessageDTO { get; set; }
    }
}
