using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Codidact.Application.Common.Behaviors
{
    /// <summary>
    /// Logs every command that is sent to the application
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;

        public RequestLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation($"Codidact Request: {requestName} {request}");
            return Task.CompletedTask;
        }
    }
}
