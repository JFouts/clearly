using Microsoft.Extensions.DependencyInjection;

namespace DomainModeling.EventRepository.LocalMemory.AspNetCore
{
    public static class EventStoreMvcCoreBuilderExtensions
    {
        public static IMvcCoreBuilder AddLocalMemory(this IMvcCoreBuilder builder)
        {
            RegisterServices(builder);
            return builder;
        }

        private static void RegisterServices(IMvcCoreBuilder builder)
        {
            builder.Services.AddSingleton<IEventRepository, LocalMemoryRepository>();
        }
    }
}