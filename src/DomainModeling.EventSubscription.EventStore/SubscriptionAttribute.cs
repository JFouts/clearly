using System;

namespace DomainModeling.EventSubscription.EventStore
{
    public class SubscriptionAttribute : Attribute
    {
        public string StreamName { get; }

        public SubscriptionAttribute(string streamName)
        {
            StreamName = streamName;
        }
    }
}