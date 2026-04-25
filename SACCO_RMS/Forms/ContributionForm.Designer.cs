namespace SACCO_RMS.Forms
{
    partial class ContributionForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.lblTitle    = new System.Windows.Forms.Label();
            this.grpRecord   = new System.Windows.Forms.GroupBox();
            this.lblMember   = new System.Windows.Forms.Label();
            this.cmbMember   = new System.Windows.Forms.ComboBox();
            this.lblBalance  = new System.Windows.Forms.Label();
            this.lblAmt      = new System.Windows.Forms.Label();
            this.nudAmount   = new System.Windows.Forms.NumericUpDown();
            this.lblDate     = new System.Windows.Forms.Label();
            this.dtpDate     = new System.Windows.Forms.DateTimePicker();
            this.btnRecord   = new System.Windows.Forms.Button();
            this.lblStatus   = new System.Windows.Forms.Label();
            this.grpReceipt  = new System.Windows.Forms.GroupBox();
            this.lblRMember  = new System.Windows.Forms.Label();
            this.lblRDate    = new System.Windows.Forms.Label();
            this.lblRAmount  = new System.Windows.Forms.Label();
            this.lblRBalance = new System.Windows.Forms.Label();
            this.grpHistory  = new System.Windows.Forms.GroupBox();
            this.lblHistMem  = new System.Windows.Forms.Label();
            this.cmbHistory  = new System.Windows.Forms.ComboBox();
            this.dgvHistory  = new System.Windows.Forms.DataGridView();
            this.grpRecord.SuspendLayout();
            this.grpReceipt.SuspendLayout();
            this.grpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 6);
            this.lblTitle.Size = new System.Drawing.Size(500, 28);
            this.lblTitle.Text = "Contributions  (FR-04, FR-05)";

            // grpRecord
            this.grpRecord.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grpRecord.Location = new System.Drawing.Point(10, 40);
            this.grpRecord.Size = new System.Drawing.Size(400, 210);
            this.grpRecord.Text = "Record Weekly Contribution";
            this.grpRecord.Controls.AddRange(new System.Windows.Forms.Control[] { lblMember, cmbMember, lblBalance, lblAmt, nudAmount, lblDate, dtpDate, btnRecord, lblStatus });

            int lx = 10, tx = 160, y = 24, g = 34;
            void AddRow(System.Windows.Forms.Label l, string t, System.Windows.Forms.Control ctrl) {
                l.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular);
                l.Location = new System.Drawing.Point(lx, y + 3); l.Size = new System.Drawing.Size(145, 20); l.Text = t;
                ctrl.Font = new System.Drawing.Font("Arial", 10F); ctrl.Location = new System.Drawing.Point(tx, y); ctrl.Size = new System.Drawing.Size(220, 26);
                y += g; }

            AddRow(lblMember,  "Member *",          cmbMember);
            cmbMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbMember.SelectedIndexChanged += new System.EventHandler(this.cmbMember_SelectedIndexChanged);

            this.lblBalance.Font = new System.Drawing.Font("Arial", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblBalance.ForeColor = System.Drawing.Color.Green;
            this.lblBalance.Location = new System.Drawing.Point(tx, y); this.lblBalance.Size = new System.Drawing.Size(220, 18);
            this.lblBalance.Text = "Current balance:  —"; y += 24;

            AddRow(lblAmt,  "Amount (UGX) *", nudAmount);
            nudAmount.Minimum = 1000; nudAmount.Maximum = 10000000; nudAmount.Value = 25000;
            nudAmount.Increment = 5000; nudAmount.ThousandsSeparator = true;

            AddRow(lblDate, "Date *", dtpDate);
            dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.btnRecord.BackColor = System.Drawing.Color.FromArgb(31, 73, 125);
            this.btnRecord.ForeColor = System.Drawing.Color.White;
            this.btnRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecord.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnRecord.Location = new System.Drawing.Point(tx, y); this.btnRecord.Size = new System.Drawing.Size(200, 32);
            this.btnRecord.Text = "Record & Generate Receipt";
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);

            this.lblStatus.Font = new System.Drawing.Font("Arial", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(lx, y + 36); this.lblStatus.Size = new System.Drawing.Size(380, 20);
            this.lblStatus.Text = "";

            // grpReceipt
            this.grpReceipt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grpReceipt.Location = new System.Drawing.Point(420, 40);
            this.grpReceipt.Size = new System.Drawing.Size(340, 160);
            this.grpReceipt.Text = "Receipt";
            this.grpReceipt.Visible = false;
            this.grpReceipt.Controls.AddRange(new System.Windows.Forms.Control[] { lblRMember, lblRDate, lblRAmount, lblRBalance });

            var rLabels = new[] { lblRMember, lblRDate, lblRAmount, lblRBalance };
            for (int i = 0; i < 4; i++) {
                rLabels[i].Font = new System.Drawing.Font("Arial", 9.5F);
                rLabels[i].Location = new System.Drawing.Point(12, 24 + i * 26);
                rLabels[i].Size = new System.Drawing.Size(310, 22);
                rLabels[i].Text = "—"; }
            lblRAmount.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold);
            lblRBalance.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold);
            lblRBalance.ForeColor = System.Drawing.Color.Green;

            // grpHistory
            this.grpHistory.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grpHistory.Location = new System.Drawing.Point(10, 260);
            this.grpHistory.Size = new System.Drawing.Size(840, 300);
            this.grpHistory.Text = "Contribution History (FR-05)";
            this.grpHistory.Controls.AddRange(new System.Windows.Forms.Control[] { lblHistMem, cmbHistory, dgvHistory });

            this.lblHistMem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular);
            this.lblHistMem.Location = new System.Drawing.Point(10, 26); this.lblHistMem.Size = new System.Drawing.Size(120, 22); this.lblHistMem.Text = "Select Member:";
            this.cmbHistory.Font = new System.Drawing.Font("Arial", 10F);
            this.cmbHistory.Location = new System.Drawing.Point(135, 24); this.cmbHistory.Size = new System.Drawing.Size(280, 26);
            this.cmbHistory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHistory.SelectedIndexChanged += new System.EventHandler(this.cmbHistory_SelectedIndexChanged);

            this.dgvHistory.AllowUserToAddRows = false; this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.ReadOnly = true; this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.RowHeadersVisible = false; this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistory.Font = new System.Drawing.Font("Arial", 9F);
            this.dgvHistory.Location = new System.Drawing.Point(10, 56); this.dgvHistory.Size = new System.Drawing.Size(816, 230);
            this.dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(31, 73, 125);
            this.dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvHistory.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);

            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 570);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { lblTitle, grpRecord, grpReceipt, grpHistory });
            this.Name = "ContributionForm"; this.Text = "Contributions";
            this.grpRecord.ResumeLayout(false); this.grpReceipt.ResumeLayout(false);
            this.grpHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpRecord, grpReceipt, grpHistory;
        private System.Windows.Forms.Label lblMember, lblBalance, lblAmt, lblDate, lblStatus;
        private System.Windows.Forms.Label lblRMember, lblRDate, lblRAmount, lblRBalance;
        private System.Windows.Forms.Label lblHistMem;
        private System.Windows.Forms.ComboBox cmbMember, cmbHistory;
        private System.Windows.Forms.NumericUpDown nudAmount;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.DataGridView dgvHistory;
    }
}
