using Quartz;

namespace privatext.Services;

public class DeleteMessageJob : IJob
{
    private readonly IMessageService _messageService;
    public DeleteMessageJob(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        // Get the MessageId from the job's data map
        var messageId = context.JobDetail.JobDataMap.GetString("MessageId");

        if (!string.IsNullOrEmpty(messageId))
        {
            // Call the message service to delete the message
            await _messageService.DeleteMessage(messageId);
        }
    }
}
