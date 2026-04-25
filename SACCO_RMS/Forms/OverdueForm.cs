using System;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public partial class OverdueForm : Form
    {
        public OverdueForm() { InitializeComponent(); LoadOverdue(); }

        private void LoadOverdue()
        {
            var dt = DatabaseHelper.GetOverdueLoans();
            dgvOverdue.DataSource = dt;
            if (dt.Rows.Count == 0)
            { lblSummary.Text = "No overdue loans — all borrowers are current."; lblSummary.ForeColor = System.Drawing.Color.Green; }
            else
            { lblSummary.Text = $"{dt.Rows.Count} loan(s) overdue as of today."; lblSummary.ForeColor = System.Drawing.Color.Red; }
        }

        private void btnRefresh_Click(object sender, EventArgs e) { LoadOverdue(); }
    }
}
