namespace Clearly.EventSubscription;

public interface ISubscriptionProvider
{
    IEnumerable<SubscriptionDetail> GetSubscriptionDetails();
}
