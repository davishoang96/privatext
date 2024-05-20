using FastEndpoints;
using privatext.Common.Request;
using privatext.Services;

namespace privatext.Endpoints;

public class CreateMessageEndpoint : Endpoint<CreateMessageRequest, CreateMessageResponse>
{
    private readonly IMessageService messageService;
    public CreateMessageEndpoint(IMessageService messageService)
    {
        this.messageService = messageService;
    }

    public override void Configure()
    {
        Post("/createMessage/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateMessageRequest r, CancellationToken c)
    {
        if(await messageService.CreateMessage(r.MessageDTO))
        {
            await SendAsync(new CreateMessageResponse
            {
                MessageId = r.MessageDTO.MessageId,
            });
        }
    }
}
