using Microsoft.Extensions.DependencyInjection;

namespace FmgLib.HttpClientHelper;

public static class ServiceRegistration
{
    public static IServiceCollection AddFmgLibHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient();

        return services;
    }
}
