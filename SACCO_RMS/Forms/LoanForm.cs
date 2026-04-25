using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public partial class LoanForm : Form
    {
        private int _activeLoanId;
        public LoanForm() { InitializeComponent(); LoadMembers(); LoadLoans(); dtpStart.Value = DateTime.Today; dtpRepay.Value = DateTime.Today; }

        private void LoadMembers()
        {
            cmbMember.Items.Clear();
            cmbMember.Items.Add(new ComboItem(0, "— Select member —"));
            foreach (DataRow r in DatabaseHelper.GetAllMembers().Rows)
                cmbMember.Items.Add(new ComboItem(Convert.ToInt32(r["MemberID"]), r["FullName"].ToString()));
            cmbMember.SelectedIndex = 0;
        }

        private void LoadLoans() { dgvLoans.DataSource = DatabaseHelper.GetActiveLoans(); }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            lblCalcStatus.Text = string.Empty;
            if (!(cmbMember.SelectedItem is ComboItem item) || item.Id == 0 || string.IsNullOrWhiteSpace(txtPurpose.Text))
            { lblCalcStatus.ForeColor = Color.Red; lblCalcStatus.Text = "All 6 fields required (FR-06)."; return; }

            int principal = (int)nudPrincipal.Value;
            double rate   = (double)nudRate.Value;
            int weeks     = (int)nudWeeks.Value;
            int total     = (int)Math.Round(principal * (1 + rate / 100.0));
            int inst      = (int)Math.Round((double)total / weeks);

            lblScheduleSummary.Text = $"Principal: UGX {principal:N0}   |   Total Repayable: UGX {total:N0}   |   Weekly Instalment: UGX {inst:N0}";

            var dt = new DataTable();
            dt.Columns.Add("Week"); dt.Columns.Add("Due Date"); dt.Columns.Add("Instalment (UGX)"); dt.Columns.Add("Balance After (UGX)");
            int bal = total; var due = dtpStart.Value.Date;
            for (int w = 1; w <= weeks; w++)
            {
                bal -= inst; if (bal < 0) bal = 0;
                dt.Rows.Add(w, due.ToString("dd/MM/yyyy"), $"{inst:N0}", $"{bal:N0}");
                due = due.AddDays(7);
            }
            dgvSchedule.DataSource = dt;
            grpSchedule.Visible = true;
            btnConfirm.Enabled  = true;
            lblCalcStatus.ForeColor = Color.Green;
            lblCalcStatus.Text = "Schedule ready — review above then click Confirm.";
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!(cmbMember.SelectedItem is ComboItem item) || item.Id == 0) return;
            DatabaseHelper.CreateLoan(item.Id, (int)nudPrincipal.Value, (double)nudRate.Value,
                (int)nudWeeks.Value, dtpStart.Value.Date, txtPurpose.Text.Trim());
            lblCalcStatus.ForeColor = Color.Green;
            lblCalcStatus.Text = $"Loan disbursed to {item.Name}.";
            grpSchedule.Visible = false; btnConfirm.Enabled = false;
            cmbMember.SelectedIndex = 0; txtPurpose.Clear();
            LoadLoans();
        }

        private void dgvLoans_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoans.SelectedRows.Count == 0) return;
            _activeLoanId = Convert.ToInt32(dgvLoans.SelectedRows[0].Cells["LoanID"].Value);
            string name = dgvLoans.SelectedRows[0].Cells["Member"].Value?.ToString() ?? "";
            grpRepay.Text = $"Record Repayment  —  {name}";
            grpRepay.Visible = true;
        }

        private void btnRepay_Click(object sender, EventArgs e)
        {
            DatabaseHelper.RecordRepayment(_activeLoanId, (int)nudRepay.Value, dtpRepay.Value.Date);
            lblRepayStatus.ForeColor = Color.Green;
            lblRepayStatus.Text = $"Repayment of UGX {nudRepay.Value:N0} recorded.";
            LoadLoans();
        }
    }
}
