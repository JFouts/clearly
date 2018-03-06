using System;
using System.Threading;

namespace DomainModeling.EventSubscription.AspNetCore
{
    public class SubscriptionStatistics : ISubscriptionStatistics
    {
        private long _numberOfEventProcessed;

        public long NumberOfActiveSubscriptions { get; set; }
        public long NumberOfEventsProcessed => _numberOfEventProcessed;
        public DateTime LastProcessesTime { get; set; }
        public DateTime StartedTime { get; set; }

        public void IncrementEventsProcessed()
        {
            Interlocked.Increment(ref _numberOfEventProcessed);
        }
    }
}