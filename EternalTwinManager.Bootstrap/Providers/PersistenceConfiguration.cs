using EternalTwinManager.Core.Interfaces.Brutes.Apis;
using EternalTwinManager.Core.Interfaces.Brutes.Ports;
using EternalTwinManager.Core.Interfaces.Shared;
using EternalTwinManager.Core.Interfaces.Shared.Ports;
using EternalTwinManager.Core.Options;
using EternalTwinManager.Infraestructure.ApiClients;
using EternalTwinManager.Infraestructure.Authentication.Brute;
using EternalTwinManager.Infraestructure.Authentication.Shared;
using EternalTwinManager.Infraestructure.Http;
using EternalTwinManager.Infrastructure.Workflows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EternalTwinManager.Bootstrap.Providers;

public static class PersistenceConfiguration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiSettingsOptions>(
            configuration.GetSection(ApiSettingsOptions.Api));

        services.AddTransient<IEternalHttpClientFactory, EternalHttpClientFactory>();
        services.AddTransient<IBruteApiClient, BruteApiClient>();
        services.AddTransient<IBruteLoginClient, BruteLoginClient>();
        services.AddTransient<IBruteOAuthClient, BruteOAuthClient>();
        services.AddTransient<IBruteTokenParser, BruteTokenParser>();
        services.AddTransient<IPasswordEncoder, PasswordEncoder>();
        services.AddTransient<IHeaderConfigurator, HeaderConfigurator>();
        services.AddTransient<IBruteAuthenticator, BruteAuthenticator>();
        services.AddTransient<IGameWorkflow, GameWorkflow>();

        return services;
    }
}
