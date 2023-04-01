using Clearly.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Clearly.EventSubscription.AspNetCore
{
    public class EventSubscriptionService : BackgroundService
    {
        private readonly ISubscriptionHost _subscriptionHost;
        private readonly ISubscriptionProvider _subscriptionProvider;
        private readonly IServiceProvider _services;
        private readonly SubscriptionStatistics _statistics;

        public EventSubscriptionService(ISubscriptionHost subscriptionHost, ISubscriptionProvider subscriptionProvider, IServiceProvider services, SubscriptionStatistics statistics)
        {
            _subscriptionHost = subscriptionHost;
            _subscriptionProvider = subscriptionProvider;
            _services = services;
            _statistics = statistics;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _statistics.StartedTime = DateTime.UtcNow;
            await _subscriptionHost.StartAsync();
            AddSubscriptions();
        }

        private void AddSubscriptions()
        {
            foreach (var subscription in _subscriptionProvider.GetSubscriptionDetails())
            {
                // TODO: In C#11 we can make Stream required so that we don't have to ! it here.
                _subscriptionHost.Subscribe(subscription.Stream!, x => InvokeEventHandler(subscription, x));
                _statistics.NumberOfActiveSubscriptions++;
            }
        }

        private async Task InvokeEventHandler(SubscriptionDetail subscription, IDomainEvent @event)
        {
            if (!subscription.EventHandlers.ContainsKey(@event.GetType()))
                return;

            using (var scope = _services.CreateScope())
            {
                var handler = subscription.EventHandlers[@event.GetType()];
                // TODO: In C#11 we can make Type required so that we don't have to ! it here.
                var instance = scope.ServiceProvider.GetRequiredService(subscription.Type!);
                _statistics.LastProcessesTime = DateTime.UtcNow;

                try
                {
                    await handler(instance, @event);
                }
                finally
                {
                    _statistics.IncrementEventsProcessed();
                }
            }
        }

        public override void Dispose()
        {
            ((IDisposable)_subscriptionHost).Dispose();
            base.Dispose();
        }
    }
}