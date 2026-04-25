using System;
using System.Data;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public partial class MemberForm : Form
    {
        public MemberForm() { InitializeComponent(); LoadMembers(); }

        private void LoadMembers()
        {
            dgvMembers.DataSource = DatabaseHelper.GetAllMembers();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var dt = DatabaseHelper.SearchMembers(txtSearch.Text.Trim());
            dgvMembers.DataSource = dt;
            lblCount.Text = dt.Rows.Count == 0 ? "No results found." : $"{dt.Rows.Count} member(s) found.";
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearch.Clear(); LoadMembers();
            lblCount.Text = $"{DatabaseHelper.GetAllMembers().Rows.Count} member(s).";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblStatus.Text = string.Empty;
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtNID.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) || string.IsNullOrWhiteSpace(txtKin.Text) ||
                nudShare.Value == 0)
            { lblStatus.ForeColor = System.Drawing.Color.Red; lblStatus.Text = "All five fields are required (FR-01)."; return; }

            if (DatabaseHelper.NationalIDExists(txtNID.Text.Trim().ToUpper()))
            { lblStatus.ForeColor = System.Drawing.Color.Red; lblStatus.Text = "Duplicate National ID — this member already exists (FR-02)."; return; }

            bool ok = DatabaseHelper.RegisterMember(txtName.Text.Trim(), txtNID.Text.Trim().ToUpper(),
                txtPhone.Text.Trim(), txtKin.Text.Trim(), (int)nudShare.Value);

            if (ok)
            {
                lblStatus.ForeColor = System.Drawing.Color.Green;
                lblStatus.Text = $"Member \"{txtName.Text.Trim()}\" registered successfully.";
                txtName.Clear(); txtNID.Clear(); txtPhone.Clear(); txtKin.Clear(); nudShare.Value = 50000;
                LoadMembers();
            }
        }
    }
}
