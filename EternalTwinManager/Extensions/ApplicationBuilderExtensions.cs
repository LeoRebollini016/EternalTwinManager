using EternalTwinManager.Bootstrap;
using EternalTwinManager.WinForms.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace EternalTwinManager.WinForms.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IServiceProvider ConfigureAppServices()
    {
        var services = new ServiceCollection();
        services.AddBootstrapDependencies();
        services.AddWinFormsInjection();

        return services.BuildServiceProvider();
    }
}
