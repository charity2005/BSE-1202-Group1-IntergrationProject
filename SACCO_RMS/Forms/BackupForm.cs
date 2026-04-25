using System;
using System.IO;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public partial class BackupForm : Form
    {
        private string _restoreFilePath = string.Empty;

        public BackupForm() { InitializeComponent(); RefreshDrives(); }

        private void RefreshDrives()
        {
            cmbDrives.Items.Clear();
            foreach (var drive in DriveInfo.GetDrives())
                if (drive.DriveType == DriveType.Removable && drive.IsReady)
                    cmbDrives.Items.Add($"{drive.Name}  ({drive.AvailableFreeSpace / 1048576} MB free)");
            if (cmbDrives.Items.Count == 0)
                cmbDrives.Items.Add("— No USB drives detected —");
            cmbDrives.SelectedIndex = 0;
        }

        private void btnRefreshDrives_Click(object sender, EventArgs e) { RefreshDrives(); }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (cmbDrives.SelectedItem == null || cmbDrives.SelectedItem.ToString().StartsWith("—"))
            { ShowStatus("No USB drive detected. Insert a drive and click Refresh.", true); return; }
            try
            {
                string driveLetter = cmbDrives.SelectedItem.ToString().Substring(0, 3).Trim();
                DatabaseHelper.BackupToPath(driveLetter);
                string fn = $"sacco_backup_{DateTime.Today:yyyy-MM-dd}.db";
                ShowStatus($"Backup successful: {driveLetter}{fn}", false);
                lstHistory.Items.Insert(0, $"{DateTime.Now:dd MMM yyyy HH:mm}  —  {fn}  —  {driveLetter}  —  SUCCESS");
            }
            catch (Exception ex) { ShowStatus($"Backup failed: {ex.Message}", true); }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog { Title = "Select SACCO Backup File", Filter = "SQLite DB|*.db|All Files|*.*" };
            if (dlg.ShowDialog() == DialogResult.OK)
            { _restoreFilePath = dlg.FileName; ShowStatus($"File selected: {Path.GetFileName(_restoreFilePath)}", false); }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_restoreFilePath) || !File.Exists(_restoreFilePath))
            { ShowStatus("Please browse and select a backup file first.", true); return; }
            if (MessageBox.Show("This will OVERWRITE all current data. Continue?", "Confirm Restore",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                DatabaseHelper.RestoreFromFile(_restoreFilePath);
                ShowStatus("Database restored. Please restart the application.", false);
                lstHistory.Items.Insert(0, $"{DateTime.Now:dd MMM yyyy HH:mm}  —  RESTORED from {Path.GetFileName(_restoreFilePath)}");
            }
            catch (Exception ex) { ShowStatus($"Restore failed: {ex.Message}", true); }
        }

        private void ShowStatus(string msg, bool error)
        {
            lblStatus.Text = msg;
            lblStatus.ForeColor = error ? System.Drawing.Color.Red : System.Drawing.Color.Green;
        }
    }
}
