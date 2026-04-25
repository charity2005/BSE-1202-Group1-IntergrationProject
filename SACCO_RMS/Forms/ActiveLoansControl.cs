// ============================================================
//  ActiveLoansControl.cs
//  SACCO Records Management System
//  Chris – FR-08: Record repayments, update outstanding balance
// ============================================================
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public class ActiveLoansControl : UserControl
    {
        private DataGridView grid;
        private Panel        pnlRepay;
        private Label        lblRepayTitle, lblRepayStatus;
        private NumericUpDown nudRepayAmount;
        private DateTimePicker dtpRepayDate;
        private int          _selectedLoanId;

        public ActiveLoansControl()
        {
            InitialiseComponent();
            LoadLoans();
        }

        private void InitialiseComponent()
        {
            BackColor = Color.White;
            Padding   = new Padding(10);

            Controls.Add(new Label { Text = "Active Loans", Font = new Font("Arial", 14, FontStyle.Bold), AutoSize = true, Top = 0, Left = 0 });
            Controls.Add(new Label { Text = "FR-08: Select a loan and record a repayment to update outstanding balance.", ForeColor = Color.Gray, Font = new Font("Arial", 9), AutoSize = true, Top = 32, Left = 0 });

            var btnRefresh = new Button { Text = "Refresh", Width = 80, Height = 26, Left = 0, Top = 60, FlatStyle = FlatStyle.Flat };
            btnRefresh.Click += (s, e) => LoadLoans();
            Controls.Add(btnRefresh);

            grid = MainDashboard.MakeGrid();
            grid.Top    = 92;
            grid.Left   = 0;
            grid.Width  = 860;
            grid.Height = 260;
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grid.SelectionChanged += Grid_SelectionChanged;
            Controls.Add(grid);

            // ── Repayment sub-form ────────────────────────────
            pnlRepay = new Panel { Left = 0, Top = 364, Width = 500, Height = 180, BackColor = Color.FromArgb(245,248,252), BorderStyle = BorderStyle.FixedSingle, Visible = false };

            lblRepayTitle = new Label { Text = "Record Repayment", Font = new Font("Arial", 10, FontStyle.Bold), AutoSize = true, Left = 10, Top = 10 };
            pnlRepay.Controls.Add(lblRepayTitle);

            pnlRepay.Controls.Add(new Label { Text = "Amount (UGX):", Font = new Font("Arial", 9), AutoSize = true, Left = 10, Top = 42 });
            nudRepayAmount = new NumericUpDown { Minimum = 1000, Maximum = 10000000, Increment = 5000, Width = 180, Left = 160, Top = 40, Font = new Font("Arial", 10), ThousandsSeparator = true };
            pnlRepay.Controls.Add(nudRepayAmount);

            pnlRepay.Controls.Add(new Label { Text = "Date:", Font = new Font("Arial", 9), AutoSize = true, Left = 10, Top = 82 });
            dtpRepayDate = new DateTimePicker { Width = 180, Left = 160, Top = 80, Font = new Font("Arial", 10), Format = DateTimePickerFormat.Short };
            pnlRepay.Controls.Add(dtpRepayDate);

            var btnRecord = new Button { Text = "Record Repayment", Width = 160, Height = 30, Left = 10, Top = 118, BackColor = Color.FromArgb(31,73,125), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Arial", 9, FontStyle.Bold) };
            btnRecord.FlatAppearance.BorderSize = 0;
            btnRecord.Click += BtnRecord_Click;
            pnlRepay.Controls.Add(btnRecord);

            var btnCancel = new Button { Text = "Cancel", Width = 70, Height = 30, Left = 178, Top = 118, FlatStyle = FlatStyle.Flat };
            btnCancel.Click += (s, e) => pnlRepay.Visible = false;
            pnlRepay.Controls.Add(btnCancel);

            lblRepayStatus = new Label { Text = string.Empty, AutoSize = false, Width = 460, Height = 20, Left = 10, Top = 152, Font = new Font("Arial", 8, FontStyle.Bold) };
            pnlRepay.Controls.Add(lblRepayStatus);

            Controls.Add(pnlRepay);
        }

        private void LoadLoans()
        {
            grid.DataSource = DatabaseHelper.GetActiveLoans();
            pnlRepay.Visible = false;
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0) return;
            var row = grid.SelectedRows[0];
            _selectedLoanId = Convert.ToInt32(row.Cells["LoanID"].Value);

            string memberName = row.Cells["Member"].Value?.ToString() ?? string.Empty;
            lblRepayTitle.Text = $"Record Repayment  –  {memberName}";
            pnlRepay.Visible   = true;
        }

        private void BtnRecord_Click(object sender, EventArgs e)
        {
            if (_selectedLoanId <= 0) return;

            DatabaseHelper.RecordRepayment(_selectedLoanId, (int)nudRepayAmount.Value, dtpRepayDate.Value.Date);

            lblRepayStatus.Text      = $"Repayment of UGX {nudRepayAmount.Value:N0} recorded.";
            lblRepayStatus.ForeColor = Color.FromArgb(0, 128, 0);
            LoadLoans();
        }
    }
}
