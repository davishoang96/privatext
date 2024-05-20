using privatext.Common.DTO;

namespace privatext.Services;

public interface IMessageService
{
    MessageDTO GetMessage(string messageId);
    Task<bool> DeleteMessage(string messageId);
    Task<bool> CreateMessage(MessageDTO messageDTO);
}
