namespace SACCO_RMS.Forms
{
    partial class LoanForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.lblTitle          = new System.Windows.Forms.Label();
            this.grpApply          = new System.Windows.Forms.GroupBox();
            this.lblMember         = new System.Windows.Forms.Label();
            this.cmbMember         = new System.Windows.Forms.ComboBox();
            this.lblPrincipal      = new System.Windows.Forms.Label();
            this.nudPrincipal      = new System.Windows.Forms.NumericUpDown();
            this.lblPurpose        = new System.Windows.Forms.Label();
            this.txtPurpose        = new System.Windows.Forms.TextBox();
            this.lblRate           = new System.Windows.Forms.Label();
            this.nudRate           = new System.Windows.Forms.NumericUpDown();
            this.lblStart          = new System.Windows.Forms.Label();
            this.dtpStart          = new System.Windows.Forms.DateTimePicker();
            this.lblWeeks          = new System.Windows.Forms.Label();
            this.nudWeeks          = new System.Windows.Forms.NumericUpDown();
            this.btnCalculate      = new System.Windows.Forms.Button();
            this.btnConfirm        = new System.Windows.Forms.Button();
            this.lblCalcStatus     = new System.Windows.Forms.Label();
            this.grpSchedule       = new System.Windows.Forms.GroupBox();
            this.lblScheduleSummary= new System.Windows.Forms.Label();
            this.dgvSchedule       = new System.Windows.Forms.DataGridView();
            this.grpLoans          = new System.Windows.Forms.GroupBox();
            this.dgvLoans          = new System.Windows.Forms.DataGridView();
            this.grpRepay          = new System.Windows.Forms.GroupBox();
            this.lblRepayAmt       = new System.Windows.Forms.Label();
            this.nudRepay          = new System.Windows.Forms.NumericUpDown();
            this.lblRepayDate      = new System.Windows.Forms.Label();
            this.dtpRepay          = new System.Windows.Forms.DateTimePicker();
            this.btnRepay          = new System.Windows.Forms.Button();
            this.lblRepayStatus    = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrincipal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeeks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepay)).BeginInit();
            this.grpApply.SuspendLayout(); this.grpSchedule.SuspendLayout();
            this.grpLoans.SuspendLayout(); this.grpRepay.SuspendLayout();
            this.SuspendLayout();

            this.lblTitle.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 6); this.lblTitle.Size = new System.Drawing.Size(500, 28);
            this.lblTitle.Text = "Loan Management  (FR-06, FR-07, FR-08)";

            // grpApply
            this.grpApply.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grpApply.Location = new System.Drawing.Point(10, 40); this.grpApply.Size = new System.Drawing.Size(500, 270);
            this.grpApply.Text = "New Loan Application  (6 mandatory fields)";
            this.grpApply.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblMember, cmbMember, lblPrincipal, nudPrincipal, lblPurpose, txtPurpose,
                lblRate, nudRate, lblStart, dtpStart, lblWeeks, nudWeeks,
                btnCalculate, btnConfirm, lblCalcStatus });

            int lx=10, tx=200, y=22, g=34;
            var lbls = new[]{lblMember,lblPrincipal,lblPurpose,lblRate,lblStart,lblWeeks};
            var ctls = new System.Windows.Forms.Control[]{cmbMember,nudPrincipal,txtPurpose,nudRate,dtpStart,nudWeeks};
            var txts = new[]{"Member *","Principal (UGX) *","Purpose *","Interest Rate (%) *","Start Date *","Weeks *"};
            for(int i=0;i<6;i++){
                lbls[i].Font=new System.Drawing.Font("Arial",9F,System.Drawing.FontStyle.Regular);
                lbls[i].Location=new System.Drawing.Point(lx,y+4); lbls[i].Size=new System.Drawing.Size(185,20); lbls[i].Text=txts[i];
                ctls[i].Font=new System.Drawing.Font("Arial",10F); ctls[i].Location=new System.Drawing.Point(tx,y); ctls[i].Size=new System.Drawing.Size(220,26);
                y+=g; }
            cmbMember.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            nudPrincipal.Minimum=10000; nudPrincipal.Maximum=50000000; nudPrincipal.Value=500000; nudPrincipal.Increment=50000; nudPrincipal.ThousandsSeparator=true;
            nudRate.Minimum=1; nudRate.Maximum=100; nudRate.Value=5; nudRate.DecimalPlaces=1;
            nudWeeks.Minimum=1; nudWeeks.Maximum=104; nudWeeks.Value=12;
            dtpStart.Format=System.Windows.Forms.DateTimePickerFormat.Short;

            this.btnCalculate.BackColor=System.Drawing.Color.FromArgb(0,128,0); this.btnCalculate.ForeColor=System.Drawing.Color.White;
            this.btnCalculate.FlatStyle=System.Windows.Forms.FlatStyle.Flat; this.btnCalculate.Font=new System.Drawing.Font("Arial",10F,System.Drawing.FontStyle.Bold);
            this.btnCalculate.Location=new System.Drawing.Point(tx,y); this.btnCalculate.Size=new System.Drawing.Size(160,32);
            this.btnCalculate.Text="Calculate Schedule"; this.btnCalculate.Click+=new System.EventHandler(this.btnCalculate_Click);

            this.btnConfirm.BackColor=System.Drawing.Color.FromArgb(31,73,125); this.btnConfirm.ForeColor=System.Drawing.Color.White;
            this.btnConfirm.FlatStyle=System.Windows.Forms.FlatStyle.Flat; this.btnConfirm.Font=new System.Drawing.Font("Arial",10F,System.Drawing.FontStyle.Bold);
            this.btnConfirm.Location=new System.Drawing.Point(tx+168,y); this.btnConfirm.Size=new System.Drawing.Size(120,32);
            this.btnConfirm.Text="Confirm Loan"; this.btnConfirm.Enabled=false; this.btnConfirm.Click+=new System.EventHandler(this.btnConfirm_Click);

            this.lblCalcStatus.Font=new System.Drawing.Font("Arial",8.5F,System.Drawing.FontStyle.Bold);
            this.lblCalcStatus.Location=new System.Drawing.Point(lx,y+36); this.lblCalcStatus.Size=new System.Drawing.Size(480,20); this.lblCalcStatus.Text="";

            // grpSchedule
            this.grpSchedule.Font=new System.Drawing.Font("Arial",9F,System.Drawing.FontStyle.Bold);
            this.grpSchedule.Location=new System.Drawing.Point(520,40); this.grpSchedule.Size=new System.Drawing.Size(330,270);
            this.grpSchedule.Text="Repayment Schedule Preview (FR-07)"; this.grpSchedule.Visible=false;
            this.grpSchedule.Controls.AddRange(new System.Windows.Forms.Control[]{lblScheduleSummary,dgvSchedule});

            this.lblScheduleSummary.Font=new System.Drawing.Font("Arial",8.5F); this.lblScheduleSummary.ForeColor=System.Drawing.Color.FromArgb(31,73,125);
            this.lblScheduleSummary.Location=new System.Drawing.Point(6,20); this.lblScheduleSummary.Size=new System.Drawing.Size(316,36); this.lblScheduleSummary.Text="";

            this.dgvSchedule.AllowUserToAddRows=false; this.dgvSchedule.ReadOnly=true;
            this.dgvSchedule.AutoSizeColumnsMode=System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSchedule.RowHeadersVisible=false; this.dgvSchedule.BackgroundColor=System.Drawing.Color.White;
            this.dgvSchedule.Font=new System.Drawing.Font("Arial",8F);
            this.dgvSchedule.Location=new System.Drawing.Point(6,60); this.dgvSchedule.Size=new System.Drawing.Size(316,200);
            this.dgvSchedule.ColumnHeadersDefaultCellStyle.BackColor=System.Drawing.Color.FromArgb(31,73,125);
            this.dgvSchedule.ColumnHeadersDefaultCellStyle.ForeColor=System.Drawing.Color.White;

            // grpLoans
            this.grpLoans.Font=new System.Drawing.Font("Arial",9F,System.Drawing.FontStyle.Bold);
            this.grpLoans.Location=new System.Drawing.Point(10,320); this.grpLoans.Size=new System.Drawing.Size(840,200);
            this.grpLoans.Text="Active Loans  (click a row to record repayment — FR-08)";
            this.grpLoans.Controls.Add(dgvLoans);

            this.dgvLoans.AllowUserToAddRows=false; this.dgvLoans.AllowUserToDeleteRows=false; this.dgvLoans.ReadOnly=true;
            this.dgvLoans.AutoSizeColumnsMode=System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLoans.SelectionMode=System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoans.RowHeadersVisible=false; this.dgvLoans.BackgroundColor=System.Drawing.Color.White;
            this.dgvLoans.Font=new System.Drawing.Font("Arial",9F);
            this.dgvLoans.Location=new System.Drawing.Point(8,22); this.dgvLoans.Size=new System.Drawing.Size(820,168);
            this.dgvLoans.ColumnHeadersDefaultCellStyle.BackColor=System.Drawing.Color.FromArgb(31,73,125);
            this.dgvLoans.ColumnHeadersDefaultCellStyle.ForeColor=System.Drawing.Color.White;
            this.dgvLoans.ColumnHeadersDefaultCellStyle.Font=new System.Drawing.Font("Arial",9F,System.Drawing.FontStyle.Bold);
            this.dgvLoans.SelectionChanged+=new System.EventHandler(this.dgvLoans_SelectionChanged);

            // grpRepay
            this.grpRepay.Font=new System.Drawing.Font("Arial",9F,System.Drawing.FontStyle.Bold);
            this.grpRepay.Location=new System.Drawing.Point(10,530); this.grpRepay.Size=new System.Drawing.Size(500,110);
            this.grpRepay.Text="Record Repayment"; this.grpRepay.Visible=false;
            this.grpRepay.Controls.AddRange(new System.Windows.Forms.Control[]{lblRepayAmt,nudRepay,lblRepayDate,dtpRepay,btnRepay,lblRepayStatus});

            this.lblRepayAmt.Font=new System.Drawing.Font("Arial",9F,System.Drawing.FontStyle.Regular);
            this.lblRepayAmt.Location=new System.Drawing.Point(10,28); this.lblRepayAmt.Size=new System.Drawing.Size(130,20); this.lblRepayAmt.Text="Amount (UGX):";
            this.nudRepay.Font=new System.Drawing.Font("Arial",10F); this.nudRepay.Location=new System.Drawing.Point(148,26);
            this.nudRepay.Size=new System.Drawing.Size(160,26); this.nudRepay.Minimum=1000; this.nudRepay.Maximum=10000000; this.nudRepay.ThousandsSeparator=true;

            this.lblRepayDate.Font=new System.Drawing.Font("Arial",9F,System.Drawing.FontStyle.Regular);
            this.lblRepayDate.Location=new System.Drawing.Point(10,60); this.lblRepayDate.Size=new System.Drawing.Size(130,20); this.lblRepayDate.Text="Date:";
            this.dtpRepay.Font=new System.Drawing.Font("Arial",10F); this.dtpRepay.Location=new System.Drawing.Point(148,58);
            this.dtpRepay.Size=new System.Drawing.Size(160,26); this.dtpRepay.Format=System.Windows.Forms.DateTimePickerFormat.Short;

            this.btnRepay.BackColor=System.Drawing.Color.FromArgb(31,73,125); this.btnRepay.ForeColor=System.Drawing.Color.White;
            this.btnRepay.FlatStyle=System.Windows.Forms.FlatStyle.Flat; this.btnRepay.Font=new System.Drawing.Font("Arial",9F,System.Drawing.FontStyle.Bold);
            this.btnRepay.Location=new System.Drawing.Point(320,26); this.btnRepay.Size=new System.Drawing.Size(150,30);
            this.btnRepay.Text="Record Repayment"; this.btnRepay.Click+=new System.EventHandler(this.btnRepay_Click);

            this.lblRepayStatus.Font=new System.Drawing.Font("Arial",8.5F,System.Drawing.FontStyle.Bold);
            this.lblRepayStatus.Location=new System.Drawing.Point(10,80); this.lblRepayStatus.Size=new System.Drawing.Size(470,20); this.lblRepayStatus.Text="";

            this.BackColor=System.Drawing.Color.White;
            this.ClientSize=new System.Drawing.Size(860,650);
            this.Controls.AddRange(new System.Windows.Forms.Control[]{lblTitle,grpApply,grpSchedule,grpLoans,grpRepay});
            this.Name="LoanForm"; this.Text="Loan Management";
            this.grpApply.ResumeLayout(false); this.grpSchedule.ResumeLayout(false);
            this.grpLoans.ResumeLayout(false); this.grpRepay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudPrincipal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeeks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepay)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpApply, grpSchedule, grpLoans, grpRepay;
        private System.Windows.Forms.Label lblMember, lblPrincipal, lblPurpose, lblRate, lblStart, lblWeeks;
        private System.Windows.Forms.Label lblCalcStatus, lblScheduleSummary, lblRepayAmt, lblRepayDate, lblRepayStatus;
        private System.Windows.Forms.ComboBox cmbMember;
        private System.Windows.Forms.NumericUpDown nudPrincipal, nudRate, nudWeeks, nudRepay;
        private System.Windows.Forms.TextBox txtPurpose;
        private System.Windows.Forms.DateTimePicker dtpStart, dtpRepay;
        private System.Windows.Forms.Button btnCalculate, btnConfirm, btnRepay;
        private System.Windows.Forms.DataGridView dgvSchedule, dgvLoans;
    }
}
