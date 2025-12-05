using EternalTwinManager.Application.Brutes.Interfaces.Services;
using EternalTwinManager.Application.Interfaces.Services;
using EternalTwinManager.Application.Orchestrators;
using EternalTwinManager.Application.Orchestrators.Pipelines;
using EternalTwinManager.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EternalTwinManager.Bootstrap.Providers;

public static class ApplicationServiceConfiguration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IBruteIntelligenceService, BruteIntelligenceService>();
        services.AddTransient<IBruteAutomationService, BruteAutomationService>();
        services.AddTransient<AutoFightPipeline>();
        services.AddTransient<DailyTasksOrchestrator>();
        services.AddTransient<MultiAccountOrchestrator>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("EternalTwinManager.Application")));
        return services;
    }
}