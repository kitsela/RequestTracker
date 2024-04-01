using MassTransit;
using RequestTracker.Consumer.Services;
using System.Text.Json;

namespace RequestTracker.Consumer.Consumers
{
    public class RequestInfo : IConsumer<Common.Models.RequestInfo>
    {
        private readonly IRequestInfoProcessor _requestInfoProcessor;

        //todo add logger
        public RequestInfo(IRequestInfoProcessor requestInfoProcessor)
        {
            _requestInfoProcessor = requestInfoProcessor;
        }

        public async Task Consume(ConsumeContext<Common.Models.RequestInfo> context)
        {
            _requestInfoProcessor.Process(context.Message);
        }
    }
}
