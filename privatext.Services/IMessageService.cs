using privatext.Common.DTO;

namespace privatext.Services;

public interface IMessageService
{
    MessageDTO GetMessage(int messageId);
    Task<bool> DeleteMessage(int messageId);
    Task<bool> CreateMessage(MessageDTO messageDTO);
}
