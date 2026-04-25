// ============================================================
//  ContributionControl.cs
//  SACCO Records Management System
//  Jonathan – FR-04: Record contribution, auto-update balance,
//                    generate receipt
// ============================================================
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public class ContributionControl : UserControl
    {
        private ComboBox       cmbMember;
        private NumericUpDown  nudAmount;
        private DateTimePicker dtpDate;
        private Label          lblBalance, lblStatus;
        private Button         btnRecord;

        // Receipt data stored for printing
        private string  _receiptMember;
        private int     _receiptAmount;
        private DateTime _receiptDate;
        private int     _receiptNewBalance;

        public ContributionControl()
        {
            InitialiseComponent();
            LoadMembers();
        }

        private void InitialiseComponent()
        {
            BackColor = Color.White;
            Padding   = new Padding(10);

            Controls.Add(new Label { Text = "Record Weekly Contribution", Font = new Font("Arial", 14, FontStyle.Bold), AutoSize = true, Top = 0, Left = 0 });
            Controls.Add(new Label { Text = "FR-04: records payment, updates cumulative balance, generates receipt.", ForeColor = Color.Gray, Font = new Font("Arial", 9), AutoSize = true, Top = 32, Left = 0 });

            int y = 66;
            // Member selector
            Controls.Add(new Label { Text = "Member *", Font = new Font("Arial", 9), AutoSize = true, Left = 0, Top = y + 2 });
            cmbMember = new ComboBox { Width = 300, Left = 180, Top = y, Font = new Font("Arial", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbMember.SelectedIndexChanged += CmbMember_Changed;
            Controls.Add(cmbMember);
            y += 42;

            // Current balance display
            lblBalance = new Label { Text = "Current balance:  —", Font = new Font("Arial", 9, FontStyle.Italic), ForeColor = Color.FromArgb(0,100,0), AutoSize = true, Left = 0, Top = y };
            Controls.Add(lblBalance);
            y += 28;

            // Amount (whole numbers only – NFR-04)
            Controls.Add(new Label { Text = "Amount (UGX) *", Font = new Font("Arial", 9), AutoSize = true, Left = 0, Top = y + 2 });
            nudAmount = new NumericUpDown { Minimum = 1000, Maximum = 10000000, Value = 25000, Increment = 5000, Width = 200, Left = 180, Top = y, Font = new Font("Arial", 10), ThousandsSeparator = true };
            Controls.Add(nudAmount);
            y += 42;

            // Date
            Controls.Add(new Label { Text = "Date *", Font = new Font("Arial", 9), AutoSize = true, Left = 0, Top = y + 2 });
            dtpDate = new DateTimePicker { Width = 200, Left = 180, Top = y, Font = new Font("Arial", 10), Format = DateTimePickerFormat.Short };
            Controls.Add(dtpDate);
            y += 42;

            lblStatus = new Label { Text = string.Empty, AutoSize = false, Width = 500, Height = 26, Top = y, Left = 0, Font = new Font("Arial", 9, FontStyle.Bold) };
            Controls.Add(lblStatus);
            y += 32;

            btnRecord = new Button { Text = "Record & Generate Receipt", Width = 220, Height = 34, Left = 0, Top = y, BackColor = Color.FromArgb(31,73,125), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Arial", 10, FontStyle.Bold) };
            btnRecord.FlatAppearance.BorderSize = 0;
            btnRecord.Click += BtnRecord_Click;
            Controls.Add(btnRecord);
            y += 50;

            Controls.Add(new Label { Text = "— Receipt will appear in a pop-up window and can be printed —", ForeColor = Color.Gray, Font = new Font("Arial", 8), AutoSize = true, Left = 0, Top = y });
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

        private void CmbMember_Changed(object sender, EventArgs e)
        {
            if (cmbMember.SelectedItem is ComboItem item && item.Id > 0)
            {
                int bal = DatabaseHelper.GetMemberBalance(item.Id);
                lblBalance.Text = $"Current balance:  UGX {bal:N0}";
            }
            else
            {
                lblBalance.Text = "Current balance:  —";
            }
        }

        private void BtnRecord_Click(object sender, EventArgs e)
        {
            if (!(cmbMember.SelectedItem is ComboItem item) || item.Id == 0)
            { ShowStatus("Please select a member.", true); return; }

            if (nudAmount.Value != Math.Floor(nudAmount.Value) || nudAmount.Value <= 0)
            { ShowStatus("Amount must be a positive whole number (UGX, no decimals).", true); return; }

            int amount = (int)nudAmount.Value;

            DatabaseHelper.RecordContribution(item.Id, amount, dtpDate.Value.Date);

            int newBalance = DatabaseHelper.GetMemberBalance(item.Id);
            lblBalance.Text = $"Current balance:  UGX {newBalance:N0}";

            // Store receipt data
            _receiptMember     = item.Name;
            _receiptAmount     = amount;
            _receiptDate       = dtpDate.Value.Date;
            _receiptNewBalance = newBalance;

            ShowStatus($"Contribution of UGX {amount:N0} recorded for {item.Name}.", false);
            ShowReceipt();
        }

        private void ShowStatus(string msg, bool error)
        {
            lblStatus.Text      = msg;
            lblStatus.ForeColor = error ? Color.Red : Color.FromArgb(0, 128, 0);
        }

        private void ShowReceipt()
        {
            var form = new Form
            {
                Text            = "Contribution Receipt",
                Size            = new Size(400, 340),
                StartPosition   = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox     = false,
                BackColor       = Color.White
            };

            var border = new Panel { Left = 20, Top = 10, Width = 350, Height = 230, BorderStyle = BorderStyle.FixedSingle };

            void AddRow(string label, string value, int top, bool bold = false)
            {
                border.Controls.Add(new Label { Text = label, Font = new Font("Arial", 9), Left = 10, Top = top, AutoSize = true, ForeColor = Color.Gray });
                border.Controls.Add(new Label { Text = value, Font = new Font("Arial", 10, bold ? FontStyle.Bold : FontStyle.Regular), Left = 180, Top = top, AutoSize = true });
            }

            border.Controls.Add(new Label { Text = "Rural Life Development Mission SACCO", Font = new Font("Arial", 10, FontStyle.Bold), Left = 10, Top = 12, AutoSize = true });
            border.Controls.Add(new Label { Text = "OFFICIAL CONTRIBUTION RECEIPT", Font = new Font("Arial", 8), Left = 10, Top = 32, AutoSize = true, ForeColor = Color.Gray });
            border.Controls.Add(new Panel { Left = 10, Top = 50, Width = 328, Height = 1, BackColor = Color.LightGray });

            AddRow("Member:",       _receiptMember,                    58);
            AddRow("Date:",         _receiptDate.ToString("dd/MM/yyyy"), 80);
            AddRow("Amount paid:",  $"UGX {_receiptAmount:N0}",        102, bold: true);
            AddRow("New balance:",  $"UGX {_receiptNewBalance:N0}",    124, bold: true);

            border.Controls.Add(new Panel { Left = 10, Top = 152, Width = 328, Height = 1, BackColor = Color.LightGray });
            border.Controls.Add(new Label { Text = "Printed by SACCO RMS  –  Keep for your records", Font = new Font("Arial", 7.5f), Left = 10, Top = 158, AutoSize = true, ForeColor = Color.Gray });

            var btnPrint = new Button { Text = "Print", Width = 100, Height = 30, Left = 20, Top = 260, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(31,73,125), ForeColor = Color.White };
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Click += (s, e) => PrintReceipt();

            var btnClose = new Button { Text = "Close", Width = 80, Height = 30, Left = 130, Top = 260, FlatStyle = FlatStyle.Flat };
            btnClose.Click += (s, e) => form.Close();

            form.Controls.AddRange(new Control[] { border, btnPrint, btnClose });
            form.ShowDialog();
        }

        private void PrintReceipt()
        {
            var pd = new PrintDocument();
            pd.PrintPage += (sender, e) =>
            {
                var g = e.Graphics;
                var titleFont  = new Font("Arial", 12, FontStyle.Bold);
                var normalFont = new Font("Arial", 10);
                var boldFont   = new Font("Arial", 10, FontStyle.Bold);
                var smallFont  = new Font("Arial", 8);

                int y = 40;
                g.DrawString("Rural Life Development Mission SACCO", titleFont,  Brushes.Black, 40, y); y += 24;
                g.DrawString("OFFICIAL CONTRIBUTION RECEIPT",        normalFont, Brushes.Gray,  40, y); y += 30;
                g.DrawLine(Pens.LightGray, 40, y, 560, y); y += 10;
                g.DrawString("Member:",      normalFont, Brushes.Gray,  40, y);
                g.DrawString(_receiptMember, boldFont,   Brushes.Black, 200, y); y += 22;
                g.DrawString("Date:",        normalFont, Brushes.Gray,  40, y);
                g.DrawString(_receiptDate.ToString("dd/MM/yyyy"), boldFont, Brushes.Black, 200, y); y += 22;
                g.DrawString("Amount paid:", normalFont, Brushes.Gray,  40, y);
                g.DrawString($"UGX {_receiptAmount:N0}", boldFont, Brushes.Black, 200, y); y += 22;
                g.DrawString("New balance:", normalFont, Brushes.Gray,  40, y);
                g.DrawString($"UGX {_receiptNewBalance:N0}", boldFont, Brushes.Black, 200, y); y += 30;
                g.DrawLine(Pens.LightGray, 40, y, 560, y); y += 10;
                g.DrawString("Printed by SACCO RMS", smallFont, Brushes.Gray, 40, y);
            };
            new PrintDialog { Document = pd }.ShowDialog();
        }
    }

    // Helper for ComboBox items
    public class ComboItem
    {
        public int    Id   { get; }
        public string Name { get; }
        public ComboItem(int id, string name) { Id = id; Name = name; }
        public override string ToString() => Name;
    }
}
