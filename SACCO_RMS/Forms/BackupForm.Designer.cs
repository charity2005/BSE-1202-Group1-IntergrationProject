namespace SACCO_RMS.Forms
{
    partial class BackupForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblTitle        = new System.Windows.Forms.Label();
            this.grpExport       = new System.Windows.Forms.GroupBox();
            this.lblStep1        = new System.Windows.Forms.Label();
            this.lblStep2        = new System.Windows.Forms.Label();
            this.btnRefreshDrives= new System.Windows.Forms.Button();
            this.lblStep3        = new System.Windows.Forms.Label();
            this.cmbDrives       = new System.Windows.Forms.ComboBox();
            this.lblStep4        = new System.Windows.Forms.Label();
            this.btnExport       = new System.Windows.Forms.Button();
            this.lblStep5        = new System.Windows.Forms.Label();
            this.grpRestore      = new System.Windows.Forms.GroupBox();
            this.lblWarn         = new System.Windows.Forms.Label();
            this.lblSelectFile   = new System.Windows.Forms.Label();
            this.btnBrowse       = new System.Windows.Forms.Button();
            this.btnRestore      = new System.Windows.Forms.Button();
            this.lblStatus       = new System.Windows.Forms.Label();
            this.grpHistory      = new System.Windows.Forms.GroupBox();
            this.lstHistory      = new System.Windows.Forms.ListBox();
            this.grpExport.SuspendLayout();
            this.grpRestore.SuspendLayout();
            this.grpHistory.SuspendLayout();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 6);
            this.lblTitle.Size = new System.Drawing.Size(500, 28);
            this.lblTitle.Text = "Backup & Restore  (NFR-05)";

            // grpExport
            this.grpExport.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grpExport.Location = new System.Drawing.Point(10, 40);
            this.grpExport.Size = new System.Drawing.Size(420, 230);
            this.grpExport.Text = "Export Backup to USB  (5 steps)";
            this.grpExport.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblStep1, lblStep2, btnRefreshDrives, lblStep3, cmbDrives,
                lblStep4, btnExport, lblStep5 });

            int sy = 22, sg = 36;
            System.Windows.Forms.Label[] steps = { lblStep1, lblStep2, lblStep3, lblStep4, lblStep5 };
            string[] stepTexts = {
                "Step 1:  Connect your USB drive to the computer.",
                "Step 2:  Click Refresh Drives to detect it.",
                "Step 3:  Select the USB drive from the list.",
                "Step 4:  Click Export Backup Now.",
                "Step 5:  Confirm success message, then remove USB." };
            for (int i = 0; i < 5; i++)
            {
                steps[i].Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular);
                steps[i].Location = new System.Drawing.Point(10, sy + i * sg);
                steps[i].Size = new System.Drawing.Size(395, 18);
                steps[i].Text = stepTexts[i];
                if (i == 4) steps[i].ForeColor = System.Drawing.Color.Green;
            }

            this.btnRefreshDrives.Font = new System.Drawing.Font("Arial", 8.5F);
            this.btnRefreshDrives.Location = new System.Drawing.Point(10, sy + sg + 20);
            this.btnRefreshDrives.Size = new System.Drawing.Size(110, 24);
            this.btnRefreshDrives.Text = "Refresh Drives";
            this.btnRefreshDrives.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshDrives.Click += new System.EventHandler(this.btnRefreshDrives_Click);

            this.cmbDrives.Font = new System.Drawing.Font("Arial", 9F);
            this.cmbDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrives.Location = new System.Drawing.Point(10, sy + 2 * sg + 20);
            this.cmbDrives.Size = new System.Drawing.Size(280, 24);

            this.btnExport.BackColor = System.Drawing.Color.FromArgb(31, 73, 125);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnExport.Location = new System.Drawing.Point(10, sy + 3 * sg + 20);
            this.btnExport.Size = new System.Drawing.Size(180, 32);
            this.btnExport.Text = "Export Backup Now";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

            // grpRestore
            this.grpRestore.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grpRestore.Location = new System.Drawing.Point(440, 40);
            this.grpRestore.Size = new System.Drawing.Size(360, 180);
            this.grpRestore.Text = "Restore from Backup";
            this.grpRestore.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblWarn, lblSelectFile, btnBrowse, btnRestore });

            this.lblWarn.Font = new System.Drawing.Font("Arial", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblWarn.ForeColor = System.Drawing.Color.DarkRed;
            this.lblWarn.Location = new System.Drawing.Point(10, 22);
            this.lblWarn.Size = new System.Drawing.Size(335, 40);
            this.lblWarn.Text = "WARNING: Restoring will overwrite ALL current data.\r\nOnly use if data has been lost or corrupted.";

            this.lblSelectFile.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular);
            this.lblSelectFile.Location = new System.Drawing.Point(10, 72);
            this.lblSelectFile.Size = new System.Drawing.Size(335, 18);
            this.lblSelectFile.Text = "Select backup file from USB drive:";

            this.btnBrowse.Font = new System.Drawing.Font("Arial", 9F);
            this.btnBrowse.Location = new System.Drawing.Point(10, 96);
            this.btnBrowse.Size = new System.Drawing.Size(130, 26);
            this.btnBrowse.Text = "Browse USB...";
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);

            this.btnRestore.BackColor = System.Drawing.Color.FromArgb(150, 30, 30);
            this.btnRestore.ForeColor = System.Drawing.Color.White;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnRestore.Location = new System.Drawing.Point(10, 132);
            this.btnRestore.Size = new System.Drawing.Size(150, 30);
            this.btnRestore.Text = "Restore Database";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);

            // lblStatus
            this.lblStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(10, 280);
            this.lblStatus.Size = new System.Drawing.Size(790, 22);
            this.lblStatus.Text = "";

            // grpHistory
            this.grpHistory.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grpHistory.Location = new System.Drawing.Point(10, 308);
            this.grpHistory.Size = new System.Drawing.Size(840, 240);
            this.grpHistory.Text = "Backup History";
            this.grpHistory.Controls.Add(this.lstHistory);

            this.lstHistory.Font = new System.Drawing.Font("Arial", 9F);
            this.lstHistory.Location = new System.Drawing.Point(8, 22);
            this.lstHistory.Size = new System.Drawing.Size(820, 208);
            this.lstHistory.Items.Add("07 Apr 2026  17:32  —  sacco_backup_2026-04-07.db  —  E:\\  —  SUCCESS");
            this.lstHistory.Items.Add("31 Mar 2026  17:10  —  sacco_backup_2026-03-31.db  —  E:\\  —  SUCCESS");
            this.lstHistory.Items.Add("24 Mar 2026  18:05  —  sacco_backup_2026-03-24.db  —  F:\\  —  SUCCESS");

            // BackupForm
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 562);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTitle, grpExport, grpRestore, lblStatus, grpHistory });
            this.Name = "BackupForm";
            this.Text = "Backup & Restore";
            this.grpExport.ResumeLayout(false);
            this.grpRestore.ResumeLayout(false);
            this.grpHistory.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label     lblTitle;
        private System.Windows.Forms.GroupBox  grpExport, grpRestore, grpHistory;
        private System.Windows.Forms.Label     lblStep1, lblStep2, lblStep3, lblStep4, lblStep5;
        private System.Windows.Forms.Button    btnRefreshDrives, btnExport;
        private System.Windows.Forms.ComboBox  cmbDrives;
        private System.Windows.Forms.Label     lblWarn, lblSelectFile;
        private System.Windows.Forms.Button    btnBrowse, btnRestore;
        private System.Windows.Forms.Label     lblStatus;
        private System.Windows.Forms.ListBox   lstHistory;
    }
}
