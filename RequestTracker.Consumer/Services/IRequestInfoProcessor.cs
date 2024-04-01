using RequestTracker.Common.Models;

namespace RequestTracker.Consumer.Services
{
    public interface IRequestInfoProcessor
    {
        void Process(RequestInfo requestInfo);
    }
}