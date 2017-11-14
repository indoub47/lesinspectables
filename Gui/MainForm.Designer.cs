namespace Gui
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblKoefOverdue = new System.Windows.Forms.Label();
            this.lblKoefMain = new System.Windows.Forms.Label();
            this.lblKoef064 = new System.Windows.Forms.Label();
            this.nudKoefOverdue = new System.Windows.Forms.NumericUpDown();
            this.nudKoefMain = new System.Windows.Forms.NumericUpDown();
            this.nudKoef064 = new System.Windows.Forms.NumericUpDown();
            this.lblLinijos = new System.Windows.Forms.Label();
            this.chlbLinijos = new System.Windows.Forms.CheckedListBox();
            this.lblDatai = new System.Windows.Forms.Label();
            this.dtpDatai = new System.Windows.Forms.DateTimePicker();
            this.lblSkodai = new System.Windows.Forms.Label();
            this.lblLiko = new System.Windows.Forms.Label();
            this.nudLiko = new System.Windows.Forms.NumericUpDown();
            this.chlbSkodai = new System.Windows.Forms.CheckedListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnRepaint = new System.Windows.Forms.Button();
            this.grbFiltrai = new System.Windows.Forms.GroupBox();
            this.chbNepagr = new System.Windows.Forms.CheckBox();
            this.grbKoeficientai = new System.Windows.Forms.GroupBox();
            this.pb = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.slblCollected = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnExportCollected = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudKoefOverdue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKoefMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKoef064)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLiko)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grbFiltrai.SuspendLayout();
            this.grbKoeficientai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblKoefOverdue
            // 
            this.lblKoefOverdue.AutoSize = true;
            this.lblKoefOverdue.Location = new System.Drawing.Point(8, 20);
            this.lblKoefOverdue.Name = "lblKoefOverdue";
            this.lblKoefOverdue.Size = new System.Drawing.Size(46, 13);
            this.lblKoefOverdue.TabIndex = 18;
            this.lblKoefOverdue.Text = "pradelsti";
            this.lblKoefOverdue.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblKoefMain
            // 
            this.lblKoefMain.AutoSize = true;
            this.lblKoefMain.Location = new System.Drawing.Point(8, 98);
            this.lblKoefMain.Name = "lblKoefMain";
            this.lblKoefMain.Size = new System.Drawing.Size(43, 13);
            this.lblKoefMain.TabIndex = 16;
            this.lblKoefMain.Text = "pagr. k.";
            this.lblKoefMain.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblKoef064
            // 
            this.lblKoef064.AutoSize = true;
            this.lblKoef064.Location = new System.Drawing.Point(8, 59);
            this.lblKoef064.Name = "lblKoef064";
            this.lblKoef064.Size = new System.Drawing.Size(28, 13);
            this.lblKoef064.TabIndex = 15;
            this.lblKoef064.Text = "06.4";
            this.lblKoef064.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // nudKoefOverdue
            // 
            this.nudKoefOverdue.Location = new System.Drawing.Point(11, 36);
            this.nudKoefOverdue.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudKoefOverdue.Name = "nudKoefOverdue";
            this.nudKoefOverdue.Size = new System.Drawing.Size(42, 20);
            this.nudKoefOverdue.TabIndex = 13;
            this.nudKoefOverdue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudKoefOverdue.ValueChanged += new System.EventHandler(this.nudKoefOverdue_ValueChanged);
            // 
            // nudKoefMain
            // 
            this.nudKoefMain.Location = new System.Drawing.Point(11, 114);
            this.nudKoefMain.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudKoefMain.Name = "nudKoefMain";
            this.nudKoefMain.Size = new System.Drawing.Size(42, 20);
            this.nudKoefMain.TabIndex = 11;
            this.nudKoefMain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudKoefMain.ValueChanged += new System.EventHandler(this.nudKoefMain_ValueChanged);
            // 
            // nudKoef064
            // 
            this.nudKoef064.Location = new System.Drawing.Point(11, 75);
            this.nudKoef064.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudKoef064.Name = "nudKoef064";
            this.nudKoef064.Size = new System.Drawing.Size(42, 20);
            this.nudKoef064.TabIndex = 10;
            this.nudKoef064.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudKoef064.ValueChanged += new System.EventHandler(this.nudKoef064_ValueChanged);
            // 
            // lblLinijos
            // 
            this.lblLinijos.AutoSize = true;
            this.lblLinijos.Location = new System.Drawing.Point(8, 16);
            this.lblLinijos.Name = "lblLinijos";
            this.lblLinijos.Size = new System.Drawing.Size(32, 13);
            this.lblLinijos.TabIndex = 8;
            this.lblLinijos.Text = "linijos";
            // 
            // chlbLinijos
            // 
            this.chlbLinijos.CheckOnClick = true;
            this.chlbLinijos.FormattingEnabled = true;
            this.chlbLinijos.Location = new System.Drawing.Point(11, 32);
            this.chlbLinijos.Name = "chlbLinijos";
            this.chlbLinijos.Size = new System.Drawing.Size(83, 79);
            this.chlbLinijos.TabIndex = 7;
            // 
            // lblDatai
            // 
            this.lblDatai.AutoSize = true;
            this.lblDatai.Location = new System.Drawing.Point(3, 38);
            this.lblDatai.Name = "lblDatai";
            this.lblDatai.Size = new System.Drawing.Size(32, 13);
            this.lblDatai.TabIndex = 6;
            this.lblDatai.Text = "Datai";
            // 
            // dtpDatai
            // 
            this.dtpDatai.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDatai.Location = new System.Drawing.Point(6, 54);
            this.dtpDatai.Name = "dtpDatai";
            this.dtpDatai.Size = new System.Drawing.Size(83, 20);
            this.dtpDatai.TabIndex = 5;
            // 
            // lblSkodai
            // 
            this.lblSkodai.AutoSize = true;
            this.lblSkodai.Location = new System.Drawing.Point(8, 125);
            this.lblSkodai.Name = "lblSkodai";
            this.lblSkodai.Size = new System.Drawing.Size(63, 13);
            this.lblSkodai.TabIndex = 4;
            this.lblSkodai.Text = "sąlyg. kodai";
            this.lblSkodai.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblLiko
            // 
            this.lblLiko.AutoSize = true;
            this.lblLiko.Location = new System.Drawing.Point(8, 228);
            this.lblLiko.Name = "lblLiko";
            this.lblLiko.Size = new System.Drawing.Size(52, 13);
            this.lblLiko.TabIndex = 3;
            this.lblLiko.Text = "liko dienų";
            this.lblLiko.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // nudLiko
            // 
            this.nudLiko.Location = new System.Drawing.Point(11, 244);
            this.nudLiko.Maximum = new decimal(new int[] {
            183,
            0,
            0,
            0});
            this.nudLiko.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudLiko.Name = "nudLiko";
            this.nudLiko.Size = new System.Drawing.Size(55, 20);
            this.nudLiko.TabIndex = 2;
            this.nudLiko.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chlbSkodai
            // 
            this.chlbSkodai.CheckOnClick = true;
            this.chlbSkodai.FormattingEnabled = true;
            this.chlbSkodai.Location = new System.Drawing.Point(11, 141);
            this.chlbSkodai.Name = "chlbSkodai";
            this.chlbSkodai.Size = new System.Drawing.Size(83, 34);
            this.chlbSkodai.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.btnExportCollected);
            this.splitContainer1.Panel1.Controls.Add(this.btnRepaint);
            this.splitContainer1.Panel1.Controls.Add(this.grbFiltrai);
            this.splitContainer1.Panel1.Controls.Add(this.grbKoeficientai);
            this.splitContainer1.Panel1.Controls.Add(this.dtpDatai);
            this.splitContainer1.Panel1.Controls.Add(this.lblDatai);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pb);
            this.splitContainer1.Size = new System.Drawing.Size(1362, 590);
            this.splitContainer1.SplitterDistance = 122;
            this.splitContainer1.TabIndex = 2;
            // 
            // btnRepaint
            // 
            this.btnRepaint.Location = new System.Drawing.Point(6, 8);
            this.btnRepaint.Name = "btnRepaint";
            this.btnRepaint.Size = new System.Drawing.Size(105, 25);
            this.btnRepaint.TabIndex = 21;
            this.btnRepaint.Text = "Perbraižyti";
            this.btnRepaint.UseVisualStyleBackColor = true;
            this.btnRepaint.Click += new System.EventHandler(this.btnRepaint_Click);
            // 
            // grbFiltrai
            // 
            this.grbFiltrai.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grbFiltrai.Controls.Add(this.chbNepagr);
            this.grbFiltrai.Controls.Add(this.chlbSkodai);
            this.grbFiltrai.Controls.Add(this.nudLiko);
            this.grbFiltrai.Controls.Add(this.lblLinijos);
            this.grbFiltrai.Controls.Add(this.lblSkodai);
            this.grbFiltrai.Controls.Add(this.lblLiko);
            this.grbFiltrai.Controls.Add(this.chlbLinijos);
            this.grbFiltrai.Location = new System.Drawing.Point(6, 84);
            this.grbFiltrai.Name = "grbFiltrai";
            this.grbFiltrai.Size = new System.Drawing.Size(106, 272);
            this.grbFiltrai.TabIndex = 20;
            this.grbFiltrai.TabStop = false;
            this.grbFiltrai.Text = "Filtrai";
            // 
            // chbNepagr
            // 
            this.chbNepagr.AutoSize = true;
            this.chbNepagr.Location = new System.Drawing.Point(11, 199);
            this.chbNepagr.Name = "chbNepagr";
            this.chbNepagr.Size = new System.Drawing.Size(74, 17);
            this.chbNepagr.TabIndex = 9;
            this.chbNepagr.Text = "nepagr. k.";
            this.chbNepagr.UseVisualStyleBackColor = true;
            // 
            // grbKoeficientai
            // 
            this.grbKoeficientai.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grbKoeficientai.Controls.Add(this.nudKoefOverdue);
            this.grbKoeficientai.Controls.Add(this.nudKoef064);
            this.grbKoeficientai.Controls.Add(this.nudKoefMain);
            this.grbKoeficientai.Controls.Add(this.lblKoefOverdue);
            this.grbKoeficientai.Controls.Add(this.lblKoefMain);
            this.grbKoeficientai.Controls.Add(this.lblKoef064);
            this.grbKoeficientai.Location = new System.Drawing.Point(6, 368);
            this.grbKoeficientai.Name = "grbKoeficientai";
            this.grbKoeficientai.Size = new System.Drawing.Size(106, 144);
            this.grbKoeficientai.TabIndex = 19;
            this.grbKoeficientai.TabStop = false;
            this.grbKoeficientai.Text = "Koeficientai";
            // 
            // pb
            // 
            this.pb.BackColor = System.Drawing.Color.Transparent;
            this.pb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb.Location = new System.Drawing.Point(0, 0);
            this.pb.Name = "pb";
            this.pb.Padding = new System.Windows.Forms.Padding(5);
            this.pb.Size = new System.Drawing.Size(1236, 590);
            this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            this.pb.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_Paint);
            this.pb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_MouseDown);
            //this.pb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_MouseMove);
            this.pb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1362, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slblCollected});
            this.statusStrip1.Location = new System.Drawing.Point(0, 614);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1362, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // slblCollected
            // 
            this.slblCollected.Name = "slblCollected";
            this.slblCollected.Size = new System.Drawing.Size(0, 17);
            this.slblCollected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnExportCollected
            // 
            this.btnExportCollected.Location = new System.Drawing.Point(6, 518);
            this.btnExportCollected.Name = "btnExportCollected";
            this.btnExportCollected.Size = new System.Drawing.Size(106, 23);
            this.btnExportCollected.TabIndex = 22;
            this.btnExportCollected.Text = "Eksportuoti";
            this.btnExportCollected.UseVisualStyleBackColor = true;
            this.btnExportCollected.Click += new System.EventHandler(this.btnExportCollected_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 636);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "Les Inspectables";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudKoefOverdue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKoefMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKoef064)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLiko)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grbFiltrai.ResumeLayout(false);
            this.grbFiltrai.PerformLayout();
            this.grbKoeficientai.ResumeLayout(false);
            this.grbKoeficientai.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckedListBox chlbSkodai;
        private System.Windows.Forms.NumericUpDown nudLiko;
        private System.Windows.Forms.Label lblSkodai;
        private System.Windows.Forms.Label lblLiko;
        private System.Windows.Forms.Label lblDatai;
        private System.Windows.Forms.DateTimePicker dtpDatai;
        private System.Windows.Forms.CheckedListBox chlbLinijos;
        private System.Windows.Forms.NumericUpDown nudKoefMain;
        private System.Windows.Forms.NumericUpDown nudKoef064;
        private System.Windows.Forms.Label lblLinijos;
        private System.Windows.Forms.NumericUpDown nudKoefOverdue;
        private System.Windows.Forms.Label lblKoefOverdue;
        private System.Windows.Forms.Label lblKoefMain;
        private System.Windows.Forms.Label lblKoef064;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnRepaint;
        private System.Windows.Forms.GroupBox grbFiltrai;
        private System.Windows.Forms.GroupBox grbKoeficientai;
        private System.Windows.Forms.CheckBox chbNepagr;
        private System.Windows.Forms.ToolStripStatusLabel slblCollected;
        private System.Windows.Forms.Button btnExportCollected;
    }
}

