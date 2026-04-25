namespace SACCO_RMS.Forms
{
    partial class MemberForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblTitle    = new System.Windows.Forms.Label();
            this.grpSearch   = new System.Windows.Forms.GroupBox();
            this.txtSearch   = new System.Windows.Forms.TextBox();
            this.btnSearch   = new System.Windows.Forms.Button();
            this.btnShowAll  = new System.Windows.Forms.Button();
            this.lblCount    = new System.Windows.Forms.Label();
            this.dgvMembers  = new System.Windows.Forms.DataGridView();
            this.grpRegister = new System.Windows.Forms.GroupBox();
            this.lblName     = new System.Windows.Forms.Label();
            this.txtName     = new System.Windows.Forms.TextBox();
            this.lblNID      = new System.Windows.Forms.Label();
            this.txtNID      = new System.Windows.Forms.TextBox();
            this.lblPhone    = new System.Windows.Forms.Label();
            this.txtPhone    = new System.Windows.Forms.TextBox();
            this.lblKin      = new System.Windows.Forms.Label();
            this.txtKin      = new System.Windows.Forms.TextBox();
            this.lblShare    = new System.Windows.Forms.Label();
            this.nudShare    = new System.Windows.Forms.NumericUpDown();
            this.btnSave     = new System.Windows.Forms.Button();
            this.lblStatus   = new System.Windows.Forms.Label();
            this.grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).BeginInit();
            this.grpRegister.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudShare)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 6);
            this.lblTitle.Size = new System.Drawing.Size(400, 28);
            this.lblTitle.Text = "Member Management  (FR-01, FR-02, FR-03)";

            // grpSearch
            this.grpSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grpSearch.Location = new System.Drawing.Point(10, 40);
            this.grpSearch.Size = new System.Drawing.Size(840, 60);
            this.grpSearch.Text = "Search Members";
            this.grpSearch.Controls.AddRange(new System.Windows.Forms.Control[] { txtSearch, btnSearch, btnShowAll, lblCount });

            this.txtSearch.Font = new System.Drawing.Font("Arial", 10F);
            this.txtSearch.Location = new System.Drawing.Point(10, 24);
            this.txtSearch.Size = new System.Drawing.Size(300, 24);
            

            this.btnSearch.Font = new System.Drawing.Font("Arial", 9F);
            this.btnSearch.Location = new System.Drawing.Point(320, 23);
            this.btnSearch.Size = new System.Drawing.Size(80, 26);
            this.btnSearch.Text = "Search";
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(31,73,125);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.btnShowAll.Font = new System.Drawing.Font("Arial", 9F);
            this.btnShowAll.Location = new System.Drawing.Point(408, 23);
            this.btnShowAll.Size = new System.Drawing.Size(80, 26);
            this.btnShowAll.Text = "Show All";
            this.btnShowAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);

            this.lblCount.Font = new System.Drawing.Font("Arial", 8F);
            this.lblCount.ForeColor = System.Drawing.Color.Gray;
            this.lblCount.Location = new System.Drawing.Point(498, 27);
            this.lblCount.Size = new System.Drawing.Size(200, 18);
            this.lblCount.Text = "";

            // dgvMembers
            this.dgvMembers.AllowUserToAddRows = false;
            this.dgvMembers.AllowUserToDeleteRows = false;
            this.dgvMembers.ReadOnly = true;
            this.dgvMembers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMembers.RowHeadersVisible = false;
            this.dgvMembers.BackgroundColor = System.Drawing.Color.White;
            this.dgvMembers.Font = new System.Drawing.Font("Arial", 9F);
            this.dgvMembers.Location = new System.Drawing.Point(10, 108);
            this.dgvMembers.Size = new System.Drawing.Size(840, 220);
            this.dgvMembers.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvMembers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(31, 73, 125);
            this.dgvMembers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvMembers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);

            // grpRegister
            this.grpRegister.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grpRegister.Location = new System.Drawing.Point(10, 338);
            this.grpRegister.Size = new System.Drawing.Size(840, 200);
            this.grpRegister.Text = "Register New Member  (all fields mandatory – FR-01)";
            this.grpRegister.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblName, txtName, lblNID, txtNID, lblPhone, txtPhone,
                lblKin, txtKin, lblShare, nudShare, btnSave, lblStatus });

            int lx = 10, tx = 180, rw = 240, fy = 24, gap = 34;
            System.Windows.Forms.Label[] ls = { lblName, lblNID, lblPhone, lblKin, lblShare };
            System.Windows.Forms.Control[] ts = { txtName, txtNID, txtPhone, txtKin, null };
            string[] lt = { "Full Name *", "National ID *", "Phone Number *", "Next-of-Kin *", "Share Capital (UGX) *" };
            for (int i = 0; i < 5; i++)
            {
                ls[i].Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular);
                ls[i].Location = new System.Drawing.Point(lx, fy + i * gap + 4);
                ls[i].Size = new System.Drawing.Size(160, 20);
                ls[i].Text = lt[i];
                if (ts[i] != null)
                {
                    ts[i].Font = new System.Drawing.Font("Arial", 10F);
                    ts[i].Location = new System.Drawing.Point(tx, fy + i * gap);
                    ts[i].Size = new System.Drawing.Size(rw, 26);
                }
            }
            this.nudShare.Font = new System.Drawing.Font("Arial", 10F);
            this.nudShare.Location = new System.Drawing.Point(tx, fy + 4 * gap);
            this.nudShare.Size = new System.Drawing.Size(160, 26);
            this.nudShare.Minimum = 0; this.nudShare.Maximum = 10000000;
            this.nudShare.Value = 50000; this.nudShare.Increment = 5000;
            this.nudShare.ThousandsSeparator = true;

            this.btnSave.BackColor = System.Drawing.Color.FromArgb(31, 73, 125);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(tx, fy + 5 * gap - 4);
            this.btnSave.Size = new System.Drawing.Size(140, 32);
            this.btnSave.Text = "Save Member";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.lblStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(lx, fy + 5 * gap - 2);
            this.lblStatus.Size = new System.Drawing.Size(840, 22);
            this.lblStatus.Text = "";

            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 548);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { lblTitle, grpSearch, dgvMembers, grpRegister });
            this.Name = "MemberForm";
            this.Text = "Member Management";
            this.grpSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).EndInit();
            this.grpRegister.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudShare)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpSearch, grpRegister;
        private System.Windows.Forms.TextBox txtSearch, txtName, txtNID, txtPhone, txtKin;
        private System.Windows.Forms.Button btnSearch, btnShowAll, btnSave;
        private System.Windows.Forms.Label lblCount, lblName, lblNID, lblPhone, lblKin, lblShare, lblStatus;
        private System.Windows.Forms.NumericUpDown nudShare;
        private System.Windows.Forms.DataGridView dgvMembers;
    }
}
