using Microsoft.Extensions.DependencyInjection;

namespace Clearly.EventSourcing.AspNetCore
{
    public static class ServiceCollcetionExtensions
    {
        public static IServiceCollection AddEventSourcing(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEventSourcedAggregateRepository<>), typeof(AggregateRepository<>));
            services.AddScoped(typeof(IEventDispatcher<>), typeof(EventDispatcher<>));
            services.AddScoped(typeof(IEventHandlerFactory<>), typeof(EventHandlerFactory<>));

            return services;
        }
    }
}