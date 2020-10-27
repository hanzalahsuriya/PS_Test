using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Paymentsense.Coding.Challenge.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSettings<T>(this IServiceCollection services, IConfiguration configuration, string configKeyName) where T : class, new()
        {
            services.Configure<T>(configuration.GetSection(configKeyName));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<T>>().Value);
            return services;
        }
    }
}