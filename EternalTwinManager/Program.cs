using EternalTwinManager.WinForms.ErrorHandling;
using EternalTwinManager.WinForms.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace EternalTwinManager
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var provider = ApplicationBuilderExtensions.ConfigureAppServices();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            var errorHandler = provider.GetRequiredService<GlobalErrorHandler>();

            ApplicationConfiguration.Initialize();



            System.Windows.Forms.Application.ThreadException += (sender, args) =>
            {
                errorHandler.Handle(args.Exception);
            };

            var mainForm = provider.GetRequiredService<MainForm>();
            System.Windows.Forms.Application.Run(mainForm);
        }
    }
}