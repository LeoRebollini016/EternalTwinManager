using EternalTwinManager.Core.Dtos.Brutes.Shared;
using EternalTwinManager.Core.Enums;
using EternalTwinManager.Core.Events;
using EternalTwinManager.WinForms.Controllers;
using EternalTwinManager.WinForms.ViewModels;

namespace EternalTwinManager.WinForms;

public partial class MainForm : Form
{
    private readonly MultiAccountController _controller;
    private IProgress<AccountProgressUpdate> _progress;
    private string? _accountsFilePath;
    private List<AccountProcessStatusVM> _loadedRows = new();
    private List<LoginDto> _loadedAccounts = new();

    public MainForm(MultiAccountController controller)
    {
        InitializeComponent();
        _controller = controller;
        _progress = new Progress<AccountProgressUpdate>(update =>
        {
            UpdateUI(update);
        });
    }

    private async void btnRun_Click(object sender, EventArgs e)
    {
        if (_loadedAccounts == null || _loadedAccounts.Count == 0)
        {
            MessageBox.Show("Por favor, seleccione un archivo de cuentas.");
            return;
        }
        btnRun.Enabled = false;

        var tuples = _loadedAccounts
            .Select(a => (user: a.user, password: a.password))
            .ToList();

        await _controller.RunAsync(tuples, _progress, CancellationToken.None);

        _loadedAccounts.Clear();
        btnRun.Enabled = true;
    }

    private void MainForm_Load(object sender, EventArgs e)
    {

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void label1_Click_1(object sender, EventArgs e)
    {

    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog();
        dialog.Filter = "JSON files (.json)|*.json";

        if (dialog.ShowDialog() == DialogResult.OK)
        {

            _accountsFilePath = dialog.FileName;
            txtAccountsPath.Text = _accountsFilePath;
            var accounts = _controller.LoadAccounts(_accountsFilePath);
            _loadedAccounts = accounts;

            _loadedRows = accounts.Select((acc, i) =>
                new AccountProcessStatusVM(
                    i + 1,
                    acc.user,
                    "",
                    AccountProcessStateEnum.Pending,
                    "",
                    DateTime.Now
                )).ToList();
        }
        FillGrid(_loadedRows);
    }

    private void FillGrid(List<AccountProcessStatusVM> loadedRows)
    {
        dgvAccounts.Rows.Clear();

        foreach (var row in loadedRows)
        {
            dgvAccounts.Rows.Add(
                row.Id,
                row.AccountName,
                row.CreatureName,
                row.State.ToString(),
                row.ErrorMessage,
                row.LastUpdate.ToString("HH:mm:ss")
            );
        }
    }

    protected internal void UpdateUI(AccountProgressUpdate update)
    {
        var row = dgvAccounts.Rows
            .Cast<DataGridViewRow>()
            .FirstOrDefault(r => (string)r.Cells["AccountName"].Value == update.AccountName);

        if (row == null)
            return;

        row.Cells["CreatureName"].Value = update.CreatureName ?? "";
        row.Cells["State"].Value = update.State.ToString();
        row.Cells["Message"].Value = update.Message;
        row.Cells["LastUpdate"].Value = DateTime.Now.ToString("HH:mm:ss");
    }

    private void dgvAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}
