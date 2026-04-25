// ============================================================
//  ContribHistoryControl.cs
//  SACCO Records Management System
//  Jonathan – FR-05: Full contribution history per member
//             Shows date, amount, running balance (most recent first)
// ============================================================
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public class ContribHistoryControl : UserControl
    {
        private ComboBox     cmbMember;
        private DataGridView grid;
        private Label        lblInfo;

        public ContribHistoryControl()
        {
            InitialiseComponent();
            LoadMembers();
        }

        private void InitialiseComponent()
        {
            BackColor = Color.White;
            Padding   = new Padding(10);

            Controls.Add(new Label { Text = "Contribution History", Font = new Font("Arial", 14, FontStyle.Bold), AutoSize = true, Top = 0, Left = 0 });
            Controls.Add(new Label { Text = "FR-05: Full payment history per member, most recent first.", ForeColor = Color.Gray, Font = new Font("Arial", 9), AutoSize = true, Top = 32, Left = 0 });

            Controls.Add(new Label { Text = "Select Member:", Font = new Font("Arial", 9), AutoSize = true, Left = 0, Top = 66 });
            cmbMember = new ComboBox { Width = 300, Left = 130, Top = 63, Font = new Font("Arial", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbMember.SelectedIndexChanged += (s, e) => LoadHistory();
            Controls.Add(cmbMember);

            lblInfo = new Label { Text = string.Empty, ForeColor = Color.FromArgb(0,100,0), Font = new Font("Arial", 9, FontStyle.Italic), AutoSize = true, Left = 0, Top = 100 };
            Controls.Add(lblInfo);

            grid = MainDashboard.MakeGrid();
            grid.Top    = 122;
            grid.Left   = 0;
            grid.Width  = 700;
            grid.Height = 480;
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            Controls.Add(grid);
        }

        private void LoadMembers()
        {
            cmbMember.Items.Clear();
            cmbMember.Items.Add(new ComboItem(0, "— Select a member —"));
            var dt = DatabaseHelper.GetAllMembers();
            foreach (DataRow row in dt.Rows)
                cmbMember.Items.Add(new ComboItem(Convert.ToInt32(row["MemberID"]), row["FullName"].ToString()));
            cmbMember.SelectedIndex = 0;
        }

        private void LoadHistory()
        {
            if (!(cmbMember.SelectedItem is ComboItem item) || item.Id == 0)
            {
                grid.DataSource = null;
                lblInfo.Text    = string.Empty;
                return;
            }

            var raw  = DatabaseHelper.GetContributionHistory(item.Id);
            var dt   = new DataTable();
            dt.Columns.Add("Date",                typeof(string));
            dt.Columns.Add("Amount (UGX)",        typeof(string));
            dt.Columns.Add("Running Balance (UGX)", typeof(string));

            // Calculate running balance descending (FR-05)
            int balance = DatabaseHelper.GetMemberBalance(item.Id);
            foreach (DataRow row in raw.Rows)
            {
                dt.Rows.Add(row["Date"], $"{Convert.ToInt32(row["Amount (UGX)"]):N0}", $"{balance:N0}");
                balance -= Convert.ToInt32(row["Amount (UGX)"]);
            }

            grid.DataSource = dt;
            lblInfo.Text    = raw.Rows.Count == 0
                ? "No contributions recorded for this member."
                : $"{raw.Rows.Count} transaction(s) shown. Current balance: UGX {DatabaseHelper.GetMemberBalance(item.Id):N0}";
        }
    }
}
