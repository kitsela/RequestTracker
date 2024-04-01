using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RequestTracker.API.Services;
using RequestTracker.Common.Models;

namespace RequestTracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackController : ControllerBase
    {
        private readonly IFileProvider _fileProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPublishEndpoint _publisher;

        public TrackController(IFileProvider fileProvider,
            IHttpContextAccessor httpContextAccessor,
            IPublishEndpoint publisher)
        {
            _fileProvider = fileProvider;
            _httpContextAccessor = httpContextAccessor;
            _publisher = publisher;
        }

        [HttpGet]
        public async Task<IActionResult> GetFile(CancellationToken cancellationToken)
        {
            var requestInfo = GetRequestInfo();

            await _publisher.Publish<RequestInfo>(requestInfo, cancellationToken);

            var trackImage = _fileProvider.TrackImgBytes;
            return File(trackImage, "image/gif");
        }

        private RequestInfo GetRequestInfo()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            return new RequestInfo
            {
                Referrer = httpContext.Request.Headers["Referer"].ToString(),
                UserAgent = httpContext.Request.Headers.UserAgent.ToString(),
                IpAddress = httpContext.Connection.RemoteIpAddress.ToString()
            };
        }
    }
}
