using FastEndpoints;
using privatext.Common.Request;
using privatext.Common.Response;
using privatext.Services;

namespace privatext.Endpoints
{
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
        }

        public override async Task HandleAsync(GetMessageRequest r, CancellationToken c)
        {
            var res = new GetMessageResponse();
            var model = messageService.GetMessage(r.MessageId);
            if (model == null)
            {
                res.AddError("Message has been deleted");
                await SendAsync(res);
            }

            var decryptedMessage = await cryptoService.Decrypt(model.Content, r.MessageId);
            await SendAsync(new GetMessageResponse
            {
                MessageDTO = new Common.DTO.MessageDTO
                {
                    Content = decryptedMessage,
                    DateCreated = model.DateCreated,
                }
            });
        }
    }
}
