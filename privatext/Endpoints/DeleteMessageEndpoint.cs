using FastEndpoints;
using privatext.Common.Request;
using privatext.Services;
using FluentValidation.Results;
using Quartz;

namespace privatext.Endpoints;

public class DeleteMessageEndpoint : Endpoint<DeleteMessageRequest, bool>
{
    private readonly IMessageService messageService;
    private readonly ISchedulerFactory schedulerFactory;
    public DeleteMessageEndpoint(IMessageService messageService, ISchedulerFactory schedulerFactory)
    {
        this.messageService = messageService;
        this.schedulerFactory = schedulerFactory;
    }
    
    public override void Configure()
    {
        Post("/deleteMessage/");
        AllowAnonymous();
        DontCatchExceptions();
    }

    public override async Task HandleAsync(DeleteMessageRequest r, CancellationToken c)
    {
        if (!string.IsNullOrEmpty(r.MessageId))
        {
            var result = await messageService.DeleteMessage(r.MessageId);
            var scheduler = await schedulerFactory.GetScheduler();
            await scheduler.DeleteJob(new JobKey($"DeleteMessage-{r.MessageId}", "MessageJobs"));

            await SendAsync(result);
        }
        else
        {
            ThrowError(new ValidationFailure
            {
                ErrorMessage = "Error when deleting a message",
                Severity = FluentValidation.Severity.Error,
                PropertyName = nameof(GetMessageEndpoint),
            });
        }
    }
}