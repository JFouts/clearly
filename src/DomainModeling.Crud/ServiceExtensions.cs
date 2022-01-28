using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DomainModeling.Crud {
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCrud(this IServiceCollection services)
        {
            return services;
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCrud(this IApplicationBuilder app)
        {
            return app;
        }
    }
}