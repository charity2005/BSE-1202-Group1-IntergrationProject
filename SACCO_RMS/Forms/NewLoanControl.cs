// ============================================================
//  NewLoanControl.cs
//  SACCO Records Management System
//  Chris – FR-06: Record loan application (6 mandatory fields)
//          FR-07: Auto-calculate repayment schedule BEFORE confirmation
// ============================================================
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public class NewLoanControl : UserControl
    {
        private ComboBox       cmbMember;
        private NumericUpDown  nudPrincipal;
        private TextBox        txtPurpose;
        private NumericUpDown  nudRate;
        private DateTimePicker dtpStart;
        private NumericUpDown  nudWeeks;
        private Label          lblSchedule, lblStatus;
        private DataGridView   gridSchedule;
        private Button         btnCalculate, btnConfirm;
        private bool           _scheduleReady;

        public NewLoanControl()
        {
            InitialiseComponent();
            LoadMembers();
        }

        private void InitialiseComponent()
        {
            BackColor = Color.White;
            Padding   = new Padding(10);

            Controls.Add(new Label { Text = "New Loan Application", Font = new Font("Arial", 14, FontStyle.Bold), AutoSize = true, Top = 0, Left = 0 });
            Controls.Add(new Label { Text = "FR-06: 6 mandatory fields. FR-07: schedule shown BEFORE confirmation.", ForeColor = Color.Gray, Font = new Font("Arial", 9), AutoSize = true, Top = 32, Left = 0 });

            int y = 66;
            Controls.Add(new Label { Text = "Member *", Font = new Font("Arial", 9), AutoSize = true, Left = 0, Top = y + 2 });
            cmbMember = new ComboBox { Width = 280, Left = 200, Top = y, Font = new Font("Arial", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            Controls.Add(cmbMember); y += 40;

            Controls.Add(new Label { Text = "Principal Amount (UGX) *", Font = new Font("Arial", 9), AutoSize = true, Left = 0, Top = y + 2 });
            nudPrincipal = new NumericUpDown { Minimum = 10000, Maximum = 50000000, Value = 500000, Increment = 50000, Width = 200, Left = 200, Top = y, Font = new Font("Arial", 10), ThousandsSeparator = true };
            Controls.Add(nudPrincipal); y += 40;

            Controls.Add(new Label { Text = "Purpose *", Font = new Font("Arial", 9), AutoSize = true, Left = 0, Top = y + 2 });
            txtPurpose = new TextBox { Width = 280, Left = 200, Top = y, Font = new Font("Arial", 10), BorderStyle = BorderStyle.FixedSingle };
            Controls.Add(txtPurpose); y += 40;

            Controls.Add(new Label { Text = "Interest Rate (% flat) *", Font = new Font("Arial", 9), AutoSize = true, Left = 0, Top = y + 2 });
            nudRate = new NumericUpDown { Minimum = 1, Maximum = 100, Value = 5, DecimalPlaces = 1, Width = 120, Left = 200, Top = y, Font = new Font("Arial", 10) };
            Controls.Add(nudRate); y += 40;

            Controls.Add(new Label { Text = "Repayment Start Date *", Font = new Font("Arial", 9), AutoSize = true, Left = 0, Top = y + 2 });
            dtpStart = new DateTimePicker { Width = 200, Left = 200, Top = y, Font = new Font("Arial", 10), Format = DateTimePickerFormat.Short };
            Controls.Add(dtpStart); y += 40;

            Controls.Add(new Label { Text = "Repayment Period (weeks) *", Font = new Font("Arial", 9), AutoSize = true, Left = 0, Top = y + 2 });
            nudWeeks = new NumericUpDown { Minimum = 1, Maximum = 104, Value = 12, Width = 120, Left = 200, Top = y, Font = new Font("Arial", 10) };
            Controls.Add(nudWeeks); y += 48;

            btnCalculate = new Button { Text = "Calculate Schedule →", Width = 180, Height = 32, Left = 0, Top = y, BackColor = Color.FromArgb(0,100,0), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Arial", 10, FontStyle.Bold) };
            btnCalculate.FlatAppearance.BorderSize = 0;
            btnCalculate.Click += BtnCalculate_Click;
            Controls.Add(btnCalculate); y += 44;

            // Schedule summary
            lblSchedule = new Label { Text = string.Empty, Font = new Font("Arial", 9, FontStyle.Bold), ForeColor = Color.FromArgb(31,73,125), AutoSize = false, Width = 700, Height = 22, Left = 0, Top = y };
            Controls.Add(lblSchedule); y += 26;

            // Schedule grid
            gridSchedule = MainDashboard.MakeGrid();
            gridSchedule.Top    = y;
            gridSchedule.Left   = 0;
            gridSchedule.Width  = 700;
            gridSchedule.Height = 160;
            gridSchedule.Visible = false;
            Controls.Add(gridSchedule); y += 168;

            lblStatus = new Label { Text = string.Empty, AutoSize = false, Width = 500, Height = 24, Top = y, Left = 0, Font = new Font("Arial", 9, FontStyle.Bold) };
            Controls.Add(lblStatus); y += 30;

            btnConfirm = new Button { Text = "Confirm & Disburse Loan", Width = 210, Height = 34, Left = 0, Top = y, BackColor = Color.FromArgb(31,73,125), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Arial", 10, FontStyle.Bold), Enabled = false };
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.Click += BtnConfirm_Click;
            Controls.Add(btnConfirm);
        }

        private void LoadMembers()
        {
            cmbMember.Items.Clear();
            cmbMember.Items.Add(new ComboItem(0, "— Select member —"));
            var dt = DatabaseHelper.GetAllMembers();
            foreach (DataRow row in dt.Rows)
                cmbMember.Items.Add(new ComboItem(Convert.ToInt32(row["MemberID"]), row["FullName"].ToString()));
            cmbMember.SelectedIndex = 0;
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            // Validate all 6 fields (FR-06)
            if (!(cmbMember.SelectedItem is ComboItem item) || item.Id == 0)
            { ShowStatus("Please select a member.", true); return; }
            if (string.IsNullOrWhiteSpace(txtPurpose.Text))
            { ShowStatus("Purpose is required.", true); return; }

            int    principal  = (int)nudPrincipal.Value;
            double rate       = (double)nudRate.Value;
            int    weeks      = (int)nudWeeks.Value;
            DateTime start    = dtpStart.Value.Date;

            int total      = (int)Math.Round(principal * (1 + rate / 100.0));
            int instalment = (int)Math.Round((double)total / weeks);

            // Build schedule DataTable
            var dt = new DataTable();
            dt.Columns.Add("Week",               typeof(int));
            dt.Columns.Add("Due Date",            typeof(string));
            dt.Columns.Add("Instalment (UGX)",    typeof(string));
            dt.Columns.Add("Balance After (UGX)", typeof(string));

            int balance = total;
            var dueDate = start;
            for (int w = 1; w <= weeks; w++)
            {
                balance -= instalment;
                if (balance < 0) balance = 0;
                dt.Rows.Add(w, dueDate.ToString("dd/MM/yyyy"), $"{instalment:N0}", $"{balance:N0}");
                dueDate = dueDate.AddDays(7);
            }

            gridSchedule.DataSource = dt;
            gridSchedule.Visible    = true;

            lblSchedule.Text = $"Principal: UGX {principal:N0}   |   Total repayable: UGX {total:N0}   |   Weekly instalment: UGX {instalment:N0}";
            _scheduleReady   = true;
            btnConfirm.Enabled = true;
            ShowStatus("Schedule calculated. Review above, then confirm.", false);
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (!_scheduleReady) return;
            if (!(cmbMember.SelectedItem is ComboItem item) || item.Id == 0) return;

            DatabaseHelper.CreateLoan(
                item.Id,
                (int)nudPrincipal.Value,
                (double)nudRate.Value,
                (int)nudWeeks.Value,
                dtpStart.Value.Date,
                txtPurpose.Text.Trim()
            );

            ShowStatus($"Loan of UGX {nudPrincipal.Value:N0} disbursed to {item.Name}.", false);
            btnConfirm.Enabled    = false;
            gridSchedule.Visible  = false;
            lblSchedule.Text      = string.Empty;
            _scheduleReady        = false;
            cmbMember.SelectedIndex = 0;
            nudPrincipal.Value    = 500000;
            txtPurpose.Clear();
        }

        private void ShowStatus(string msg, bool error)
        {
            lblStatus.Text      = msg;
            lblStatus.ForeColor = error ? Color.Red : Color.FromArgb(0, 128, 0);
        }
    }
}
