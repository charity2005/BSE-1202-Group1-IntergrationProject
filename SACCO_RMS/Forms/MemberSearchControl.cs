// ============================================================
//  MemberSearchControl.cs
//  SACCO Records Management System
//  Jonathan – FR-03: Search member register by name or ID
// ============================================================
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public class MemberSearchControl : UserControl
    {
        private TextBox      txtSearch;
        private DataGridView grid;
        private Label        lblCount;

        public MemberSearchControl()
        {
            InitialiseComponent();
            LoadAll();
        }

        private void InitialiseComponent()
        {
            BackColor = Color.White;
            Padding   = new Padding(10);

            var lblTitle = new Label
            {
                Text     = "Member Search",
                Font     = new Font("Arial", 14, FontStyle.Bold),
                AutoSize = true,
                Top      = 0, Left = 0
            };
            Controls.Add(lblTitle);

            var lblSub = new Label
            {
                Text      = "Search by full name or National ID number (FR-03). Results appear on this screen.",
                ForeColor = Color.Gray,
                Font      = new Font("Arial", 9),
                AutoSize  = true,
                Top       = 32, Left = 0
            };
            Controls.Add(lblSub);

            txtSearch = new TextBox
            {
                Width       = 320,
                Top         = 64,
                Left        = 0,
                Font        = new Font("Arial", 10),
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Type name or National ID..."
            };
            txtSearch.TextChanged += (s, e) => Search();
            Controls.Add(txtSearch);

            var btnSearch = new Button
            {
                Text      = "Search",
                Width     = 90,
                Height    = 28,
                Top       = 63,
                Left      = 328,
                BackColor = Color.FromArgb(31, 73, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += (s, e) => Search();
            Controls.Add(btnSearch);

            var btnShowAll = new Button
            {
                Text      = "Show All",
                Width     = 90,
                Height    = 28,
                Top       = 63,
                Left      = 426,
                FlatStyle = FlatStyle.Flat
            };
            btnShowAll.Click += (s, e) => { txtSearch.Clear(); LoadAll(); };
            Controls.Add(btnShowAll);

            lblCount = new Label
            {
                Text      = string.Empty,
                ForeColor = Color.Gray,
                Font      = new Font("Arial", 8),
                AutoSize  = true,
                Top       = 100, Left = 0
            };
            Controls.Add(lblCount);

            grid = MainDashboard.MakeGrid();
            grid.Top    = 118;
            grid.Left   = 0;
            grid.Width  = 900;
            grid.Height = 480;
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Left |
                          AnchorStyles.Right | AnchorStyles.Bottom;
            Controls.Add(grid);
        }

        private void LoadAll()
        {
            var dt = DatabaseHelper.GetAllMembers();
            ShowResults(dt);
        }

        private void Search()
        {
            var dt = DatabaseHelper.SearchMembers(txtSearch.Text.Trim());
            ShowResults(dt);
        }

        private void ShowResults(DataTable dt)
        {
            grid.DataSource = dt;
            int count = dt.Rows.Count;
            lblCount.Text = count == 0
                ? "No results found."
                : $"{count} member(s) found.";
        }
    }
}
