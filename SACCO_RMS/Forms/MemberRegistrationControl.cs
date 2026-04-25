// ============================================================
//  MemberRegistrationControl.cs
//  SACCO Records Management System
//  Jonathan – FR-01: Register new member (5 mandatory fields)
//             FR-02: Duplicate national ID detection
// ============================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public class MemberRegistrationControl : UserControl
    {
        private TextBox txtFullName, txtNationalID, txtPhone, txtNextOfKin;
        private NumericUpDown nudShareCapital;
        private Label   lblStatus;
        private Button  btnSave, btnClear;

        public MemberRegistrationControl()
        {
            InitialiseComponent();
        }

        private void InitialiseComponent()
        {
            BackColor = Color.White;
            Padding   = new Padding(10);

            var lblTitle = new Label
            {
                Text     = "Register New Member",
                Font     = new Font("Arial", 14, FontStyle.Bold),
                AutoSize = true,
                Top      = 0, Left = 0
            };
            Controls.Add(lblTitle);

            var lblSub = new Label
            {
                Text      = "All five fields are mandatory (FR-01). Duplicate National ID is rejected (FR-02).",
                ForeColor = Color.Gray,
                Font      = new Font("Arial", 9),
                AutoSize  = true,
                Top       = 32, Left = 0
            };
            Controls.Add(lblSub);

            int y = 66;
            txtFullName    = AddField("Full Name *",          ref y);
            txtNationalID  = AddField("National ID Number *", ref y);
            txtPhone       = AddField("Phone Number *",       ref y);
            txtNextOfKin   = AddField("Next-of-Kin Name *",   ref y);

            // Share capital – numeric only
            var lblShare = new Label { Text = "Share Capital Paid (UGX) *", Font = new Font("Arial", 9), Left = 0, Top = y, AutoSize = true };
            Controls.Add(lblShare);
            nudShareCapital = new NumericUpDown
            {
                Minimum    = 0,
                Maximum    = 10000000,
                Increment  = 5000,
                Width      = 260,
                Left       = 220,
                Top        = y - 2,
                Font       = new Font("Arial", 10),
                ThousandsSeparator = true
            };
            Controls.Add(nudShareCapital);
            y += 42;

            lblStatus = new Label
            {
                Text      = string.Empty,
                AutoSize  = false,
                Width     = 500,
                Height    = 26,
                Top       = y,
                Left      = 0,
                Font      = new Font("Arial", 9, FontStyle.Bold)
            };
            Controls.Add(lblStatus);
            y += 32;

            btnSave = new Button
            {
                Text      = "Save Member",
                Width     = 130,
                Height    = 34,
                Left      = 0,
                Top       = y,
                BackColor = Color.FromArgb(31, 73, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Arial", 10, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            Controls.Add(btnSave);

            btnClear = new Button
            {
                Text      = "Clear",
                Width     = 80,
                Height    = 34,
                Left      = 140,
                Top       = y,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Arial", 10)
            };
            btnClear.Click += (s, e) => ClearFields();
            Controls.Add(btnClear);
        }

        private TextBox AddField(string label, ref int y)
        {
            var lbl = new Label { Text = label, Font = new Font("Arial", 9), Left = 0, Top = y + 2, AutoSize = true };
            var txt = new TextBox { Width = 260, Left = 220, Top = y, Font = new Font("Arial", 10), BorderStyle = BorderStyle.FixedSingle };
            Controls.Add(lbl);
            Controls.Add(txt);
            y += 42;
            return txt;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate all five fields (FR-01)
            if (string.IsNullOrWhiteSpace(txtFullName.Text)   ||
                string.IsNullOrWhiteSpace(txtNationalID.Text)  ||
                string.IsNullOrWhiteSpace(txtPhone.Text)       ||
                string.IsNullOrWhiteSpace(txtNextOfKin.Text)   ||
                nudShareCapital.Value == 0)
            {
                ShowStatus("All five fields are required.", error: true);
                return;
            }

            // Duplicate check (FR-02)
            if (DatabaseHelper.NationalIDExists(txtNationalID.Text.Trim()))
            {
                ShowStatus("Duplicate National ID — a member with this ID already exists.", error: true);
                return;
            }

            bool ok = DatabaseHelper.RegisterMember(
                txtFullName.Text.Trim(),
                txtNationalID.Text.Trim().ToUpper(),
                txtPhone.Text.Trim(),
                txtNextOfKin.Text.Trim(),
                (int)nudShareCapital.Value
            );

            if (ok)
            {
                ShowStatus($"Member \"{txtFullName.Text.Trim()}\" registered successfully.", error: false);
                ClearFields();
            }
            else
            {
                ShowStatus("Registration failed. Duplicate National ID detected.", error: true);
            }
        }

        private void ShowStatus(string message, bool error)
        {
            lblStatus.Text      = message;
            lblStatus.ForeColor = error ? Color.Red : Color.FromArgb(0, 128, 0);
        }

        private void ClearFields()
        {
            txtFullName.Clear();
            txtNationalID.Clear();
            txtPhone.Clear();
            txtNextOfKin.Clear();
            nudShareCapital.Value = 0;
        }
    }
}
