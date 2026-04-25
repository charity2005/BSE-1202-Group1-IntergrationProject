using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            var today  = DateTime.Today;
            int offset = (int)today.DayOfWeek - (int)DayOfWeek.Monday;
            if (offset < 0) offset += 7;
            var weekStart = today.AddDays(-offset);
            var weekEnd   = weekStart.AddDays(6);

            int members  = DatabaseHelper.GetAllMembers().Rows.Count;
            int overdue  = DatabaseHelper.GetOverdueLoans().Rows.Count;
            int loans    = DatabaseHelper.GetActiveLoans().Rows.Count;
            var summary  = DatabaseHelper.GetWeeklySummary(weekStart, weekEnd);
            int contrib  = summary.Rows.Count > 0 ? Convert.ToInt32(summary.Rows[0]["TotalContributions"]) : 0;

            lblMembers.Text  = members.ToString();
            lblContrib.Text  = $"UGX {contrib:N0}";
            lblLoans.Text    = loans.ToString();
            lblOverdue.Text  = overdue.ToString();
            lblOverdue.ForeColor = overdue > 0 ? Color.Red : Color.FromArgb(0,128,0);

            lblWelcome.Text = $"Welcome, {Session.Username}  ({Session.Role})   —   {DateTime.Now:dddd, dd MMMM yyyy}";

            if (overdue > 0)
                lblAlert.Text = $"⚠  {overdue} loan(s) overdue this week — check the Overdue Loans tab.";
            else
                lblAlert.Text = "✔  All loan repayments are current.";
            lblAlert.ForeColor = overdue > 0 ? Color.DarkRed : Color.DarkGreen;

            var txData = DatabaseHelper.GetWeeklyTransactions(weekStart, weekEnd);
            dgvRecent.DataSource = txData;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            new LoginForm().Show();
            Close();
        }

        // ── Tab navigation buttons ────────────────────────────
        private void btnNavMembers_Click(object sender, EventArgs e)    => tabMain.SelectedTab = tabMembers;
        private void btnNavContrib_Click(object sender, EventArgs e)    => tabMain.SelectedTab = tabContrib;
        private void btnNavLoans_Click(object sender, EventArgs e)      => tabMain.SelectedTab = tabLoans;
        private void btnNavOverdue_Click(object sender, EventArgs e)    => tabMain.SelectedTab = tabOverdue;
        private void btnNavReport_Click(object sender, EventArgs e)     => tabMain.SelectedTab = tabReport;
        private void btnNavBackup_Click(object sender, EventArgs e)     => tabMain.SelectedTab = tabBackup;

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabDashboard) LoadDashboard();
        }
    }
}
