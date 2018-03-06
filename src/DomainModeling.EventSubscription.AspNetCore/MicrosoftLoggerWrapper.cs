using System;
using Microsoft.Extensions.Logging;

namespace DomainModeling.EventSubscription.AspNetCore
{
    public class MicrosoftLoggerWrapper<T> : Core.Logging.ILogger<T>
    {
        private readonly ILogger<T> _logger;

        public MicrosoftLoggerWrapper(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        public void LogError(Exception exception, string message)
        {
            _logger.LogError(exception, message);
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }
    }
}