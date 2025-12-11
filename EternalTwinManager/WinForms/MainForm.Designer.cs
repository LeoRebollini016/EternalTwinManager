
namespace EternalTwinManager.WinForms;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        lblAccounts = new Label();
        txtAccountsPath = new TextBox();
        btnRun = new Button();
        btnBrowse = new Button();
        lblLog = new Label();
        fileSystemWatcher1 = new FileSystemWatcher();
        dgvAccounts = new DataGridView();
        panel1 = new Panel();
        PanelTop = new Panel();
        Id = new DataGridViewTextBoxColumn();
        AccountName = new DataGridViewTextBoxColumn();
        CreatureName = new DataGridViewTextBoxColumn();
        State = new DataGridViewTextBoxColumn();
        Message = new DataGridViewTextBoxColumn();
        LastUpdate = new DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvAccounts).BeginInit();
        panel1.SuspendLayout();
        PanelTop.SuspendLayout();
        SuspendLayout();
        // 
        // lblAccounts
        // 
        lblAccounts.AutoSize = true;
        lblAccounts.Location = new Point(9, 20);
        lblAccounts.Name = "lblAccounts";
        lblAccounts.Size = new Size(108, 15);
        lblAccounts.TabIndex = 0;
        lblAccounts.Text = "Archivo de cuentas";
        lblAccounts.Click += label1_Click;
        // 
        // txtAccountsPath
        // 
        txtAccountsPath.Location = new Point(12, 33);
        txtAccountsPath.Name = "txtAccountsPath";
        txtAccountsPath.ReadOnly = true;
        txtAccountsPath.Size = new Size(320, 23);
        txtAccountsPath.TabIndex = 1;
        // 
        // btnRun
        // 
        btnRun.Location = new Point(9, 74);
        btnRun.Name = "btnRun";
        btnRun.Size = new Size(108, 23);
        btnRun.TabIndex = 2;
        btnRun.Text = "Iniciar tareas";
        btnRun.UseVisualStyleBackColor = true;
        btnRun.Click += btnRun_Click;
        // 
        // btnBrowse
        // 
        btnBrowse.Cursor = Cursors.Hand;
        btnBrowse.Location = new Point(338, 33);
        btnBrowse.Name = "btnBrowse";
        btnBrowse.Size = new Size(100, 23);
        btnBrowse.TabIndex = 3;
        btnBrowse.Text = "Examinar";
        btnBrowse.UseVisualStyleBackColor = true;
        btnBrowse.Click += btnBrowse_Click;
        // 
        // lblLog
        // 
        lblLog.AutoSize = true;
        lblLog.Location = new Point(9, 122);
        lblLog.Name = "lblLog";
        lblLog.Size = new Size(124, 15);
        lblLog.TabIndex = 5;
        lblLog.Text = "Seguimiento de tareas";
        lblLog.Click += label1_Click_1;
        // 
        // fileSystemWatcher1
        // 
        fileSystemWatcher1.EnableRaisingEvents = true;
        fileSystemWatcher1.SynchronizingObject = this;
        // 
        // dgvAccounts
        // 
        dgvAccounts.AllowUserToAddRows = false;
        dgvAccounts.AllowUserToDeleteRows = false;
        dgvAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvAccounts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
        dgvAccounts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvAccounts.Columns.AddRange(new DataGridViewColumn[] { Id, AccountName, CreatureName, State, Message, LastUpdate });
        dgvAccounts.Dock = DockStyle.Fill;
        dgvAccounts.Location = new Point(5, 5);
        dgvAccounts.Name = "dgvAccounts";
        dgvAccounts.ReadOnly = true;
        dgvAccounts.RowHeadersVisible = false;
        dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvAccounts.Size = new Size(752, 204);
        dgvAccounts.TabIndex = 6;
        dgvAccounts.CellContentClick += dgvAccounts_CellContentClick;
        // 
        // panel1
        // 
        panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panel1.AutoSize = true;
        panel1.Controls.Add(dgvAccounts);
        panel1.Location = new Point(12, 208);
        panel1.Name = "panel1";
        panel1.Padding = new Padding(5);
        panel1.Size = new Size(762, 214);
        panel1.TabIndex = 7;
        // 
        // PanelTop
        // 
        PanelTop.Controls.Add(btnBrowse);
        PanelTop.Controls.Add(lblLog);
        PanelTop.Controls.Add(lblAccounts);
        PanelTop.Controls.Add(txtAccountsPath);
        PanelTop.Controls.Add(btnRun);
        PanelTop.Dock = DockStyle.Top;
        PanelTop.Location = new Point(0, 0);
        PanelTop.Name = "PanelTop";
        PanelTop.Padding = new Padding(5);
        PanelTop.Size = new Size(800, 190);
        PanelTop.TabIndex = 8;
        // 
        // Id
        // 
        Id.FillWeight = 6.92115736F;
        Id.HeaderText = "ID";
        Id.Name = "Id";
        Id.ReadOnly = true;
        // 
        // AccountName
        // 
        AccountName.FillWeight = 25F;
        AccountName.HeaderText = "Cuenta";
        AccountName.Name = "AccountName";
        AccountName.ReadOnly = true;
        // 
        // CreatureName
        // 
        CreatureName.FillWeight = 27.68463F;
        CreatureName.HeaderText = "Nombre";
        CreatureName.Name = "CreatureName";
        CreatureName.ReadOnly = true;
        // 
        // State
        // 
        State.FillWeight = 30F;
        State.HeaderText = "Estado";
        State.Name = "State";
        State.ReadOnly = true;
        // 
        // Message
        // 
        Message.FillWeight = 55.21157F;
        Message.HeaderText = "Mensaje";
        Message.Name = "Message";
        Message.ReadOnly = true;
        // 
        // LastUpdate
        // 
        LastUpdate.FillWeight = 17F;
        LastUpdate.HeaderText = "Última actualización";
        LastUpdate.Name = "LastUpdate";
        LastUpdate.ReadOnly = true;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(panel1);
        Controls.Add(PanelTop);
        Name = "MainForm";
        Text = "Eternal Twin Manager";
        Load += MainForm_Load;
        ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvAccounts).EndInit();
        panel1.ResumeLayout(false);
        PanelTop.ResumeLayout(false);
        PanelTop.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private void btnAgregar_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    #endregion

    private Label lblAccounts;
    private TextBox txtAccountsPath;
    private Button btnRun;
    private Button btnBrowse;
    private Label lblLog;
    private FileSystemWatcher fileSystemWatcher1;
    private DataGridView dgvAccounts;
    private Panel panel1;
    private Panel PanelTop;
    private DataGridViewTextBoxColumn Id;
    private DataGridViewTextBoxColumn AccountName;
    private DataGridViewTextBoxColumn CreatureName;
    private DataGridViewTextBoxColumn State;
    private DataGridViewTextBoxColumn Message;
    private DataGridViewTextBoxColumn LastUpdate;
}
