using System;

namespace Clearly.EventSubscription
{
    public interface ISubscriptionStatistics
    {
        long NumberOfActiveSubscriptions { get; }
        long NumberOfEventsProcessed { get; }
        DateTime LastProcessesTime { get; }
        DateTime StartedTime { get; }
    }
}