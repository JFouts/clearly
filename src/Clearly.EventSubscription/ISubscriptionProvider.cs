using System.Collections.Generic;

namespace Clearly.EventSubscription
{
    public interface ISubscriptionProvider
    {
        IEnumerable<SubscriptionDetail> GetSubscriptionDetails();
    }
}