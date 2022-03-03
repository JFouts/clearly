using System.Net;
using Clearly.EventSubscription.AspNetCore;
using EventStore.ClientAPI;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace Clearly.EventSubscription.EventStore.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcCoreBuilder AddSubscriptions(this IMvcCoreBuilder builder)
        {
            builder.Services
                .AddBasicSubscriptions()
                .AddSingleton<ISubscriptionHost, EventStoreSubscriptionHost>()
                .AddSingleton<ISubscriptionProvider>(
                    x =>
                    {
                        var provider = new EventStoreAnnotationBasedSubscriptionProvider();
                        foreach (var applicationPart in builder.PartManager.ApplicationParts)
                        {
                            var part = (AssemblyPart) applicationPart;
                            if (part == null) continue;
                            provider.AddSubscriptionsFromAssembly(part.Assembly);
                        }

                        return provider;
                    })
                .AddSingleton(x => EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113)));

            return builder;
        }
    }
}