using privatext.Common.DTO;

namespace privatext.Common.Request;

public class CreateMessageRequest
{
    public int DeletionTime { get; set; }
    public MessageDTO MessageDTO { get; set; }
}