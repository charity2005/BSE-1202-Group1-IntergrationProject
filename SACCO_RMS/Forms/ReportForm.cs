using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public partial class ReportForm : Form
    {
        public ReportForm() { InitializeComponent(); dtpWeek.Value = DateTime.Today; }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var selected  = dtpWeek.Value.Date;
            int offset    = (int)selected.DayOfWeek - (int)DayOfWeek.Monday;
            if (offset < 0) offset += 7;
            var ws = selected.AddDays(-offset);
            var we = ws.AddDays(6);

            lblPeriod.Text = $"Week: {ws:dd MMM yyyy}  to  {we:dd MMM yyyy}";

            var summary = DatabaseHelper.GetWeeklySummary(ws, we);
            if (summary.Rows.Count > 0)
            {
                lblTC.Text = $"UGX {Convert.ToInt32(summary.Rows[0]["TotalContributions"]):N0}";
                lblTD.Text = $"UGX {Convert.ToInt32(summary.Rows[0]["TotalDisbursed"]):N0}";
                lblTR.Text = $"UGX {Convert.ToInt32(summary.Rows[0]["TotalRepayments"]):N0}";
                int bal = Convert.ToInt32(summary.Rows[0]["TotalContributions"]) +
                          Convert.ToInt32(summary.Rows[0]["TotalRepayments"]) -
                          Convert.ToInt32(summary.Rows[0]["TotalDisbursed"]);
                lblBal.Text = $"UGX {bal:N0}";
            }

            dgvTx.DataSource = DatabaseHelper.GetWeeklyTransactions(ws, we);
            pnlTotals.Visible = true;
        }
    }
}
