using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public partial class ContributionForm : Form
    {
        public ContributionForm() { InitializeComponent(); LoadMembers(); dtpDate.Value = DateTime.Today; }

        private void LoadMembers()
        {
            cmbMember.Items.Clear();
            cmbMember.Items.Add(new ComboItem(0, "— Select member —"));
            foreach (DataRow r in DatabaseHelper.GetAllMembers().Rows)
                cmbMember.Items.Add(new ComboItem(Convert.ToInt32(r["MemberID"]), r["FullName"].ToString()));
            cmbMember.SelectedIndex = 0;

            cmbHistory.Items.Clear();
            cmbHistory.Items.Add(new ComboItem(0, "— Select member —"));
            foreach (DataRow r in DatabaseHelper.GetAllMembers().Rows)
                cmbHistory.Items.Add(new ComboItem(Convert.ToInt32(r["MemberID"]), r["FullName"].ToString()));
            cmbHistory.SelectedIndex = 0;
        }

        private void cmbMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMember.SelectedItem is ComboItem item && item.Id > 0)
            {
                int bal = DatabaseHelper.GetMemberBalance(item.Id);
                lblBalance.Text = $"Current balance:  UGX {bal:N0}";
            }
            else lblBalance.Text = "Current balance:  —";
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            lblStatus.Text = string.Empty;
            if (!(cmbMember.SelectedItem is ComboItem item) || item.Id == 0)
            { lblStatus.ForeColor = Color.Red; lblStatus.Text = "Please select a member."; return; }

            DatabaseHelper.RecordContribution(item.Id, (int)nudAmount.Value, dtpDate.Value.Date);
            int newBal = DatabaseHelper.GetMemberBalance(item.Id);
            lblBalance.Text = $"Current balance:  UGX {newBal:N0}";

            // Show receipt in groupbox
            grpReceipt.Visible = true;
            lblRMember.Text  = $"Member:      {item.Name}";
            lblRDate.Text    = $"Date:           {dtpDate.Value:dd/MM/yyyy}";
            lblRAmount.Text  = $"Amount:      UGX {nudAmount.Value:N0}";
            lblRBalance.Text = $"New Balance: UGX {newBal:N0}";

            lblStatus.ForeColor = Color.Green;
            lblStatus.Text = "Contribution recorded successfully.";
        }

        private void cmbHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(cmbHistory.SelectedItem is ComboItem item) || item.Id == 0)
            { dgvHistory.DataSource = null; return; }

            var raw = DatabaseHelper.GetContributionHistory(item.Id);
            var dt  = new DataTable();
            dt.Columns.Add("Date"); dt.Columns.Add("Amount (UGX)"); dt.Columns.Add("Running Balance (UGX)");
            int balance = DatabaseHelper.GetMemberBalance(item.Id);
            foreach (DataRow row in raw.Rows)
            {
                dt.Rows.Add(row["Date"], $"{Convert.ToInt32(row["Amount (UGX)"]):N0}", $"{balance:N0}");
                balance -= Convert.ToInt32(row["Amount (UGX)"]);
            }
            dgvHistory.DataSource = dt;
        }
    }

    public class ComboItem
    {
        public int Id; public string Name;
        public ComboItem(int id, string name) { Id = id; Name = name; }
        public override string ToString() => Name;
    }
}
