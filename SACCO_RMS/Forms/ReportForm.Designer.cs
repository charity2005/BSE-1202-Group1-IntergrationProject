namespace SACCO_RMS.Forms
{
    partial class ReportForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.lblTitle   = new System.Windows.Forms.Label();
            this.lblPickLbl = new System.Windows.Forms.Label();
            this.dtpWeek    = new System.Windows.Forms.DateTimePicker();
            this.btnGenerate= new System.Windows.Forms.Button();
            this.lblPeriod  = new System.Windows.Forms.Label();
            this.pnlTotals  = new System.Windows.Forms.Panel();
            this.pnlC = new System.Windows.Forms.Panel(); this.lblCTitle = new System.Windows.Forms.Label(); this.lblTC = new System.Windows.Forms.Label();
            this.pnlD = new System.Windows.Forms.Panel(); this.lblDTitle = new System.Windows.Forms.Label(); this.lblTD = new System.Windows.Forms.Label();
            this.pnlR = new System.Windows.Forms.Panel(); this.lblRTitle = new System.Windows.Forms.Label(); this.lblTR = new System.Windows.Forms.Label();
            this.pnlB = new System.Windows.Forms.Panel(); this.lblBTitle = new System.Windows.Forms.Label(); this.lblBal = new System.Windows.Forms.Label();
            this.dgvTx      = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTx)).BeginInit();
            this.pnlTotals.SuspendLayout();
            this.SuspendLayout();

            this.lblTitle.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 6); this.lblTitle.Size = new System.Drawing.Size(500, 28);
            this.lblTitle.Text = "Weekly Financial Summary  (FR-10)";

            this.lblPickLbl.Font = new System.Drawing.Font("Arial", 9F);
            this.lblPickLbl.Location = new System.Drawing.Point(10, 44); this.lblPickLbl.Size = new System.Drawing.Size(160, 22); this.lblPickLbl.Text = "Select any date in week:";

            this.dtpWeek.Font = new System.Drawing.Font("Arial", 10F); this.dtpWeek.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpWeek.Location = new System.Drawing.Point(175, 42); this.dtpWeek.Size = new System.Drawing.Size(160, 26);

            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(31, 73, 125); this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnGenerate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenerate.Location = new System.Drawing.Point(344, 41); this.btnGenerate.Size = new System.Drawing.Size(130, 28);
            this.btnGenerate.Text = "Generate Report"; this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);

            this.lblPeriod.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Italic); this.lblPeriod.ForeColor = System.Drawing.Color.Gray;
            this.lblPeriod.Location = new System.Drawing.Point(484, 46); this.lblPeriod.Size = new System.Drawing.Size(360, 20); this.lblPeriod.Text = "";

            // Tiles panel
            this.pnlTotals.Location = new System.Drawing.Point(10, 76); this.pnlTotals.Size = new System.Drawing.Size(840, 90); this.pnlTotals.Visible = false;
            var tPanels = new[]{pnlC,pnlD,pnlR,pnlB};
            var tTitles = new[]{lblCTitle,lblDTitle,lblRTitle,lblBTitle};
            var tVals   = new[]{lblTC,lblTD,lblTR,lblBal};
            var tLabels = new[]{"Total Contributions","Total Disbursed","Repayments Received","Fund Movement"};
            var tColors = new System.Drawing.Color[]{System.Drawing.Color.FromArgb(0,128,0),System.Drawing.Color.FromArgb(180,100,0),System.Drawing.Color.FromArgb(31,73,125),System.Drawing.Color.FromArgb(100,0,120)};
            for(int i=0;i<4;i++){
                tPanels[i].BackColor=tColors[i]; tPanels[i].Location=new System.Drawing.Point(i*210,0); tPanels[i].Size=new System.Drawing.Size(200,86);
                tTitles[i].Font=new System.Drawing.Font("Arial",8F); tTitles[i].ForeColor=System.Drawing.Color.FromArgb(210,230,255);
                tTitles[i].Location=new System.Drawing.Point(8,8); tTitles[i].Size=new System.Drawing.Size(184,18); tTitles[i].Text=tLabels[i];
                tVals[i].Font=new System.Drawing.Font("Arial",14F,System.Drawing.FontStyle.Bold); tVals[i].ForeColor=System.Drawing.Color.White;
                tVals[i].Location=new System.Drawing.Point(8,28); tVals[i].Size=new System.Drawing.Size(184,40); tVals[i].Text="—";
                tPanels[i].Controls.Add(tTitles[i]); tPanels[i].Controls.Add(tVals[i]);
                this.pnlTotals.Controls.Add(tPanels[i]); }

            this.dgvTx.AllowUserToAddRows=false; this.dgvTx.AllowUserToDeleteRows=false; this.dgvTx.ReadOnly=true;
            this.dgvTx.AutoSizeColumnsMode=System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTx.SelectionMode=System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTx.RowHeadersVisible=false; this.dgvTx.BackgroundColor=System.Drawing.Color.White;
            this.dgvTx.Font=new System.Drawing.Font("Arial",9F);
            this.dgvTx.Location=new System.Drawing.Point(10,174);
            this.dgvTx.Anchor=System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Right|System.Windows.Forms.AnchorStyles.Bottom;
            this.dgvTx.Size=new System.Drawing.Size(840,380);
            this.dgvTx.ColumnHeadersDefaultCellStyle.BackColor=System.Drawing.Color.FromArgb(31,73,125);
            this.dgvTx.ColumnHeadersDefaultCellStyle.ForeColor=System.Drawing.Color.White;
            this.dgvTx.ColumnHeadersDefaultCellStyle.Font=new System.Drawing.Font("Arial",9F,System.Drawing.FontStyle.Bold);

            this.BackColor=System.Drawing.Color.White;
            this.ClientSize=new System.Drawing.Size(860,570);
            this.Controls.AddRange(new System.Windows.Forms.Control[]{lblTitle,lblPickLbl,dtpWeek,btnGenerate,lblPeriod,pnlTotals,dgvTx});
            this.Name="ReportForm"; this.Text="Weekly Financial Summary";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTx)).EndInit();
            this.pnlTotals.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle, lblPickLbl, lblPeriod;
        private System.Windows.Forms.DateTimePicker dtpWeek;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Panel pnlTotals, pnlC, pnlD, pnlR, pnlB;
        private System.Windows.Forms.Label lblCTitle, lblDTitle, lblRTitle, lblBTitle;
        private System.Windows.Forms.Label lblTC, lblTD, lblTR, lblBal;
        private System.Windows.Forms.DataGridView dgvTx;
    }
}
