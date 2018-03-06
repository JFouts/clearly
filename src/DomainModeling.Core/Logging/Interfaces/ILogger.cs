using System;

namespace DomainModeling.Core.Logging
{
    public interface ILogger<T>
    {
        void LogWarning(string message);
        void LogError(Exception exception, string message);
        void LogInformation(string message);
    }
}
