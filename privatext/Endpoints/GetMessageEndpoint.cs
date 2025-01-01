using FastEndpoints;
using FluentValidation.Results;
using privatext.Common.Request;
using privatext.Common.Response;
using privatext.Services;

namespace privatext.Endpoints;

public class GetMessageEndpoint : Endpoint<GetMessageRequest, GetMessageResponse>
{
    private readonly IMessageService messageService;
    public GetMessageEndpoint(IMessageService messageService)
    {
        this.messageService = messageService;
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
        var model = messageService.GetMessage(r.MessageId);
        if (model == null)
        {
            ThrowError(new ValidationFailure
            {
                ErrorMessage = $"Message id = {r.MessageId} has been deleted",
                Severity = FluentValidation.Severity.Error,
                PropertyName = nameof(GetMessageEndpoint),
            });
        }

        if (!await messageService.DeleteMessage(r.MessageId))
        {
            ThrowError(new ValidationFailure
            {
                ErrorMessage = "Error when deleting a message",
                Severity = FluentValidation.Severity.Error,
                PropertyName = nameof(GetMessageEndpoint),
            });
        }

        res.Content = model.Content;
        res.DateCreated = model.DateCreated;
        await SendAsync(res);
    }
}
