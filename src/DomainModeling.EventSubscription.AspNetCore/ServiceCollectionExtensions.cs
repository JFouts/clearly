using DomainModeling.Core.Logging;
using DomainModeling.Core.Utilities;
using DomainModeling.Core.Utilities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DomainModeling.EventSubscription.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBasicSubscriptions(this IServiceCollection services)
        {
            return services
                .AddSingleton<IHostedService, EventSubscriptionService>()
                .AddSingleton(typeof(ILogger<>), typeof(MicrosoftLoggerWrapper<>))
                .AddSingleton<IJsonByteConverter, JsonByteConverter>()
                .AddSingleton<SubscriptionStatistics>()
                .AddSingleton<ISubscriptionStatistics>(x => x.GetRequiredService<SubscriptionStatistics>());
        }
    }
}
