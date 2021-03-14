using System.Threading;
using System.Threading.Tasks;
using BerkeGaming.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace BerkeGaming.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserName ?? string.Empty;
            var userName = userId;

            _logger.LogInformation("Api Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);

            await Task.CompletedTask;
        }
    }
}
