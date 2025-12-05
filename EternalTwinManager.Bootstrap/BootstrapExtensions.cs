using EternalTwinManager.Bootstrap.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EternalTwinManager.Bootstrap;

public static class BootstrapExtensions
{
    public static IServiceCollection AddBootstrapDependencies(this IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        services.AddSingleton<IConfiguration>(configuration);

        services.AddLogging(builder =>
        {
            builder.AddDebug();
            builder.AddConsole();
        });

        services.ConfigureApplicationServices(configuration);
        services.ConfigurePersistenceServices(configuration);

        return services;
    }
}
