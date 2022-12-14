using Bira.App.Providers.Domain.Interfaces.Repositories;
using Bira.App.Providers.Infra.Repositories;
using Bira.App.Providers.Infra.Repositories.BaseContext;
using Bira.App.Providers.Service.Interfaces;
using Bira.App.Providers.Service.Notifications;
using Bira.App.Providers.Service.Services;

namespace Bira.App.Providers.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ApiDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
