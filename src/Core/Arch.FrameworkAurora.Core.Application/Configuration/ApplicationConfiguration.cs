using Microsoft.Extensions.DependencyInjection;

namespace Arch.FrameworkAurora.Core.Application.Configuration
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
        {
            // todo: add use cases injections
            return services;
        }
    }
}