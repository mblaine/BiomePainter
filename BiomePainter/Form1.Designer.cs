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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.label1 = new System.Windows.Forms.Label();
            this.trackPanVertical = new System.Windows.Forms.TrackBar();
            this.trackPanHorizontal = new System.Windows.Forms.TrackBar();
            this.lblMagnification = new System.Windows.Forms.Label();
            this.trackMagnification = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
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
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1197, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openWorldToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openWorldToolStripMenuItem
            // 
            this.openWorldToolStripMenuItem.Name = "openWorldToolStripMenuItem";
            this.openWorldToolStripMenuItem.Size = new System.Drawing.Size(156, 24);
            this.openWorldToolStripMenuItem.Text = "Open world";
            this.openWorldToolStripMenuItem.Click += new System.EventHandler(this.openWorldToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(156, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lstRegions
            // 
            this.lstRegions.FormattingEnabled = true;
            this.lstRegions.ItemHeight = 16;
            this.lstRegions.Location = new System.Drawing.Point(12, 31);
            this.lstRegions.Name = "lstRegions";
            this.lstRegions.Size = new System.Drawing.Size(254, 516);
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
            this.groupBox2.Location = new System.Drawing.Point(995, 142);
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
            this.groupBox3.Location = new System.Drawing.Point(995, 31);
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
            this.trackPanVertical.Scroll += new System.EventHandler(this.trackPanVertical_Scroll);
            // 
            // trackPanHorizontal
            // 
            this.trackPanHorizontal.AutoSize = false;
            this.trackPanHorizontal.Location = new System.Drawing.Point(7, 98);
            this.trackPanHorizontal.Name = "trackPanHorizontal";
            this.trackPanHorizontal.Size = new System.Drawing.Size(186, 37);
            this.trackPanHorizontal.TabIndex = 17;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "Pan (Vertical):";
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
            this.ClientSize = new System.Drawing.Size(1197, 559);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.imgRegion);
            this.Controls.Add(this.lstRegions);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Biome Painter";
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
    }
}

