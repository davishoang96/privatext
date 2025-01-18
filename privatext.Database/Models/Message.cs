namespace privatext.Database.Models;

public class Message
{
    public required string KeyIdentifier { get; set; }
    public required string MessageId { get; set; }
    public required string Content { get; set; }
    public required DateTime DateCreated { get; set; }
}
