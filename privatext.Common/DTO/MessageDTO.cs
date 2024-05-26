namespace privatext.Common.DTO;

public class MessageDTO
{
    public required string MessageId { get; set; }
    public required string Content { get; set; }
    public DateTime DateCreated { get; set; }
}
