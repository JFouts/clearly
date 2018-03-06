using System;
using System.Net;
using DomainModeling.Core.Utilities;
using DomainModeling.Core.Utilities.Interfaces;
using DomainModeling.EventSourcing.AspNetCore;
using EventStore.ClientAPI;
using Microsoft.Extensions.DependencyInjection;

namespace DomainModeling.EventRepository.EventStore.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventStore(this IServiceCollection services)
        {
            return services.AddEventStore(ApplyDefaultSettings);
        }

        public static IServiceCollection AddEventStore(this IServiceCollection services, Action<EventStoreSettings> setupAction)
        {
            RegisterServices(services, setupAction);
            return services;
        }

        private static void ApplyDefaultSettings(EventStoreSettings settings)
        {
            settings.NamingPreferences = NamingConvention.Literal;
            settings.EndPoint = new IPEndPoint(IPAddress.Loopback, 1113);
        }

        private static void RegisterServices(IServiceCollection services, Action<EventStoreSettings> setupAction)
        {
            services.AddEventSourcing();
            services.AddSingleton(x => CreateCustomSettings(setupAction));
            services.AddSingleton<EventStoreRepository>();
            services.AddSingleton<IEventRepository>(x => x.GetRequiredService<EventStoreRepository>());

            services.AddSingleton(x => EventStoreConnection.Create(CreateCustomSettings(setupAction).EndPoint));
            // TODO: All the variations of these
            services.AddSingleton<IJsonByteConverter, JsonByteConverter>();
            services.AddSingleton<IBinaryStringConverter, Utf8BinaryStringConverter>();
            services.AddSingleton<IJsonConverter, NewtonsoftJsonConverter>();
        }

        private static EventStoreSettings CreateCustomSettings(Action<EventStoreSettings> setupAction)
        {
            var settings = new EventStoreSettings();
            setupAction(settings);
            return settings;
        }
    }
}