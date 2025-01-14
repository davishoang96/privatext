using FastEndpoints;
using privatext.Common.Request;
using privatext.Services;
using FluentValidation.Results;

namespace BlazorPrivatext.Endpoints;

public class DeleteMessageEndpoint : Endpoint<DeleteMessageRequest, bool>
{
    private readonly IMessageService messageService;
    
    public DeleteMessageEndpoint(IMessageService messageService)
    {
        this.messageService = messageService;
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