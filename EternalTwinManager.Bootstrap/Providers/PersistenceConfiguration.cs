using EternalTwinManager.Core.Brute.Interfaces.Apis;
using EternalTwinManager.Core.Brute.Interfaces.Services;
using EternalTwinManager.Core.Options;
using EternalTwinManager.Core.Shared.Interfaces.Services;
using EternalTwinManager.Infraestructure.ApiClients;
using EternalTwinManager.Infraestructure.Authentication.Brute;
using EternalTwinManager.Infraestructure.Authentication.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Headers;

namespace EternalTwinManager.Bootstrap.Providers;

public static class PersistenceConfiguration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new CookieContainer());
        services.Configure<ApiSettingsOptions>(
            configuration.GetSection(ApiSettingsOptions.Api));
        services.AddTransient<IBruteApiClient, BruteApiClient>();
        services.AddTransient<IBruteLoginClient, BruteLoginClient>();
        services.AddTransient<IBruteOAuthClient, BruteOAuthClient>();
        services.AddTransient<IBruteTokenParser, BruteTokenParser>();
        services.AddTransient<IPasswordEncoder, PasswordEncoder>();
        services.AddTransient<IHeaderConfigurator, HeaderConfigurator>();
        services.AddTransient<IBruteAuthenticator, BruteAuthenticator>();
        services.AddHttpClient("BruteClient")
            .ConfigureHttpClient((sp, client) =>
            {
                var apiSettings = sp.GetRequiredService<IOptions<ApiSettingsOptions>>().Value;

                client.BaseAddress = new Uri(apiSettings.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);

                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            })
            .ConfigurePrimaryHttpMessageHandler(sp =>
            {
                var cookieContainer = sp.GetRequiredService<CookieContainer>();
                return new HttpClientHandler
                {
                    CookieContainer = cookieContainer,
                    UseCookies = true,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };
            });
        services.AddHttpClient("BruteNoRedirect")
            .ConfigureHttpClient((sp, client) =>
            {
                var apiSettings = sp.GetRequiredService<IOptions<ApiSettingsOptions>>().Value;

                client.BaseAddress = new Uri(apiSettings.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            })
            .ConfigurePrimaryHttpMessageHandler(sp =>
            {
                var cookieContainer = sp.GetRequiredService<CookieContainer>();
                return new HttpClientHandler
                {
                    CookieContainer = cookieContainer,
                    UseCookies = true,
                    AllowAutoRedirect = false,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };
            });
        return services;
    }
}
