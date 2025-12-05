using EternalTwinManager.WinForms.ErrorHandling;
using Microsoft.Extensions.DependencyInjection;

namespace EternalTwinManager.WinForms.Providers;

public static class WinFormsInjection
{
    public static IServiceCollection AddWinFormsInjection(this IServiceCollection services)
    {
        services.AddSingleton<GlobalErrorHandler>();
        services.AddTransient<MainForm>();
        return services;
    }
}
