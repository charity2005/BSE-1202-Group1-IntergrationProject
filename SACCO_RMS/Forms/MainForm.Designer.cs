namespace SACCO_RMS.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.pnlSidebar      = new System.Windows.Forms.Panel();
            this.lblAppName      = new System.Windows.Forms.Label();
            this.lblSacco        = new System.Windows.Forms.Label();
            this.btnNavDash      = new System.Windows.Forms.Button();
            this.btnNavMembers   = new System.Windows.Forms.Button();
            this.btnNavContrib   = new System.Windows.Forms.Button();
            this.btnNavLoans     = new System.Windows.Forms.Button();
            this.btnNavOverdue   = new System.Windows.Forms.Button();
            this.btnNavReport    = new System.Windows.Forms.Button();
            this.btnNavBackup    = new System.Windows.Forms.Button();
            this.btnLogout       = new System.Windows.Forms.Button();
            this.tabMain         = new System.Windows.Forms.TabControl();
            this.tabDashboard    = new System.Windows.Forms.TabPage();
            this.tabMembers      = new System.Windows.Forms.TabPage();
            this.tabContrib      = new System.Windows.Forms.TabPage();
            this.tabLoans        = new System.Windows.Forms.TabPage();
            this.tabOverdue      = new System.Windows.Forms.TabPage();
            this.tabReport       = new System.Windows.Forms.TabPage();
            this.tabBackup       = new System.Windows.Forms.TabPage();

            // Dashboard tab controls
            this.lblWelcome      = new System.Windows.Forms.Label();
            this.lblAlert        = new System.Windows.Forms.Label();
            this.pnlTile1        = new System.Windows.Forms.Panel();
            this.pnlTile2        = new System.Windows.Forms.Panel();
            this.pnlTile3        = new System.Windows.Forms.Panel();
            this.pnlTile4        = new System.Windows.Forms.Panel();
            this.lblMembers      = new System.Windows.Forms.Label();
            this.lblContrib      = new System.Windows.Forms.Label();
            this.lblLoans        = new System.Windows.Forms.Label();
            this.lblOverdue      = new System.Windows.Forms.Label();
            this.dgvRecent       = new System.Windows.Forms.DataGridView();

            this.pnlSidebar.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).BeginInit();
            this.SuspendLayout();

            // ── Sidebar ───────────────────────────────────────
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(31, 73, 125);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Width = 180;
            this.pnlSidebar.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblAppName, this.lblSacco,
                this.btnNavDash, this.btnNavMembers, this.btnNavContrib,
                this.btnNavLoans, this.btnNavOverdue, this.btnNavReport,
                this.btnNavBackup, this.btnLogout });

            this.lblAppName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblAppName.ForeColor = System.Drawing.Color.White;
            this.lblAppName.Location = new System.Drawing.Point(8, 10);
            this.lblAppName.Size = new System.Drawing.Size(164, 24);
            this.lblAppName.Text = "SACCO RMS";

            this.lblSacco.Font = new System.Drawing.Font("Arial", 7.5F);
            this.lblSacco.ForeColor = System.Drawing.Color.FromArgb(180, 210, 250);
            this.lblSacco.Location = new System.Drawing.Point(8, 34);
            this.lblSacco.Size = new System.Drawing.Size(164, 16);
            this.lblSacco.Text = "Rural Life Dev Mission";

            var navBtns = new[] { btnNavDash, btnNavMembers, btnNavContrib, btnNavLoans, btnNavOverdue, btnNavReport, btnNavBackup };
            var navText = new[] { "Dashboard", "Members", "Contributions", "Loans", "Overdue Loans", "Weekly Report", "Backup / Restore" };
            var navHandlers = new System.EventHandler[] {
                (s,e) => tabMain.SelectedTab = tabDashboard,
                btnNavMembers_Click, btnNavContrib_Click, btnNavLoans_Click,
                btnNavOverdue_Click, btnNavReport_Click, btnNavBackup_Click };
            for (int i = 0; i < navBtns.Length; i++)
            {
                navBtns[i].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                navBtns[i].ForeColor = System.Drawing.Color.White;
                navBtns[i].BackColor = System.Drawing.Color.Transparent;
                navBtns[i].Font = new System.Drawing.Font("Arial", 9F);
                navBtns[i].Location = new System.Drawing.Point(2, 62 + i * 38);
                navBtns[i].Size = new System.Drawing.Size(176, 34);
                navBtns[i].Text = navText[i];
                navBtns[i].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                navBtns[i].Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
                navBtns[i].FlatAppearance.BorderSize = 0;
                navBtns[i].Click += navHandlers[i];
            }

            this.btnLogout.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(255, 180, 180);
            this.btnLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnLogout.Font = new System.Drawing.Font("Arial", 9F);
            this.btnLogout.Location = new System.Drawing.Point(2, 590);
            this.btnLogout.Size = new System.Drawing.Size(176, 34);
            this.btnLogout.Text = "Logout";
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // ── TabControl ────────────────────────────────────
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Arial", 9F);
            this.tabMain.TabPages.AddRange(new System.Windows.Forms.TabPage[] {
                this.tabDashboard, this.tabMembers, this.tabContrib,
                this.tabLoans, this.tabOverdue, this.tabReport, this.tabBackup });
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);

            this.tabDashboard.Text = "Dashboard";
            this.tabMembers.Text   = "Members";
            this.tabContrib.Text   = "Contributions";
            this.tabLoans.Text     = "Loans";
            this.tabOverdue.Text   = "Overdue Loans";
            this.tabReport.Text    = "Weekly Report";
            this.tabBackup.Text    = "Backup";

            // ── Dashboard tab content ─────────────────────────
            this.lblWelcome.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(10, 8);
            this.lblWelcome.Size = new System.Drawing.Size(800, 26);
            this.lblWelcome.Text = "Welcome";

            this.lblAlert.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblAlert.Location = new System.Drawing.Point(10, 36);
            this.lblAlert.Size = new System.Drawing.Size(800, 22);
            this.lblAlert.Text = "";

            // Tiles
            var tileColors = new System.Drawing.Color[] {
                System.Drawing.Color.FromArgb(31,73,125),
                System.Drawing.Color.FromArgb(0,128,0),
                System.Drawing.Color.FromArgb(180,100,0),
                System.Drawing.Color.FromArgb(150,30,30) };
            var tilePanels = new[] { pnlTile1, pnlTile2, pnlTile3, pnlTile4 };
            var tileValues = new[] { lblMembers, lblContrib, lblLoans, lblOverdue };
            var tileLabels = new[] { "Total Members", "This Week's Contributions", "Active Loans", "Overdue Loans" };
            for (int i = 0; i < 4; i++)
            {
                tilePanels[i].BackColor = tileColors[i];
                tilePanels[i].Location = new System.Drawing.Point(10 + i * 205, 65);
                tilePanels[i].Size = new System.Drawing.Size(196, 80);
                var lTitle = new System.Windows.Forms.Label();
                lTitle.Font = new System.Drawing.Font("Arial", 8F);
                lTitle.ForeColor = System.Drawing.Color.FromArgb(210, 230, 255);
                lTitle.Location = new System.Drawing.Point(8, 8);
                lTitle.Size = new System.Drawing.Size(180, 18);
                lTitle.Text = tileLabels[i];
                tileValues[i].Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
                tileValues[i].ForeColor = System.Drawing.Color.White;
                tileValues[i].Location = new System.Drawing.Point(8, 30);
                tileValues[i].Size = new System.Drawing.Size(180, 34);
                tileValues[i].Text = "—";
                tilePanels[i].Controls.Add(lTitle);
                tilePanels[i].Controls.Add(tileValues[i]);
            }

            var lblRecent = new System.Windows.Forms.Label();
            lblRecent.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            lblRecent.Location = new System.Drawing.Point(10, 158);
            lblRecent.Size = new System.Drawing.Size(300, 22);
            lblRecent.Text = "Recent transactions this week:";

            // dgvRecent
            this.dgvRecent.AllowUserToAddRows = false;
            this.dgvRecent.AllowUserToDeleteRows = false;
            this.dgvRecent.ReadOnly = true;
            this.dgvRecent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecent.RowHeadersVisible = false;
            this.dgvRecent.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecent.Font = new System.Drawing.Font("Arial", 9F);
            this.dgvRecent.Location = new System.Drawing.Point(10, 184);
            this.dgvRecent.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.dgvRecent.Size = new System.Drawing.Size(820, 380);
            this.dgvRecent.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(31, 73, 125);
            this.dgvRecent.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvRecent.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);

            this.tabDashboard.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblWelcome, lblAlert, pnlTile1, pnlTile2, pnlTile3, pnlTile4, lblRecent, dgvRecent });
            this.tabDashboard.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.tabDashboard.Padding = new System.Windows.Forms.Padding(8);

            foreach (var tab in new[] { tabMembers, tabContrib, tabLoans, tabOverdue, tabReport, tabBackup })
            {
                tab.BackColor = System.Drawing.Color.White;
                tab.Padding = new System.Windows.Forms.Padding(8);
            }

            // Embed child forms into tabs
            this.tabMembers.Controls.Add(CreateEmbeddedForm(new MemberForm()));
            this.tabContrib.Controls.Add(CreateEmbeddedForm(new ContributionForm()));
            this.tabLoans.Controls.Add(CreateEmbeddedForm(new LoanForm()));
            this.tabOverdue.Controls.Add(CreateEmbeddedForm(new OverdueForm()));
            this.tabReport.Controls.Add(CreateEmbeddedForm(new ReportForm()));
            this.tabBackup.Controls.Add(CreateEmbeddedForm(new BackupForm()));

            // ── MainForm ──────────────────────────────────────
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1060, 680);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.pnlSidebar);
            this.MinimumSize = new System.Drawing.Size(1000, 680);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SACCO Records Management System — Ndejje University Group 1";

            this.pnlSidebar.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabDashboard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel CreateEmbeddedForm(System.Windows.Forms.Form child)
        {
            child.TopLevel = false;
            child.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            child.Dock = System.Windows.Forms.DockStyle.Fill;
            child.Visible = true;
            var panel = new System.Windows.Forms.Panel();
            panel.Dock = System.Windows.Forms.DockStyle.Fill;
            panel.Controls.Add(child);
            return panel;
        }

        private System.Windows.Forms.Panel    pnlSidebar;
        private System.Windows.Forms.Label    lblAppName;
        private System.Windows.Forms.Label    lblSacco;
        private System.Windows.Forms.Button   btnNavDash;
        private System.Windows.Forms.Button   btnNavMembers;
        private System.Windows.Forms.Button   btnNavContrib;
        private System.Windows.Forms.Button   btnNavLoans;
        private System.Windows.Forms.Button   btnNavOverdue;
        private System.Windows.Forms.Button   btnNavReport;
        private System.Windows.Forms.Button   btnNavBackup;
        private System.Windows.Forms.Button   btnLogout;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage  tabDashboard;
        private System.Windows.Forms.TabPage  tabMembers;
        private System.Windows.Forms.TabPage  tabContrib;
        private System.Windows.Forms.TabPage  tabLoans;
        private System.Windows.Forms.TabPage  tabOverdue;
        private System.Windows.Forms.TabPage  tabReport;
        private System.Windows.Forms.TabPage  tabBackup;
        private System.Windows.Forms.Label    lblWelcome;
        private System.Windows.Forms.Label    lblAlert;
        private System.Windows.Forms.Panel    pnlTile1;
        private System.Windows.Forms.Panel    pnlTile2;
        private System.Windows.Forms.Panel    pnlTile3;
        private System.Windows.Forms.Panel    pnlTile4;
        private System.Windows.Forms.Label    lblMembers;
        private System.Windows.Forms.Label    lblContrib;
        private System.Windows.Forms.Label    lblLoans;
        private System.Windows.Forms.Label    lblOverdue;
        private System.Windows.Forms.DataGridView dgvRecent;
    }
}
