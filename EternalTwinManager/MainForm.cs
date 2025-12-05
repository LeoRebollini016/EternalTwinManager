using EternalTwinManager.Application.Orchestrators;

namespace EternalTwinManager;

public partial class MainForm : Form
{
    private readonly MultiAccountOrchestrator _orchestrator;
    public MainForm(MultiAccountOrchestrator orchestrator)
    {
        InitializeComponent();
        _orchestrator = orchestrator;
    }

    private async void btnRun_Click(object sender, EventArgs e)
    {
        btnRun.Enabled = false;
        txtLog.AppendText("Iniciando tareas... \r\n");

        var accounts = new List<(string user, string password)>()
        {
            (txtUser.Text.Trim(), txtPassword.Text.Trim())
        };
        await _orchestrator.RunAsync(accounts, CancellationToken.None);

        txtLog.AppendText("Finalizado.\r\n");

        btnRun.Enabled = true;
    }

    private void MainForm_Load(object sender, EventArgs e)
    {

    }
}
