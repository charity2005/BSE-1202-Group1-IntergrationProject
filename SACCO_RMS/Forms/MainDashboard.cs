// ============================================================
//  MainDashboard.cs
//  SACCO Records Management System
//  Tony – Dashboard: metrics, activity feed, navigation
// ============================================================
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public class MainDashboard : Form
    {
        private Panel   pnlSidebar;
        private Panel   pnlContent;

        public MainDashboard()
        {
            InitialiseComponent();
            LoadDashboard();
        }

        private void InitialiseComponent()
        {
            Text            = $"SACCO RMS  –  Logged in as {Session.Username} ({Session.Role})";
            Size            = new Size(1100, 720);
            StartPosition   = FormStartPosition.CenterScreen;
            MinimumSize     = new Size(1000, 650);
            BackColor       = Color.FromArgb(245, 247, 250);

            BuildSidebar();
            BuildContentArea();
        }

        // ─────────────────────────────────────────────────────
        //  Sidebar (navigation)
        // ─────────────────────────────────────────────────────
        private void BuildSidebar()
        {
            pnlSidebar = new Panel
            {
                Width     = 200,
                Dock      = DockStyle.Left,
                BackColor = Color.FromArgb(31, 73, 125)
            };

            var lblApp = new Label
            {
                Text      = "SACCO RMS",
                ForeColor = Color.White,
                Font      = new Font("Arial", 12, FontStyle.Bold),
                Left      = 10, Top = 14,
                AutoSize  = true
            };
            var lblSub = new Label
            {
                Text      = "Rural Life Dev Mission",
                ForeColor = Color.FromArgb(160, 200, 240),
                Font      = new Font("Arial", 8),
                Left      = 10, Top = 36,
                AutoSize  = true
            };
            var lblUser = new Label
            {
                Text      = $"{Session.Username}\n({Session.Role})",
                ForeColor = Color.FromArgb(200, 220, 255),
                Font      = new Font("Arial", 8),
                Left      = 10, Top = 56,
                AutoSize  = true
            };

            var sep = new Panel
            {
                Height    = 1,
                Width     = 180,
                Left      = 10, Top = 88,
                BackColor = Color.FromArgb(80, 120, 180)
            };

            pnlSidebar.Controls.AddRange(new Control[] { lblApp, lblSub, lblUser, sep });

            // Nav buttons
            string[] items = {
                "Dashboard", "Register Member", "Member Search",
                "Record Contribution", "Contribution History",
                "New Loan", "Active Loans", "Overdue Loans",
                "Weekly Report", "Backup / Restore"
            };
            int y = 100;
            foreach (var item in items)
            {
                var btn = new Button
                {
                    Text      = item,
                    Width     = 196,
                    Height    = 36,
                    Left      = 2,
                    Top       = y,
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = Color.White,
                    Font      = new Font("Arial", 9),
                    BackColor = Color.Transparent,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding   = new Padding(10, 0, 0, 0),
                    Tag       = item
                };
                btn.FlatAppearance.BorderSize  = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 100, 160);
                btn.Click += NavButton_Click;

                // Disable member-only restricted buttons for non-admins
                if ((item == "Record Contribution" || item == "New Loan" ||
                     item == "Active Loans"       || item == "Backup / Restore" ||
                     item == "Weekly Report") && !Session.IsAdmin)
                {
                    btn.Enabled   = false;
                    btn.ForeColor = Color.FromArgb(100, 140, 180);
                }

                pnlSidebar.Controls.Add(btn);
                y += 38;
            }

            // Logout button at bottom
            // Logout button at bottom - FIXED
            var btnLogout = new Button
            {
                Text = "Logout",
                Width = 196,
                Height = 36,
                Left = 2,
                // FIX: Instead of setting .Bottom, we set the Top relative to the panel height
                Top = pnlSidebar.Height - 46,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(255, 180, 180),
                Font = new Font("Arial", 9),
                BackColor = Color.Transparent
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) =>
            {
                // Ensure Session.Clear() exists in your Database file
                Session.Clear();
                new LoginForm().Show();
                this.Close();
            };
            pnlSidebar.Controls.Add(btnLogout);

            pnlSidebar.Controls.Add(btnLogout);

            Controls.Add(pnlSidebar);
        }

        // ─────────────────────────────────────────────────────
        //  Content area
        // ─────────────────────────────────────────────────────
        private void BuildContentArea()
        {
            pnlContent = new Panel
            {
                Dock      = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding   = new Padding(20)
            };
            Controls.Add(pnlContent);
        }

        // ─────────────────────────────────────────────────────
        //  Navigation handler
        // ─────────────────────────────────────────────────────
        private void NavButton_Click(object sender, EventArgs e)
        {
            var tag = ((Button)sender).Tag?.ToString() ?? string.Empty;
            pnlContent.Controls.Clear();

            UserControl uc = null;
            switch (tag)
            {
                case "Dashboard":           LoadDashboard();                    return;
                case "Register Member":     uc = new MemberRegistrationControl(); break;
                case "Member Search":       uc = new MemberSearchControl();       break;
                case "Record Contribution": uc = new ContributionControl();       break;
                case "Contribution History":uc = new ContribHistoryControl();     break;
                case "New Loan":            uc = new NewLoanControl();            break;
                case "Active Loans":        uc = new ActiveLoansControl();        break;
                case "Overdue Loans":       uc = new OverdueLoansControl();       break;
                case "Weekly Report":       uc = new WeeklyReportControl();       break;
                case "Backup / Restore":    uc = new BackupControl();             break;
            }

            if (uc != null)
            {
                uc.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(uc);
            }
        }

        // ─────────────────────────────────────────────────────
        //  Dashboard panel (Tony)
        // ─────────────────────────────────────────────────────
        private void LoadDashboard()
        {
            pnlContent.Controls.Clear();

            var lbl = new Label
            {
                Text     = $"Welcome, {Session.Username}   –   {DateTime.Now:dddd, dd MMMM yyyy}",
                Font     = new Font("Arial", 13, FontStyle.Bold),
                AutoSize = true,
                Top      = 10, Left = 0
            };
            pnlContent.Controls.Add(lbl);

            // ── Summary tiles ────────────────────────────────
            int memberCount   = DatabaseHelper.GetAllMembers().Rows.Count;
            int overdueCount  = DatabaseHelper.GetOverdueLoans().Rows.Count;
            int activeLoans   = DatabaseHelper.GetActiveLoans().Rows.Count;

            var today  = DateTime.Today;
            var monday = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var sunday = monday.AddDays(6);
            var summary = DatabaseHelper.GetWeeklySummary(monday, sunday);
            int weekContrib = summary.Rows.Count > 0
                ? Convert.ToInt32(summary.Rows[0]["TotalContributions"]) : 0;

            var tiles = new (string Label, string Value, Color Colour)[]
            {
                ("Total Members",           memberCount.ToString(),                   Color.FromArgb(31, 73, 125)),
                ("This Week's Contributions","UGX " + weekContrib.ToString("N0"),     Color.FromArgb(0, 128, 0)),
                ("Active Loans",            activeLoans.ToString(),                   Color.FromArgb(200, 120, 0)),
                ("Overdue Loans",           overdueCount.ToString(),                  overdueCount > 0 ? Color.FromArgb(180,30,30) : Color.FromArgb(0,128,0))
            };

            int tx = 0;
            foreach (var (label, value, colour) in tiles)
            {
                var tile = new Panel
                {
                    Width     = 190,
                    Height    = 90,
                    Left      = tx,
                    Top       = 50,
                    BackColor = colour,
                    Cursor    = Cursors.Default
                };
                tile.Controls.Add(new Label
                {
                    Text      = label,
                    ForeColor = Color.FromArgb(220, 235, 255),
                    Font      = new Font("Arial", 8),
                    AutoSize  = false,
                    Width     = 180,
                    Top       = 10, Left = 8
                });
                tile.Controls.Add(new Label
                {
                    Text      = value,
                    ForeColor = Color.White,
                    Font      = new Font("Arial", 16, FontStyle.Bold),
                    AutoSize  = false,
                    Width     = 180,
                    Top       = 32, Left = 8
                });
                pnlContent.Controls.Add(tile);
                tx += 200;
            }

            // ── Overdue alert ─────────────────────────────────
            if (overdueCount > 0)
            {
                var alert = new Label
                {
                    Text      = $"⚠  {overdueCount} loan(s) have missed instalments. Go to Overdue Loans for details.",
                    ForeColor = Color.FromArgb(150, 0, 0),
                    BackColor = Color.FromArgb(255, 235, 235),
                    Font      = new Font("Arial", 9, FontStyle.Bold),
                    AutoSize  = false,
                    Width     = 780,
                    Height    = 28,
                    Left      = 0, Top = 158,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding   = new Padding(10, 0, 0, 0)
                };
                pnlContent.Controls.Add(alert);
            }

            // ── Recent transactions grid ──────────────────────
            var lblRecent = new Label
            {
                Text     = "Recent transactions this week:",
                Font     = new Font("Arial", 10, FontStyle.Bold),
                AutoSize = true,
                Top      = 196, Left = 0
            };
            pnlContent.Controls.Add(lblRecent);

            var grid = MakeGrid();
            grid.Top  = 220;
            grid.Left = 0;
            grid.Width  = pnlContent.Width - 48;
            grid.Height = pnlContent.Height - 240;
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Left |
                          AnchorStyles.Right | AnchorStyles.Bottom;

            var txData = DatabaseHelper.GetWeeklyTransactions(monday, sunday);
            grid.DataSource = txData;
            pnlContent.Controls.Add(grid);
        }

        // ─────────────────────────────────────────────────────
        //  Shared grid factory
        // ─────────────────────────────────────────────────────
        public static DataGridView MakeGrid()
        {
            var g = new DataGridView
            {
                AllowUserToAddRows    = false,
                AllowUserToDeleteRows = false,
                ReadOnly              = true,
                AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode         = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor       = Color.White,
                BorderStyle           = BorderStyle.None,
                RowHeadersVisible     = false,
                Font                  = new Font("Arial", 9),
                GridColor             = Color.FromArgb(220, 225, 230),
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
            };
            g.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 73, 125);
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.Font      = new Font("Arial", 9, FontStyle.Bold);
            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 252);
            return g;
        }
    }
}
