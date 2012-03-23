namespace BiomePainter
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveCurrentRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadCurrentRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboveCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.belowCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftOfCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightOfCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRegionByCoordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutBiomePainterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstRegions = new System.Windows.Forms.ListBox();
            this.chkShowMap = new System.Windows.Forms.CheckBox();
            this.chkShowBiomes = new System.Windows.Forms.CheckBox();
            this.chkShowSelection = new System.Windows.Forms.CheckBox();
            this.chkShowToolTips = new System.Windows.Forms.CheckBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.btnInvertSelection = new System.Windows.Forms.Button();
            this.radRoundBrush = new System.Windows.Forms.RadioButton();
            this.radSquareBrush = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectChunks = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblBrushDiameter = new System.Windows.Forms.Label();
            this.trackBrushDiameter = new System.Windows.Forms.TrackBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackPanVertical = new System.Windows.Forms.TrackBar();
            this.trackPanHorizontal = new System.Windows.Forms.TrackBar();
            this.lblMagnification = new System.Windows.Forms.Label();
            this.trackMagnification = new System.Windows.Forms.TrackBar();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnReplace = new System.Windows.Forms.Button();
            this.cmbReplace2 = new System.Windows.Forms.ComboBox();
            this.cmbReplace1 = new System.Windows.Forms.ComboBox();
            this.btnFill = new System.Windows.Forms.Button();
            this.cmbFill = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.imgRegion = new BitmapSelector.BitmapSelector();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBrushDiameter)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackPanVertical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPanHorizontal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMagnification)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(996, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openWorldToolStripMenuItem,
            this.closeWorldToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveCurrentRegionToolStripMenuItem,
            this.reloadCurrentRegionToolStripMenuItem,
            this.loadRegionToolStripMenuItem,
            this.loadRegionByCoordsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openWorldToolStripMenuItem
            // 
            this.openWorldToolStripMenuItem.Name = "openWorldToolStripMenuItem";
            this.openWorldToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.openWorldToolStripMenuItem.Text = "Open world";
            this.openWorldToolStripMenuItem.Click += new System.EventHandler(this.openWorldToolStripMenuItem_Click);
            // 
            // closeWorldToolStripMenuItem
            // 
            this.closeWorldToolStripMenuItem.Name = "closeWorldToolStripMenuItem";
            this.closeWorldToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.closeWorldToolStripMenuItem.Text = "Close world";
            this.closeWorldToolStripMenuItem.Click += new System.EventHandler(this.closeWorldToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(224, 6);
            // 
            // saveCurrentRegionToolStripMenuItem
            // 
            this.saveCurrentRegionToolStripMenuItem.Name = "saveCurrentRegionToolStripMenuItem";
            this.saveCurrentRegionToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.saveCurrentRegionToolStripMenuItem.Text = "Save current region";
            this.saveCurrentRegionToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentRegionToolStripMenuItem_Click);
            // 
            // reloadCurrentRegionToolStripMenuItem
            // 
            this.reloadCurrentRegionToolStripMenuItem.Name = "reloadCurrentRegionToolStripMenuItem";
            this.reloadCurrentRegionToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.reloadCurrentRegionToolStripMenuItem.Text = "Reload current region";
            this.reloadCurrentRegionToolStripMenuItem.Click += new System.EventHandler(this.reloadCurrentRegionToolStripMenuItem_Click);
            // 
            // loadRegionToolStripMenuItem
            // 
            this.loadRegionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboveCurrentToolStripMenuItem,
            this.belowCurrentToolStripMenuItem,
            this.leftOfCurrentToolStripMenuItem,
            this.rightOfCurrentToolStripMenuItem});
            this.loadRegionToolStripMenuItem.Name = "loadRegionToolStripMenuItem";
            this.loadRegionToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.loadRegionToolStripMenuItem.Text = "Load next region";
            // 
            // aboveCurrentToolStripMenuItem
            // 
            this.aboveCurrentToolStripMenuItem.Name = "aboveCurrentToolStripMenuItem";
            this.aboveCurrentToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.aboveCurrentToolStripMenuItem.Text = "Above current";
            this.aboveCurrentToolStripMenuItem.Click += new System.EventHandler(this.aboveCurrentToolStripMenuItem_Click);
            // 
            // belowCurrentToolStripMenuItem
            // 
            this.belowCurrentToolStripMenuItem.Name = "belowCurrentToolStripMenuItem";
            this.belowCurrentToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.belowCurrentToolStripMenuItem.Text = "Below current";
            this.belowCurrentToolStripMenuItem.Click += new System.EventHandler(this.belowCurrentToolStripMenuItem_Click);
            // 
            // leftOfCurrentToolStripMenuItem
            // 
            this.leftOfCurrentToolStripMenuItem.Name = "leftOfCurrentToolStripMenuItem";
            this.leftOfCurrentToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.leftOfCurrentToolStripMenuItem.Text = "Left of current";
            this.leftOfCurrentToolStripMenuItem.Click += new System.EventHandler(this.leftOfCurrentToolStripMenuItem_Click);
            // 
            // rightOfCurrentToolStripMenuItem
            // 
            this.rightOfCurrentToolStripMenuItem.Name = "rightOfCurrentToolStripMenuItem";
            this.rightOfCurrentToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.rightOfCurrentToolStripMenuItem.Text = "Right of current";
            this.rightOfCurrentToolStripMenuItem.Click += new System.EventHandler(this.rightOfCurrentToolStripMenuItem_Click);
            // 
            // loadRegionByCoordsToolStripMenuItem
            // 
            this.loadRegionByCoordsToolStripMenuItem.Name = "loadRegionByCoordsToolStripMenuItem";
            this.loadRegionByCoordsToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.loadRegionByCoordsToolStripMenuItem.Text = "Load region by coords";
            this.loadRegionByCoordsToolStripMenuItem.Click += new System.EventHandler(this.loadRegionByCoordsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(224, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutBiomePainterToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutBiomePainterToolStripMenuItem
            // 
            this.aboutBiomePainterToolStripMenuItem.Name = "aboutBiomePainterToolStripMenuItem";
            this.aboutBiomePainterToolStripMenuItem.Size = new System.Drawing.Size(216, 24);
            this.aboutBiomePainterToolStripMenuItem.Text = "About Biome Painter";
            this.aboutBiomePainterToolStripMenuItem.Click += new System.EventHandler(this.aboutBiomePainterToolStripMenuItem_Click);
            // 
            // lstRegions
            // 
            this.lstRegions.FormattingEnabled = true;
            this.lstRegions.ItemHeight = 16;
            this.lstRegions.Location = new System.Drawing.Point(12, 31);
            this.lstRegions.Name = "lstRegions";
            this.lstRegions.Size = new System.Drawing.Size(254, 596);
            this.lstRegions.TabIndex = 1;
            this.lstRegions.SelectedIndexChanged += new System.EventHandler(this.lstRegions_SelectedIndexChanged);
            // 
            // chkShowMap
            // 
            this.chkShowMap.AutoSize = true;
            this.chkShowMap.Checked = true;
            this.chkShowMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowMap.Location = new System.Drawing.Point(8, 21);
            this.chkShowMap.Name = "chkShowMap";
            this.chkShowMap.Size = new System.Drawing.Size(95, 21);
            this.chkShowMap.TabIndex = 3;
            this.chkShowMap.Text = "Show Map";
            this.chkShowMap.UseVisualStyleBackColor = true;
            this.chkShowMap.CheckedChanged += new System.EventHandler(this.chkShowMap_CheckedChanged);
            // 
            // chkShowBiomes
            // 
            this.chkShowBiomes.AutoSize = true;
            this.chkShowBiomes.Checked = true;
            this.chkShowBiomes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowBiomes.Location = new System.Drawing.Point(8, 48);
            this.chkShowBiomes.Name = "chkShowBiomes";
            this.chkShowBiomes.Size = new System.Drawing.Size(114, 21);
            this.chkShowBiomes.TabIndex = 4;
            this.chkShowBiomes.Text = "Show Biomes";
            this.chkShowBiomes.UseVisualStyleBackColor = true;
            this.chkShowBiomes.CheckedChanged += new System.EventHandler(this.chkShowBiomes_CheckedChanged);
            // 
            // chkShowSelection
            // 
            this.chkShowSelection.AutoSize = true;
            this.chkShowSelection.Checked = true;
            this.chkShowSelection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowSelection.Location = new System.Drawing.Point(9, 100);
            this.chkShowSelection.Name = "chkShowSelection";
            this.chkShowSelection.Size = new System.Drawing.Size(126, 21);
            this.chkShowSelection.TabIndex = 5;
            this.chkShowSelection.Text = "Show Selection";
            this.chkShowSelection.UseVisualStyleBackColor = true;
            this.chkShowSelection.CheckedChanged += new System.EventHandler(this.chkShowSelection_CheckedChanged);
            // 
            // chkShowToolTips
            // 
            this.chkShowToolTips.AutoSize = true;
            this.chkShowToolTips.Checked = true;
            this.chkShowToolTips.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowToolTips.Location = new System.Drawing.Point(8, 73);
            this.chkShowToolTips.Name = "chkShowToolTips";
            this.chkShowToolTips.Size = new System.Drawing.Size(127, 21);
            this.chkShowToolTips.TabIndex = 6;
            this.chkShowToolTips.Text = "Show Tool Tips";
            this.chkShowToolTips.UseVisualStyleBackColor = true;
            this.chkShowToolTips.CheckedChanged += new System.EventHandler(this.chkShowToolTips_CheckedChanged);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(6, 22);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(89, 23);
            this.btnSelectAll.TabIndex = 7;
            this.btnSelectAll.Text = "All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Location = new System.Drawing.Point(101, 21);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(89, 23);
            this.btnSelectNone.TabIndex = 8;
            this.btnSelectNone.Text = "None";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // btnInvertSelection
            // 
            this.btnInvertSelection.Location = new System.Drawing.Point(6, 51);
            this.btnInvertSelection.Name = "btnInvertSelection";
            this.btnInvertSelection.Size = new System.Drawing.Size(89, 23);
            this.btnInvertSelection.TabIndex = 9;
            this.btnInvertSelection.Text = "Invert";
            this.btnInvertSelection.UseVisualStyleBackColor = true;
            this.btnInvertSelection.Click += new System.EventHandler(this.btnInvertSelection_Click);
            // 
            // radRoundBrush
            // 
            this.radRoundBrush.AutoSize = true;
            this.radRoundBrush.Checked = true;
            this.radRoundBrush.Location = new System.Drawing.Point(9, 21);
            this.radRoundBrush.Name = "radRoundBrush";
            this.radRoundBrush.Size = new System.Drawing.Size(71, 21);
            this.radRoundBrush.TabIndex = 10;
            this.radRoundBrush.TabStop = true;
            this.radRoundBrush.Text = "Round";
            this.radRoundBrush.UseVisualStyleBackColor = true;
            this.radRoundBrush.CheckedChanged += new System.EventHandler(this.radRoundBrush_CheckedChanged);
            // 
            // radSquareBrush
            // 
            this.radSquareBrush.AutoSize = true;
            this.radSquareBrush.Location = new System.Drawing.Point(86, 21);
            this.radSquareBrush.Name = "radSquareBrush";
            this.radSquareBrush.Size = new System.Drawing.Size(75, 21);
            this.radSquareBrush.TabIndex = 11;
            this.radSquareBrush.Text = "Square";
            this.radSquareBrush.UseVisualStyleBackColor = true;
            this.radSquareBrush.CheckedChanged += new System.EventHandler(this.radSquareBrush_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkShowMap);
            this.groupBox1.Controls.Add(this.chkShowBiomes);
            this.groupBox1.Controls.Add(this.chkShowToolTips);
            this.groupBox1.Controls.Add(this.chkShowSelection);
            this.groupBox1.Location = new System.Drawing.Point(791, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 125);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Layers";
            // 
            // btnSelectChunks
            // 
            this.btnSelectChunks.Location = new System.Drawing.Point(101, 51);
            this.btnSelectChunks.Name = "btnSelectChunks";
            this.btnSelectChunks.Size = new System.Drawing.Size(89, 23);
            this.btnSelectChunks.TabIndex = 13;
            this.btnSelectChunks.Text = "Chunks";
            this.btnSelectChunks.UseVisualStyleBackColor = true;
            this.btnSelectChunks.Click += new System.EventHandler(this.btnSelectChunks_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSelectAll);
            this.groupBox2.Controls.Add(this.btnSelectChunks);
            this.groupBox2.Controls.Add(this.btnSelectNone);
            this.groupBox2.Controls.Add(this.btnInvertSelection);
            this.groupBox2.Location = new System.Drawing.Point(791, 481);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(198, 83);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblBrushDiameter);
            this.groupBox3.Controls.Add(this.trackBrushDiameter);
            this.groupBox3.Controls.Add(this.radRoundBrush);
            this.groupBox3.Controls.Add(this.radSquareBrush);
            this.groupBox3.Location = new System.Drawing.Point(791, 370);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(198, 105);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Selection Brush";
            // 
            // lblBrushDiameter
            // 
            this.lblBrushDiameter.AutoSize = true;
            this.lblBrushDiameter.Location = new System.Drawing.Point(6, 45);
            this.lblBrushDiameter.Name = "lblBrushDiameter";
            this.lblBrushDiameter.Size = new System.Drawing.Size(122, 17);
            this.lblBrushDiameter.TabIndex = 13;
            this.lblBrushDiameter.Text = "Brush Diameter: 1";
            // 
            // trackBrushDiameter
            // 
            this.trackBrushDiameter.AutoSize = false;
            this.trackBrushDiameter.Location = new System.Drawing.Point(7, 65);
            this.trackBrushDiameter.Maximum = 50;
            this.trackBrushDiameter.Minimum = 1;
            this.trackBrushDiameter.Name = "trackBrushDiameter";
            this.trackBrushDiameter.Size = new System.Drawing.Size(186, 37);
            this.trackBrushDiameter.TabIndex = 12;
            this.trackBrushDiameter.TickFrequency = 5;
            this.trackBrushDiameter.Value = 1;
            this.trackBrushDiameter.Scroll += new System.EventHandler(this.trackBrushDiameter_Scroll);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.trackPanVertical);
            this.groupBox4.Controls.Add(this.trackPanHorizontal);
            this.groupBox4.Controls.Add(this.lblMagnification);
            this.groupBox4.Controls.Add(this.trackMagnification);
            this.groupBox4.Location = new System.Drawing.Point(791, 162);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(198, 202);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Zoom";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "Pan (Vertical):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "Pan (Horizontal):";
            // 
            // trackPanVertical
            // 
            this.trackPanVertical.AutoSize = false;
            this.trackPanVertical.Location = new System.Drawing.Point(7, 158);
            this.trackPanVertical.Name = "trackPanVertical";
            this.trackPanVertical.Size = new System.Drawing.Size(186, 37);
            this.trackPanVertical.TabIndex = 18;
            this.trackPanVertical.TickFrequency = 4;
            this.trackPanVertical.Scroll += new System.EventHandler(this.trackPanVertical_Scroll);
            // 
            // trackPanHorizontal
            // 
            this.trackPanHorizontal.AutoSize = false;
            this.trackPanHorizontal.Location = new System.Drawing.Point(7, 98);
            this.trackPanHorizontal.Name = "trackPanHorizontal";
            this.trackPanHorizontal.Size = new System.Drawing.Size(186, 37);
            this.trackPanHorizontal.TabIndex = 17;
            this.trackPanHorizontal.TickFrequency = 4;
            this.trackPanHorizontal.Scroll += new System.EventHandler(this.trackPanHorizontal_Scroll);
            // 
            // lblMagnification
            // 
            this.lblMagnification.AutoSize = true;
            this.lblMagnification.Location = new System.Drawing.Point(6, 18);
            this.lblMagnification.Name = "lblMagnification";
            this.lblMagnification.Size = new System.Drawing.Size(113, 17);
            this.lblMagnification.TabIndex = 1;
            this.lblMagnification.Text = "Magnification: 1x";
            // 
            // trackMagnification
            // 
            this.trackMagnification.AutoSize = false;
            this.trackMagnification.Location = new System.Drawing.Point(7, 38);
            this.trackMagnification.Minimum = 1;
            this.trackMagnification.Name = "trackMagnification";
            this.trackMagnification.Size = new System.Drawing.Size(186, 37);
            this.trackMagnification.TabIndex = 0;
            this.trackMagnification.Value = 1;
            this.trackMagnification.Scroll += new System.EventHandler(this.trackMagnification_Scroll);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnReplace);
            this.groupBox5.Controls.Add(this.cmbReplace2);
            this.groupBox5.Controls.Add(this.cmbReplace1);
            this.groupBox5.Controls.Add(this.btnFill);
            this.groupBox5.Controls.Add(this.cmbFill);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(272, 570);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(717, 61);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Edit Biomes in Selection";
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(634, 20);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(75, 24);
            this.btnReplace.TabIndex = 5;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // cmbReplace2
            // 
            this.cmbReplace2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReplace2.FormattingEnabled = true;
            this.cmbReplace2.Location = new System.Drawing.Point(455, 21);
            this.cmbReplace2.Name = "cmbReplace2";
            this.cmbReplace2.Size = new System.Drawing.Size(174, 24);
            this.cmbReplace2.TabIndex = 4;
            // 
            // cmbReplace1
            // 
            this.cmbReplace1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReplace1.FormattingEnabled = true;
            this.cmbReplace1.Location = new System.Drawing.Point(275, 21);
            this.cmbReplace1.Name = "cmbReplace1";
            this.cmbReplace1.Size = new System.Drawing.Size(174, 24);
            this.cmbReplace1.TabIndex = 3;
            // 
            // btnFill
            // 
            this.btnFill.Location = new System.Drawing.Point(186, 21);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(75, 24);
            this.btnFill.TabIndex = 2;
            this.btnFill.Text = "Fill";
            this.btnFill.UseVisualStyleBackColor = true;
            this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
            // 
            // cmbFill
            // 
            this.cmbFill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFill.FormattingEnabled = true;
            this.cmbFill.Location = new System.Drawing.Point(6, 21);
            this.cmbFill.Name = "cmbFill";
            this.cmbFill.Size = new System.Drawing.Size(174, 24);
            this.cmbFill.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(267, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(2, 35);
            this.label3.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(272, 546);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(512, 23);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // imgRegion
            // 
            this.imgRegion.Location = new System.Drawing.Point(272, 31);
            this.imgRegion.Name = "imgRegion";
            this.imgRegion.Size = new System.Drawing.Size(512, 512);
            this.imgRegion.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(996, 640);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.imgRegion);
            this.Controls.Add(this.lstRegions);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Biome Painter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBrushDiameter)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackPanVertical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPanHorizontal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMagnification)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ListBox lstRegions;
        private BitmapSelector.BitmapSelector imgRegion;
        private System.Windows.Forms.CheckBox chkShowMap;
        private System.Windows.Forms.CheckBox chkShowBiomes;
        private System.Windows.Forms.CheckBox chkShowSelection;
        private System.Windows.Forms.CheckBox chkShowToolTips;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectNone;
        private System.Windows.Forms.Button btnInvertSelection;
        private System.Windows.Forms.RadioButton radRoundBrush;
        private System.Windows.Forms.RadioButton radSquareBrush;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelectChunks;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TrackBar trackBrushDiameter;
        private System.Windows.Forms.Label lblBrushDiameter;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblMagnification;
        private System.Windows.Forms.TrackBar trackMagnification;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackPanVertical;
        private System.Windows.Forms.TrackBar trackPanHorizontal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.ComboBox cmbReplace2;
        private System.Windows.Forms.ComboBox cmbReplace1;
        private System.Windows.Forms.Button btnFill;
        private System.Windows.Forms.ComboBox cmbFill;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeWorldToolStripMenuItem;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolStripMenuItem reloadCurrentRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboveCurrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem belowCurrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftOfCurrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightOfCurrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem loadRegionByCoordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutBiomePainterToolStripMenuItem;
    }
}

