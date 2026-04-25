namespace SACCO_RMS.Forms
{
    partial class OverdueForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.lblTitle   = new System.Windows.Forms.Label();
            this.lblSub     = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblSummary = new System.Windows.Forms.Label();
            this.dgvOverdue = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverdue)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 6); this.lblTitle.Size = new System.Drawing.Size(500, 28);
            this.lblTitle.Text = "Overdue Loans  (FR-09)";

            this.lblSub.Font = new System.Drawing.Font("Arial", 9F); this.lblSub.ForeColor = System.Drawing.Color.Gray;
            this.lblSub.Location = new System.Drawing.Point(10, 34); this.lblSub.Size = new System.Drawing.Size(700, 20);
            this.lblSub.Text = "Loans with at least one missed weekly instalment as of today's date.";

            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(31, 73, 125);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(10, 60); this.btnRefresh.Size = new System.Drawing.Size(110, 28);
            this.btnRefresh.Text = "Refresh List"; this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.lblSummary.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblSummary.Location = new System.Drawing.Point(130, 64); this.lblSummary.Size = new System.Drawing.Size(600, 22);
            this.lblSummary.Text = "";

            this.dgvOverdue.AllowUserToAddRows = false; this.dgvOverdue.AllowUserToDeleteRows = false; this.dgvOverdue.ReadOnly = true;
            this.dgvOverdue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOverdue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOverdue.RowHeadersVisible = false; this.dgvOverdue.BackgroundColor = System.Drawing.Color.White;
            this.dgvOverdue.Font = new System.Drawing.Font("Arial", 9F);
            this.dgvOverdue.Location = new System.Drawing.Point(10, 96);
            this.dgvOverdue.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.dgvOverdue.Size = new System.Drawing.Size(840, 460);
            this.dgvOverdue.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(150, 30, 30);
            this.dgvOverdue.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvOverdue.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);

            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 570);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { lblTitle, lblSub, btnRefresh, lblSummary, dgvOverdue });
            this.Name = "OverdueForm"; this.Text = "Overdue Loans";
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverdue)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle, lblSub, lblSummary;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvOverdue;
    }
}
