using Microsoft.Extensions.Logging;

namespace EternalTwinManager.WinForms.ErrorHandling;

public class GlobalErrorHandler(ILogger<GlobalErrorHandler> logger)
{
    private readonly ILogger<GlobalErrorHandler> _logger = logger;

    public void Handle(Exception ex)
    {
        _logger.LogError(ex, "An unhandled exception occurred.");

        MessageBox.Show(
            ex.Message,
            "Error inesperado",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
    }
}
