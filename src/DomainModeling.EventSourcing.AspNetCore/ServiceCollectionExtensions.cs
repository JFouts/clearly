using DomainModeling.Core;
using Microsoft.Extensions.DependencyInjection;

namespace DomainModeling.EventSourcing.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IAggregateBuilder<TAggregate> AddAggregate<TAggregate>(this IServiceCollection services) where TAggregate : AggregateRoot, new()
        {
            var builder = new AggregateBuilder<TAggregate>(services);
            return builder.AddAggregate();
        }
    }
}