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

        }
    }
}
