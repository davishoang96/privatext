using privatext.Common.DTO;
using privatext.Database;
using privatext.Database.Models;

namespace privatext.Services;

public class MessageService : IMessageService
{
    private readonly DatabaseContext db;
    public MessageService(DatabaseContext databaseContext)
    {
        db = databaseContext;
    }

    public async Task<bool> CreateMessage(MessageDTO messageDTO)
    {
        if (messageDTO == null || string.IsNullOrEmpty(messageDTO.Content))
        {
            return false;
        }

        await db.Messages.AddAsync(new Message
        {
            KeyIdentifier = messageDTO.KeyIdentifer,
            MessageId = messageDTO.MessageId,
            Content = messageDTO.Content,
            DateCreated = DateTime.Now,
        });

        await db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteMessage(string messageId)
    {
        if (string.IsNullOrEmpty(messageId))
        {
            return false;
        }

        var message = db.Messages.SingleOrDefault(s => s.MessageId == messageId);
        if (message == null)
        {
            return false;
        }

        db.Remove(message);
        await db.SaveChangesAsync();

        return true;
    }

    public MessageDTO GetMessage(string messageId)
    {
        if (string.IsNullOrEmpty(messageId))
        {
            return null;
        }

        var message = db.Messages.SingleOrDefault(s => s.MessageId == messageId);
        if (message == null)
        {
            return null;
        }

        return new MessageDTO
        {
            KeyIdentifer = message.KeyIdentifier,
            MessageId = message.MessageId,
            Content = message.Content,
            DateCreated = message.DateCreated,
        };
    }
}
