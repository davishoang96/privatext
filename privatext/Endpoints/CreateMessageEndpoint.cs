using FastEndpoints;
using privatext.Common.DTO;
using privatext.Common.Request;
using privatext.Database.Models;
using privatext.Services;
using Quartz;

namespace privatext.Endpoints;

public class CreateMessageEndpoint : Endpoint<CreateMessageRequest, bool>
{
    private readonly IMessageService messageService;
    private readonly ISchedulerFactory schedulerFactory;

    public CreateMessageEndpoint(IMessageService messageService, ISchedulerFactory schedulerFactory)
    {
        this.messageService = messageService;
        this.schedulerFactory = schedulerFactory;
    }

    public override void Configure()
    {
        Post("/createMessage/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateMessageRequest r, CancellationToken c)
    {
        var encMessageDTO = new MessageDTO
        {
            KeyIdentifer = r.MessageDTO.KeyIdentifer,
            MessageId = r.MessageDTO.MessageId,
            Content = r.MessageDTO.Content,
        };

        if (await messageService.CreateMessage(encMessageDTO))
        {
            if (r.DeletionTime != default)
            {
                // Schedule the Quartz job to delete the message after 5 minutes
                var scheduler = await schedulerFactory.GetScheduler();
                var job = JobBuilder.Create<DeleteMessageJob>()
                                    .WithIdentity($"DeleteMessage-{r.MessageDTO.MessageId}", "MessageJobs")
                                    .UsingJobData("MessageId", r.MessageDTO.MessageId) // Pass the MessageId to the job
                                    .Build();
                var trigger = TriggerBuilder.Create()
                                    .WithIdentity($"DeleteMessageTrigger-{r.MessageDTO.MessageId}", "MessageTriggers")
                                    .StartAt(DateTimeOffset.UtcNow.AddMinutes(r.DeletionTime)) // Schedule to run after 5 minutes
                                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }

            await SendAsync(true);
        }
    }
}
