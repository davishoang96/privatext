namespace privatext.Common.DTO;

public class MessageDTO
{
    public string KeyIdentifer { get; set; }
    public required string MessageId { get; set; }
    public required string Content { get; set; }
    public DateTime DateCreated { get; set; }
}
