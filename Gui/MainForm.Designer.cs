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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnExportCollected = new System.Windows.Forms.Button();
            this.btnRepaint = new System.Windows.Forms.Button();
            this.grbFiltrai = new System.Windows.Forms.GroupBox();
            this.chbNepagr = new System.Windows.Forms.CheckBox();
            this.grbKoeficientai = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabChart = new System.Windows.Forms.TabPage();
            this.pb = new System.Windows.Forms.PictureBox();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbxDangerParameters = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nudY2 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudX2 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudY1 = new System.Windows.Forms.NumericUpDown();
            this.nudX1 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudY0 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudX0 = new System.Windows.Forms.NumericUpDown();
            this.btnChangeHelpDb = new System.Windows.Forms.Button();
            this.txbHelperDbPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChangeMainDb = new System.Windows.Forms.Button();
            this.txbMainDbPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.slblCollected = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
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
            this.tabControl1.SuspendLayout();
            this.tabChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.tabOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDangerParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX0)).BeginInit();
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
            this.lblDatai.Location = new System.Drawing.Point(3, 59);
            this.lblDatai.Name = "lblDatai";
            this.lblDatai.Size = new System.Drawing.Size(32, 13);
            this.lblDatai.TabIndex = 6;
            this.lblDatai.Text = "Datai";
            // 
            // dtpDatai
            // 
            this.dtpDatai.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDatai.Location = new System.Drawing.Point(8, 75);
            this.dtpDatai.Name = "dtpDatai";
            this.dtpDatai.Size = new System.Drawing.Size(109, 20);
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
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.progressBar);
            this.splitContainer1.Panel1.Controls.Add(this.btnExportCollected);
            this.splitContainer1.Panel1.Controls.Add(this.btnRepaint);
            this.splitContainer1.Panel1.Controls.Add(this.grbFiltrai);
            this.splitContainer1.Panel1.Controls.Add(this.grbKoeficientai);
            this.splitContainer1.Panel1.Controls.Add(this.dtpDatai);
            this.splitContainer1.Panel1.Controls.Add(this.lblDatai);
            this.splitContainer1.Panel1MinSize = 80;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2MinSize = 800;
            this.splitContainer1.Size = new System.Drawing.Size(979, 614);
            this.splitContainer1.SplitterDistance = 120;
            this.splitContainer1.TabIndex = 2;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(4, 3);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(113, 12);
            this.progressBar.TabIndex = 5;
            // 
            // btnExportCollected
            // 
            this.btnExportCollected.Enabled = false;
            this.btnExportCollected.Location = new System.Drawing.Point(6, 539);
            this.btnExportCollected.Name = "btnExportCollected";
            this.btnExportCollected.Size = new System.Drawing.Size(111, 23);
            this.btnExportCollected.TabIndex = 22;
            this.btnExportCollected.Text = "Eksportuoti";
            this.btnExportCollected.UseVisualStyleBackColor = true;
            this.btnExportCollected.Click += new System.EventHandler(this.btnExportCollected_Click);
            // 
            // btnRepaint
            // 
            this.btnRepaint.Location = new System.Drawing.Point(4, 21);
            this.btnRepaint.Name = "btnRepaint";
            this.btnRepaint.Size = new System.Drawing.Size(113, 25);
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
            this.grbFiltrai.Location = new System.Drawing.Point(6, 105);
            this.grbFiltrai.Name = "grbFiltrai";
            this.grbFiltrai.Size = new System.Drawing.Size(111, 272);
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
            this.grbKoeficientai.Location = new System.Drawing.Point(6, 389);
            this.grbKoeficientai.Name = "grbKoeficientai";
            this.grbKoeficientai.Size = new System.Drawing.Size(111, 144);
            this.grbKoeficientai.TabIndex = 19;
            this.grbKoeficientai.TabStop = false;
            this.grbKoeficientai.Text = "Koeficientai";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabChart);
            this.tabControl1.Controls.Add(this.tabOptions);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(855, 614);
            this.tabControl1.TabIndex = 1;
            // 
            // tabChart
            // 
            this.tabChart.Controls.Add(this.pb);
            this.tabChart.Location = new System.Drawing.Point(4, 22);
            this.tabChart.Name = "tabChart";
            this.tabChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabChart.Size = new System.Drawing.Size(847, 588);
            this.tabChart.TabIndex = 0;
            this.tabChart.Text = "Suvirinimų tikrinimai";
            this.tabChart.UseVisualStyleBackColor = true;
            // 
            // pb
            // 
            this.pb.BackColor = System.Drawing.Color.Transparent;
            this.pb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb.Location = new System.Drawing.Point(3, 3);
            this.pb.Name = "pb";
            this.pb.Padding = new System.Windows.Forms.Padding(5);
            this.pb.Size = new System.Drawing.Size(841, 582);
            this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb.TabIndex = 1;
            this.pb.TabStop = false;
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.groupBox1);
            this.tabOptions.Controls.Add(this.btnChangeHelpDb);
            this.tabOptions.Controls.Add(this.txbHelperDbPath);
            this.tabOptions.Controls.Add(this.label2);
            this.tabOptions.Controls.Add(this.btnChangeMainDb);
            this.tabOptions.Controls.Add(this.txbMainDbPath);
            this.tabOptions.Controls.Add(this.label1);
            this.tabOptions.Location = new System.Drawing.Point(4, 22);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabOptions.Size = new System.Drawing.Size(847, 588);
            this.tabOptions.TabIndex = 1;
            this.tabOptions.Text = "Nustatymai";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pbxDangerParameters);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.nudY2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.nudX2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.nudY1);
            this.groupBox1.Controls.Add(this.nudX1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.nudY0);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nudX0);
            this.groupBox1.Location = new System.Drawing.Point(23, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(753, 123);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Koeficientai";
            // 
            // pbxDangerParameters
            // 
            this.pbxDangerParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxDangerParameters.InitialImage = null;
            this.pbxDangerParameters.Location = new System.Drawing.Point(333, 14);
            this.pbxDangerParameters.Name = "pbxDangerParameters";
            this.pbxDangerParameters.Size = new System.Drawing.Size(414, 103);
            this.pbxDangerParameters.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxDangerParameters.TabIndex = 15;
            this.pbxDangerParameters.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(239, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Y2";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(133, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "X1";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudY2
            // 
            this.nudY2.Location = new System.Drawing.Point(265, 45);
            this.nudY2.Name = "nudY2";
            this.nudY2.Size = new System.Drawing.Size(51, 20);
            this.nudY2.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(239, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "X2";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudX2
            // 
            this.nudX2.Location = new System.Drawing.Point(265, 19);
            this.nudX2.Name = "nudX2";
            this.nudX2.ReadOnly = true;
            this.nudX2.Size = new System.Drawing.Size(51, 20);
            this.nudX2.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(131, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Y1";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudY1
            // 
            this.nudY1.Location = new System.Drawing.Point(157, 46);
            this.nudY1.Name = "nudY1";
            this.nudY1.Size = new System.Drawing.Size(51, 20);
            this.nudY1.TabIndex = 8;
            // 
            // nudX1
            // 
            this.nudX1.Location = new System.Drawing.Point(157, 19);
            this.nudX1.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudX1.Minimum = new decimal(new int[] {
            183,
            0,
            0,
            -2147483648});
            this.nudX1.Name = "nudX1";
            this.nudX1.Size = new System.Drawing.Size(51, 20);
            this.nudX1.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Y0";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudY0
            // 
            this.nudY0.BackColor = System.Drawing.SystemColors.Window;
            this.nudY0.Location = new System.Drawing.Point(47, 46);
            this.nudY0.Name = "nudY0";
            this.nudY0.ReadOnly = true;
            this.nudY0.Size = new System.Drawing.Size(51, 20);
            this.nudY0.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "X0";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudX0
            // 
            this.nudX0.Location = new System.Drawing.Point(47, 19);
            this.nudX0.Minimum = new decimal(new int[] {
            183,
            0,
            0,
            -2147483648});
            this.nudX0.Name = "nudX0";
            this.nudX0.ReadOnly = true;
            this.nudX0.Size = new System.Drawing.Size(51, 20);
            this.nudX0.TabIndex = 2;
            this.nudX0.Value = new decimal(new int[] {
            183,
            0,
            0,
            -2147483648});
            // 
            // btnChangeHelpDb
            // 
            this.btnChangeHelpDb.Location = new System.Drawing.Point(701, 50);
            this.btnChangeHelpDb.Name = "btnChangeHelpDb";
            this.btnChangeHelpDb.Size = new System.Drawing.Size(75, 23);
            this.btnChangeHelpDb.TabIndex = 5;
            this.btnChangeHelpDb.Text = "Keisti";
            this.btnChangeHelpDb.UseVisualStyleBackColor = true;
            this.btnChangeHelpDb.Click += new System.EventHandler(this.btnChangeHelpDb_Click);
            // 
            // txbHelperDbPath
            // 
            this.txbHelperDbPath.Location = new System.Drawing.Point(98, 52);
            this.txbHelperDbPath.Name = "txbHelperDbPath";
            this.txbHelperDbPath.Size = new System.Drawing.Size(597, 20);
            this.txbHelperDbPath.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pagalbinė DB";
            // 
            // btnChangeMainDb
            // 
            this.btnChangeMainDb.Location = new System.Drawing.Point(701, 11);
            this.btnChangeMainDb.Name = "btnChangeMainDb";
            this.btnChangeMainDb.Size = new System.Drawing.Size(75, 23);
            this.btnChangeMainDb.TabIndex = 2;
            this.btnChangeMainDb.Text = "Keisti";
            this.btnChangeMainDb.UseVisualStyleBackColor = true;
            this.btnChangeMainDb.Click += new System.EventHandler(this.btnChangeMainDb_Click);
            // 
            // txbMainDbPath
            // 
            this.txbMainDbPath.Location = new System.Drawing.Point(98, 13);
            this.txbMainDbPath.Name = "txbMainDbPath";
            this.txbMainDbPath.Size = new System.Drawing.Size(597, 20);
            this.txbMainDbPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pagrindinė DB";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slblCollected});
            this.statusStrip1.Location = new System.Drawing.Point(0, 614);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(979, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // slblCollected
            // 
            this.slblCollected.Name = "slblCollected";
            this.slblCollected.Size = new System.Drawing.Size(0, 17);
            this.slblCollected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog";
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 636);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "Les Inspectables";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
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
            this.tabControl1.ResumeLayout(false);
            this.tabChart.ResumeLayout(false);
            this.tabChart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.tabOptions.ResumeLayout(false);
            this.tabOptions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDangerParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX0)).EndInit();
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
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnRepaint;
        private System.Windows.Forms.GroupBox grbFiltrai;
        private System.Windows.Forms.GroupBox grbKoeficientai;
        private System.Windows.Forms.CheckBox chbNepagr;
        private System.Windows.Forms.ToolStripStatusLabel slblCollected;
        private System.Windows.Forms.Button btnExportCollected;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabChart;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.Button btnChangeHelpDb;
        private System.Windows.Forms.TextBox txbHelperDbPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChangeMainDb;
        private System.Windows.Forms.TextBox txbMainDbPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pbxDangerParameters;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudY2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudX2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudY1;
        private System.Windows.Forms.NumericUpDown nudX1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudY0;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudX0;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

