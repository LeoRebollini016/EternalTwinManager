using EternalTwinManager.Core.Events;
using EternalTwinManager.WinForms.Controllers;
using EternalTwinManager.WinForms.ErrorHandling;
using Microsoft.Extensions.DependencyInjection;

namespace EternalTwinManager.WinForms.Providers;

public static class WinFormsInjection
{
    public static IServiceCollection AddWinFormsInjection(this IServiceCollection services)
    {
        services.AddSingleton<GlobalErrorHandler>();
        services.AddSingleton<MultiAccountController>();
        services.AddTransient<MainForm>();
        services.AddSingleton<IProgress<AccountProgressUpdate>>(sp =>
        {
            return new Progress<AccountProgressUpdate>(update =>
            {
                var form = sp.GetRequiredService<MainForm>();
                form.UpdateUI(update);
            });
        });
        return services;
    }
}
