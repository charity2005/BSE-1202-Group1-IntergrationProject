using System;
using System.Drawing;
using System.Windows.Forms;
using SACCO_RMS.Database;

namespace SACCO_RMS.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            { lblError.Text = "Please enter username and password."; return; }

            string role = DatabaseHelper.AuthenticateUser(txtUsername.Text.Trim(), txtPassword.Text);
            if (string.IsNullOrEmpty(role))
            { lblError.Text = "Incorrect username or password."; txtPassword.Clear(); return; }

            Session.Username = txtUsername.Text.Trim();
            Session.Role     = role;
            new MainForm().Show();
            Hide();
        }
    }
}
