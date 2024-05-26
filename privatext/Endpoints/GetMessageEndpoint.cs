using FastEndpoints;
using FluentValidation.Results;
using privatext.Common.Request;
using privatext.Common.Response;
using privatext.Services;

namespace privatext.Endpoints;

public class GetMessageEndpoint : Endpoint<GetMessageRequest, GetMessageResponse>
{
    private readonly ICryptoService cryptoService;
    private readonly IMessageService messageService;
    public GetMessageEndpoint(IMessageService messageService, ICryptoService cryptoService)
    {
        this.messageService = messageService;
        this.cryptoService = cryptoService;
    }

    public override void Configure()
    {
        Post("/getMessage/");
        AllowAnonymous();
        DontCatchExceptions();
    }

    public override async Task HandleAsync(GetMessageRequest r, CancellationToken c)
    {
        var res = new GetMessageResponse();
        var midpoint = r.MessageId.Length / 2;
        var secondHalf = r.MessageId.Substring(midpoint);
        var model = messageService.GetMessage(secondHalf);
        if (model == null)
        {
            ThrowError(new ValidationFailure
            {
                ErrorMessage = $"Message id = {r.MessageId} has been deleted",
                Severity = FluentValidation.Severity.Error,
                PropertyName = nameof(GetMessageEndpoint),
            });
        }

        if (!await messageService.DeleteMessage(secondHalf))
        {
            ThrowError(new ValidationFailure
            {
                ErrorMessage = "Error when deleting a message",
                Severity = FluentValidation.Severity.Error,
                PropertyName = nameof(GetMessageEndpoint),
            });
        }

        var decryptedMessage = await cryptoService.Decrypt(model.Content, r.MessageId);
        res.Content = decryptedMessage;
        res.DateCreated = model.DateCreated;
        await SendAsync(res);
    }
}
