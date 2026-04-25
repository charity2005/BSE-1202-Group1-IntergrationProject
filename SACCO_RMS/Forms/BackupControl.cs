// ============================================================
//  BackupControl.cs
//  SACCO Records Management System
//  Charity – NFR-05: USB backup export & restore
//            Max 5 steps, completable by treasurer without
//            technical assistance
// ============================================================
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public class BackupControl : UserControl
    {
        private ComboBox cmbDrives;
        private Label    lblBackupStatus;
        private ListBox  lstHistory;

        public BackupControl()
        {
            InitialiseComponent();
            RefreshDrives();
        }

        private void InitialiseComponent()
        {
            BackColor = Color.White;
            Padding   = new Padding(10);

            Controls.Add(new Label { Text = "Backup & Restore", Font = new Font("Arial", 14, FontStyle.Bold), AutoSize = true, Top = 0, Left = 0 });
            Controls.Add(new Label { Text = "NFR-05: Full database backup to USB. Completable in under 5 minutes.", ForeColor = Color.Gray, Font = new Font("Arial", 9), AutoSize = true, Top = 32, Left = 0 });

            // ── EXPORT PANEL ─────────────────────────────────
            var grpExport = new GroupBox { Text = "STEP-BY-STEP EXPORT (5 steps)", Font = new Font("Arial", 10, FontStyle.Bold), Left = 0, Top = 60, Width = 460, Height = 280 };

            AddStep(grpExport, 1, "Connect your USB drive to the computer.", 28);
            AddStep(grpExport, 2, "Click 'Refresh Drives' to detect it.",   60);

            var btnRefresh = new Button { Text = "Refresh Drives", Width = 120, Height = 26, Left = 30, Top = 90, FlatStyle = FlatStyle.Flat };
            btnRefresh.Click += (s, e) => RefreshDrives();
            grpExport.Controls.Add(btnRefresh);

            AddStep(grpExport, 3, "Select the USB drive below.", 122);
            grpExport.Controls.Add(new Label { Text = "Drive:", Font = new Font("Arial", 9), AutoSize = true, Left = 30, Top = 148 });
            cmbDrives = new ComboBox { Width = 200, Left = 80, Top = 146, Font = new Font("Arial", 9), DropDownStyle = ComboBoxStyle.DropDownList };
            grpExport.Controls.Add(cmbDrives);

            AddStep(grpExport, 4, "Click 'Export Backup Now'.", 172);
            var btnExport = new Button { Text = "Export Backup Now", Width = 160, Height = 32, Left = 30, Top = 198, BackColor = Color.FromArgb(31,73,125), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Arial", 10, FontStyle.Bold) };
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Click += BtnExport_Click;
            grpExport.Controls.Add(btnExport);

            AddStep(grpExport, 5, "Confirm the success message, then remove USB.", 240);
            Controls.Add(grpExport);

            // ── STATUS label ─────────────────────────────────
            lblBackupStatus = new Label { Text = string.Empty, AutoSize = false, Width = 460, Height = 24, Left = 0, Top = 348, Font = new Font("Arial", 9, FontStyle.Bold) };
            Controls.Add(lblBackupStatus);

            // ── RESTORE PANEL ────────────────────────────────
            var grpRestore = new GroupBox { Text = "RESTORE FROM BACKUP", Font = new Font("Arial", 10, FontStyle.Bold), Left = 490, Top = 60, Width = 380, Height = 170 };

            var warn = new Label { Text = "WARNING: Restoring will overwrite all current data.\nOnly use if data has been lost or corrupted.", ForeColor = Color.FromArgb(150,0,0), Font = new Font("Arial", 8.5f), AutoSize = false, Width = 350, Height = 40, Left = 10, Top = 24 };
            grpRestore.Controls.Add(warn);

            grpRestore.Controls.Add(new Label { Text = "Select backup file (.db):", Font = new Font("Arial", 9), AutoSize = true, Left = 10, Top = 74 });
            var btnBrowse = new Button { Text = "Browse USB...", Width = 130, Height = 26, Left = 10, Top = 96, FlatStyle = FlatStyle.Flat };
            btnBrowse.Click += BtnBrowse_Click;
            grpRestore.Controls.Add(btnBrowse);

            var btnRestore = new Button { Text = "Restore Database", Width = 140, Height = 30, Left = 10, Top = 130, BackColor = Color.FromArgb(150,0,0), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Arial", 9, FontStyle.Bold), Tag = "" };
            btnRestore.FlatAppearance.BorderSize = 0;
            btnRestore.Click += BtnRestore_Click;
            grpRestore.Controls.Add(btnRestore);

            Controls.Add(grpRestore);

            // ── BACKUP HISTORY ────────────────────────────────
            Controls.Add(new Label { Text = "Backup History", Font = new Font("Arial", 10, FontStyle.Bold), AutoSize = true, Left = 0, Top = 382 });
            lstHistory = new ListBox { Left = 0, Top = 404, Width = 860, Height = 180, Font = new Font("Arial", 9), BorderStyle = BorderStyle.FixedSingle };
            lstHistory.Items.Add("No backups recorded this session.");
            Controls.Add(lstHistory);
        }

        private void AddStep(GroupBox parent, int num, string text, int top)
        {
            parent.Controls.Add(new Label
            {
                Text      = $"Step {num}:  {text}",
                Font      = new Font("Arial", 9),
                ForeColor = num == 5 ? Color.FromArgb(0,100,0) : Color.Black,
                AutoSize  = false,
                Width     = 420,
                Height    = 18,
                Left      = 10,
                Top       = top
            });
        }

        private void RefreshDrives()
        {
            cmbDrives.Items.Clear();
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Removable && drive.IsReady)
                    cmbDrives.Items.Add($"{drive.Name}  ({drive.AvailableFreeSpace / 1048576} MB free)");
            }
            if (cmbDrives.Items.Count == 0)
                cmbDrives.Items.Add("— No USB drives detected —");
            cmbDrives.SelectedIndex = 0;
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (cmbDrives.SelectedItem == null || cmbDrives.SelectedItem.ToString().StartsWith("—"))
            {
                ShowStatus("No USB drive detected. Please insert a drive and click Refresh Drives.", error: true);
                return;
            }

            try
            {
                // Extract drive letter (first 3 chars e.g. "C:\")
                string driveLetter = cmbDrives.SelectedItem.ToString().Substring(0, 3).Trim();
                DatabaseHelper.BackupToPath(driveLetter);
                string filename = $"sacco_backup_{DateTime.Today:yyyy-MM-dd}.db";
                ShowStatus($"Backup successful: {driveLetter}{filename}", error: false);

                if (lstHistory.Items[0].ToString() == "No backups recorded this session.")
                    lstHistory.Items.Clear();
                lstHistory.Items.Insert(0, $"{DateTime.Now:dd MMM yyyy HH:mm}  –  {filename}  –  Drive {driveLetter}  –  SUCCESS");
            }
            catch (Exception ex)
            {
                ShowStatus($"Backup failed: {ex.Message}", error: true);
                lstHistory.Items.Insert(0, $"{DateTime.Now:dd MMM yyyy HH:mm}  –  FAILED: {ex.Message}");
            }
        }

        private string _restoreFilePath = string.Empty;

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title  = "Select SACCO Backup File",
                Filter = "SQLite Database|*.db|All Files|*.*",
                InitialDirectory = @"E:\"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _restoreFilePath = dlg.FileName;
                ShowStatus($"File selected: {Path.GetFileName(_restoreFilePath)}", error: false);
            }
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_restoreFilePath) || !File.Exists(_restoreFilePath))
            {
                ShowStatus("Please browse and select a valid backup file first.", error: true);
                return;
            }

            var confirm = MessageBox.Show(
                "This will OVERWRITE all current data with the selected backup.\n\nAre you absolutely sure?",
                "Confirm Restore",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (confirm != DialogResult.Yes) return;

            try
            {
                DatabaseHelper.RestoreFromFile(_restoreFilePath);
                ShowStatus("Database restored successfully. Please restart the application.", error: false);
                lstHistory.Items.Insert(0, $"{DateTime.Now:dd MMM yyyy HH:mm}  –  RESTORED from {Path.GetFileName(_restoreFilePath)}");
            }
            catch (Exception ex)
            {
                ShowStatus($"Restore failed: {ex.Message}", error: true);
            }
        }

        private void ShowStatus(string msg, bool error)
        {
            lblBackupStatus.Text      = msg;
            lblBackupStatus.ForeColor = error ? Color.Red : Color.FromArgb(0, 128, 0);
        }
    }
}
