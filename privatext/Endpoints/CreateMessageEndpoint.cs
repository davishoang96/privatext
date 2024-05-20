using FastEndpoints;
using privatext.Client.HttpClient;
using privatext.Services;

namespace privatext.Endpoints;

public class CreateMessageEndpoint : Endpoint<CreateMessageRequest, bool>
{
    private readonly ICryptoService cryptoService;
    private readonly IMessageService messageService;
    public CreateMessageEndpoint(IMessageService messageService, ICryptoService cryptoService)
    {
        this.messageService = messageService;
        this.cryptoService = cryptoService;
    }

    public override void Configure()
    {
        Post("/createMessage/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateMessageRequest r, CancellationToken c)
    {
        var messageContent = await cryptoService.Encrypt(r.MessageDTO.Content, r.MessageDTO.MessageId);
        var messageId = r.MessageDTO.MessageId;
        var midpoint = messageId.Length / 2;
        var secondHalf = messageId.Substring(midpoint);
        var encMessageDTO = new Common.DTO.MessageDTO
        {
            MessageId = secondHalf,
            Content = messageContent,
        };

        if (await messageService.CreateMessage(encMessageDTO))
        {
            await SendAsync(true);
        }
    }
}
