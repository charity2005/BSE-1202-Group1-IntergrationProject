// ============================================================
//  OverdueLoansControl.cs
//  SACCO Records Management System
//  Esther – FR-09: List all loans with missed instalments
//           Shows member name, amount overdue, weeks overdue
// ============================================================
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public class OverdueLoansControl : UserControl
    {
        private DataGridView grid;
        private Label        lblSummary;

        public OverdueLoansControl()
        {
            InitialiseComponent();
            LoadOverdue();
        }

        private void InitialiseComponent()
        {
            BackColor = Color.White;
            Padding   = new Padding(10);

            Controls.Add(new Label { Text = "Overdue Loans", Font = new Font("Arial", 14, FontStyle.Bold), AutoSize = true, Top = 0, Left = 0 });
            Controls.Add(new Label { Text = "FR-09: Loans with at least one missed weekly instalment as of today.", ForeColor = Color.Gray, Font = new Font("Arial", 9), AutoSize = true, Top = 32, Left = 0 });

            var btnRefresh = new Button { Text = "Refresh List", Width = 100, Height = 26, Left = 0, Top = 62, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(180,30,30), ForeColor = Color.White };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) => LoadOverdue();
            Controls.Add(btnRefresh);

            lblSummary = new Label
            {
                Text      = string.Empty,
                AutoSize  = false,
                Width     = 700,
                Height    = 28,
                Left      = 0,
                Top       = 96,
                Font      = new Font("Arial", 9, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding   = new Padding(10, 0, 0, 0),
                Visible   = false
            };
            Controls.Add(lblSummary);

            grid = MainDashboard.MakeGrid();
            grid.Top    = 132;
            grid.Left   = 0;
            grid.Width  = 860;
            grid.Height = 460;
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            Controls.Add(grid);
        }

        private void LoadOverdue()
        {
            var dt = DatabaseHelper.GetOverdueLoans();
            grid.DataSource = dt;

            if (dt.Rows.Count == 0)
            {
                lblSummary.Text      = "  No overdue loans found — all borrowers are current.";
                lblSummary.BackColor = Color.FromArgb(0, 128, 0);
                lblSummary.Visible   = true;
            }
            else
            {
                // Total amount overdue
                int totalOverdue = 0;
                foreach (DataRow row in dt.Rows)
                    totalOverdue += Convert.ToInt32(row["Amount Overdue (UGX)"]);

                lblSummary.Text      = $"  {dt.Rows.Count} loan(s) overdue  –  Total amount outstanding: UGX {totalOverdue:N0}";
                lblSummary.BackColor = Color.FromArgb(180, 30, 30);
                lblSummary.Visible   = true;
            }
        }
    }
}
