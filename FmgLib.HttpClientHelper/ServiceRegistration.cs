using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace FmgLib.HttpClientHelper;

public static class ServiceRegistration
{
    public static IServiceCollection AddFmgLibHttpClient(this IServiceCollection services, JsonSerializerOptions options = null)
    {
        services.AddHttpClient();
        SerializerSettings.Settings = options;

        return services;
    }
}
