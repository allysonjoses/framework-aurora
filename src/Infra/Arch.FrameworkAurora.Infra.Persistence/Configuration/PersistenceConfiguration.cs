using Microsoft.Extensions.DependencyInjection;

namespace Arch.FrameworkAurora.Infra.Persistence.Configuration
{
    public static class PersistenceConfiguration
    {
        public static IServiceCollection AddPersistenceConfiguration(this IServiceCollection services)
        {
            // todo: add AddDbContext
            // todo: add dependency injection

            return services;
        }
    }
}