// ============================================================
//  WeeklyReportControl.cs
//  SACCO Records Management System
//  Chris – FR-10: Weekly financial summary report
//          Totals: contributions, disbursements, repayments,
//          loan fund balance – generated in < 5 seconds
// ============================================================
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public class WeeklyReportControl : UserControl
    {
        private DateTimePicker dtpWeek;
        private Label          lblContrib, lblDisbursed, lblRepaid, lblBalance;
        private DataGridView   grid;
        private Panel          pnlTotals;
        private bool           _reportLoaded;
        private int _tc, _td, _tr;

        public WeeklyReportControl()
        {
            InitialiseComponent();
        }

        private void InitialiseComponent()
        {
            BackColor = Color.White;
            Padding   = new Padding(10);

            Controls.Add(new Label { Text = "Weekly Financial Summary", Font = new Font("Arial", 14, FontStyle.Bold), AutoSize = true, Top = 0, Left = 0 });
            Controls.Add(new Label { Text = "FR-10: Select any date in the week. Report generates within 5 seconds.", ForeColor = Color.Gray, Font = new Font("Arial", 9), AutoSize = true, Top = 32, Left = 0 });

            Controls.Add(new Label { Text = "Select date in week:", Font = new Font("Arial", 9), AutoSize = true, Left = 0, Top = 66 });
            dtpWeek = new DateTimePicker { Width = 200, Left = 170, Top = 63, Font = new Font("Arial", 10), Format = DateTimePickerFormat.Short };
            Controls.Add(dtpWeek);

            var btnGen = new Button { Text = "Generate Report", Width = 140, Height = 30, Left = 380, Top = 62, BackColor = Color.FromArgb(31,73,125), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Arial", 9, FontStyle.Bold) };
            btnGen.FlatAppearance.BorderSize = 0;
            btnGen.Click += BtnGenerate_Click;
            Controls.Add(btnGen);

            var btnPrint = new Button { Text = "Print", Width = 80, Height = 30, Left = 528, Top = 62, FlatStyle = FlatStyle.Flat };
            btnPrint.Click += (s, e) => { if (_reportLoaded) PrintReport(); };
            Controls.Add(btnPrint);

            // ── Summary tiles ─────────────────────────────────
            pnlTotals = new Panel { Left = 0, Top = 106, Width = 860, Height = 80, Visible = false };
            Controls.Add(pnlTotals);

            var tileLabels = new[] { "Total Contributions", "Total Disbursed", "Repayments Received", "Loan Fund Balance" };
            var tileColors = new[] { Color.FromArgb(0,100,0), Color.FromArgb(180,100,0), Color.FromArgb(31,73,125), Color.FromArgb(100,0,120) };
            int tx = 0;
            for (int i = 0; i < 4; i++)
            {
                var tile = new Panel { Left = tx, Top = 0, Width = 210, Height = 78, BackColor = tileColors[i] };
                var lv   = new Label { Font = new Font("Arial", 13, FontStyle.Bold), ForeColor = Color.White, Left = 8, Top = 28, Width = 194, AutoSize = false };
                tile.Controls.Add(new Label { Text = tileLabels[i], ForeColor = Color.FromArgb(200,230,255), Font = new Font("Arial", 8), Left = 8, Top = 8, AutoSize = true });
                tile.Controls.Add(lv);
                pnlTotals.Controls.Add(tile);
                switch (i)
                {
                    case 0: lblContrib   = lv; break;
                    case 1: lblDisbursed = lv; break;
                    case 2: lblRepaid    = lv; break;
                    case 3: lblBalance   = lv; break;
                }
                tx += 216;
            }

            grid = MainDashboard.MakeGrid();
            grid.Top    = 196;
            grid.Left   = 0;
            grid.Width  = 860;
            grid.Height = 430;
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            Controls.Add(grid);
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            DateTime selected  = dtpWeek.Value.Date;
            // Monday–Sunday of the selected week
            int offset         = (int)selected.DayOfWeek - (int)DayOfWeek.Monday;
            if (offset < 0) offset += 7;
            DateTime weekStart = selected.AddDays(-offset);
            DateTime weekEnd   = weekStart.AddDays(6);

            var summary = DatabaseHelper.GetWeeklySummary(weekStart, weekEnd);
            if (summary.Rows.Count > 0)
            {
                _tc = Convert.ToInt32(summary.Rows[0]["TotalContributions"]);
                _td = Convert.ToInt32(summary.Rows[0]["TotalDisbursed"]);
                _tr = Convert.ToInt32(summary.Rows[0]["TotalRepayments"]);
                int balance = _tc + _tr - _td;   // simplified fund movement

                lblContrib.Text   = $"UGX {_tc:N0}";
                lblDisbursed.Text = $"UGX {_td:N0}";
                lblRepaid.Text    = $"UGX {_tr:N0}";
                lblBalance.Text   = $"UGX {balance:N0}";
            }

            var txData = DatabaseHelper.GetWeeklyTransactions(weekStart, weekEnd);
            grid.DataSource   = txData;
            pnlTotals.Visible = true;
            _reportLoaded     = true;
        }

        private void PrintReport()
        {
            var pd = new PrintDocument();
            pd.PrintPage += (sender, e) =>
            {
                var g       = e.Graphics;
                var bf      = new Font("Arial", 11, FontStyle.Bold);
                var nf      = new Font("Arial", 10);
                var sf      = new Font("Arial", 8);
                int y       = 40;
                g.DrawString("Rural Life Development Mission SACCO", bf, Brushes.Black, 40, y); y += 24;
                g.DrawString($"WEEKLY FINANCIAL SUMMARY — Week of {dtpWeek.Value:dd MMM yyyy}", nf, Brushes.Gray, 40, y); y += 30;
                g.DrawLine(Pens.LightGray, 40, y, 760, y); y += 10;
                g.DrawString($"Total Contributions:  UGX {_tc:N0}", bf, Brushes.Black, 40, y); y += 22;
                g.DrawString($"Total Disbursed:      UGX {_td:N0}", nf, Brushes.Black, 40, y); y += 22;
                g.DrawString($"Repayments Received:  UGX {_tr:N0}", nf, Brushes.Black, 40, y); y += 22;
                g.DrawString($"Net Fund Movement:    UGX {(_tc + _tr - _td):N0}", bf, Brushes.Black, 40, y); y += 30;
                g.DrawLine(Pens.LightGray, 40, y, 760, y); y += 10;
                g.DrawString("Transaction details — see on-screen report", sf, Brushes.Gray, 40, y);
            };
            new PrintDialog { Document = pd }.ShowDialog();
        }
    }
}
