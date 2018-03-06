using System.Collections.Generic;

namespace DomainModeling.EventSubscription
{
    public interface ISubscriptionProvider
    {
        IEnumerable<SubscriptionDetail> GetSubscriptionDetails();
    }
}