using FastEndpoints;
using privatext.Client.HttpClient;
using privatext.Services;

namespace privatext.Endpoints;

public class CreateMessageEndpoint : Endpoint<CreateMessageRequest, bool>
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
        var encMessageDTO = new Common.DTO.MessageDTO
        {
            MessageId = r.MessageDTO.MessageId,
            Content = r.MessageDTO.Content,
        };

        if (await messageService.CreateMessage(encMessageDTO))
        {
            await SendAsync(true);
        }
    }
}
