namespace privatext.Common.DTO;

public class MessageDTO
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public DateTime DateCreated { get; set; }
}
