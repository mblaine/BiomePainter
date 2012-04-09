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
            this.components = new System.ComponentModel.Container();
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
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.setChunksInSelectionToBePopulatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unsetChunksInSelectionToBePopulatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chunksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
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
            this.chkShowPopulate = new System.Windows.Forms.CheckBox();
            this.chkShowBrush = new System.Windows.Forms.CheckBox();
            this.chkShowChunkBoundaries = new System.Windows.Forms.CheckBox();
            this.btnSelectChunks = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblBrushDiameter = new System.Windows.Forms.Label();
            this.trackBrushDiameter = new System.Windows.Forms.TrackBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
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
            this.btnSaveRegion = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnRegionLeft = new System.Windows.Forms.Button();
            this.btnRegionUp = new System.Windows.Forms.Button();
            this.btnRegionRight = new System.Windows.Forms.Button();
            this.btnRegionDown = new System.Windows.Forms.Button();
            this.btnRegionJump = new System.Windows.Forms.Button();
            this.btnAddbyBlocks = new System.Windows.Forms.Button();
            this.btnRemovebyBlocks = new System.Windows.Forms.Button();
            this.btnRemovebyBiomes = new System.Windows.Forms.Button();
            this.btnAddbyBiomes = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.cmbBlockType = new System.Windows.Forms.ComboBox();
            this.imgRegion = new BitmapSelector.BitmapSelector();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.cmbBiomeType = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBrushDiameter)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackMagnification)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.selectToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(982, 28);
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
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openWorldToolStripMenuItem
            // 
            this.openWorldToolStripMenuItem.Name = "openWorldToolStripMenuItem";
            this.openWorldToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openWorldToolStripMenuItem.Size = new System.Drawing.Size(279, 24);
            this.openWorldToolStripMenuItem.Text = "&Open world";
            this.openWorldToolStripMenuItem.Click += new System.EventHandler(this.openWorldToolStripMenuItem_Click);
            // 
            // closeWorldToolStripMenuItem
            // 
            this.closeWorldToolStripMenuItem.Name = "closeWorldToolStripMenuItem";
            this.closeWorldToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeWorldToolStripMenuItem.Size = new System.Drawing.Size(279, 24);
            this.closeWorldToolStripMenuItem.Text = "&Close world";
            this.closeWorldToolStripMenuItem.Click += new System.EventHandler(this.closeWorldToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(276, 6);
            // 
            // saveCurrentRegionToolStripMenuItem
            // 
            this.saveCurrentRegionToolStripMenuItem.Name = "saveCurrentRegionToolStripMenuItem";
            this.saveCurrentRegionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveCurrentRegionToolStripMenuItem.Size = new System.Drawing.Size(279, 24);
            this.saveCurrentRegionToolStripMenuItem.Text = "&Save current region";
            this.saveCurrentRegionToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentRegionToolStripMenuItem_Click);
            // 
            // reloadCurrentRegionToolStripMenuItem
            // 
            this.reloadCurrentRegionToolStripMenuItem.Name = "reloadCurrentRegionToolStripMenuItem";
            this.reloadCurrentRegionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.reloadCurrentRegionToolStripMenuItem.Size = new System.Drawing.Size(279, 24);
            this.reloadCurrentRegionToolStripMenuItem.Text = "&Reload current region";
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
            this.loadRegionToolStripMenuItem.Size = new System.Drawing.Size(279, 24);
            this.loadRegionToolStripMenuItem.Text = "Load &next region";
            // 
            // aboveCurrentToolStripMenuItem
            // 
            this.aboveCurrentToolStripMenuItem.Name = "aboveCurrentToolStripMenuItem";
            this.aboveCurrentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.aboveCurrentToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.aboveCurrentToolStripMenuItem.Text = "&Above current";
            this.aboveCurrentToolStripMenuItem.Click += new System.EventHandler(this.aboveCurrentToolStripMenuItem_Click);
            // 
            // belowCurrentToolStripMenuItem
            // 
            this.belowCurrentToolStripMenuItem.Name = "belowCurrentToolStripMenuItem";
            this.belowCurrentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.belowCurrentToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.belowCurrentToolStripMenuItem.Text = "&Below current";
            this.belowCurrentToolStripMenuItem.Click += new System.EventHandler(this.belowCurrentToolStripMenuItem_Click);
            // 
            // leftOfCurrentToolStripMenuItem
            // 
            this.leftOfCurrentToolStripMenuItem.Name = "leftOfCurrentToolStripMenuItem";
            this.leftOfCurrentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.leftOfCurrentToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.leftOfCurrentToolStripMenuItem.Text = "&Left of current";
            this.leftOfCurrentToolStripMenuItem.Click += new System.EventHandler(this.leftOfCurrentToolStripMenuItem_Click);
            // 
            // rightOfCurrentToolStripMenuItem
            // 
            this.rightOfCurrentToolStripMenuItem.Name = "rightOfCurrentToolStripMenuItem";
            this.rightOfCurrentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.rightOfCurrentToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.rightOfCurrentToolStripMenuItem.Text = "&Right of current";
            this.rightOfCurrentToolStripMenuItem.Click += new System.EventHandler(this.rightOfCurrentToolStripMenuItem_Click);
            // 
            // loadRegionByCoordsToolStripMenuItem
            // 
            this.loadRegionByCoordsToolStripMenuItem.Name = "loadRegionByCoordsToolStripMenuItem";
            this.loadRegionByCoordsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.loadRegionByCoordsToolStripMenuItem.Size = new System.Drawing.Size(279, 24);
            this.loadRegionByCoordsToolStripMenuItem.Text = "Load region by &coords";
            this.loadRegionByCoordsToolStripMenuItem.Click += new System.EventHandler(this.loadRegionByCoordsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(276, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(279, 24);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator5,
            this.setChunksInSelectionToBePopulatedToolStripMenuItem,
            this.unsetChunksInSelectionToBePopulatedToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.redoToolStripMenuItem.Text = "&Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(354, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(354, 6);
            // 
            // setChunksInSelectionToBePopulatedToolStripMenuItem
            // 
            this.setChunksInSelectionToBePopulatedToolStripMenuItem.Name = "setChunksInSelectionToBePopulatedToolStripMenuItem";
            this.setChunksInSelectionToBePopulatedToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.setChunksInSelectionToBePopulatedToolStripMenuItem.Text = "Set Chunks in Selection to be Populated";
            this.setChunksInSelectionToBePopulatedToolStripMenuItem.Click += new System.EventHandler(this.setChunksInSelectionToBePopulatedToolStripMenuItem_Click);
            // 
            // unsetChunksInSelectionToBePopulatedToolStripMenuItem
            // 
            this.unsetChunksInSelectionToBePopulatedToolStripMenuItem.Name = "unsetChunksInSelectionToBePopulatedToolStripMenuItem";
            this.unsetChunksInSelectionToBePopulatedToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.unsetChunksInSelectionToBePopulatedToolStripMenuItem.Text = "Unset Chunks in Selection to be Populated";
            this.unsetChunksInSelectionToBePopulatedToolStripMenuItem.Click += new System.EventHandler(this.unsetChunksInSelectionToBePopulatedToolStripMenuItem_Click);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.noneToolStripMenuItem,
            this.invertToolStripMenuItem,
            this.chunksToolStripMenuItem});
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(61, 24);
            this.selectToolStripMenuItem.Text = "&Select";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.allToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.allToolStripMenuItem.Text = "&All";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.noneToolStripMenuItem.Text = "&None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // invertToolStripMenuItem
            // 
            this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
            this.invertToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.invertToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.invertToolStripMenuItem.Text = "&Invert";
            this.invertToolStripMenuItem.Click += new System.EventHandler(this.invertToolStripMenuItem_Click);
            // 
            // chunksToolStripMenuItem
            // 
            this.chunksToolStripMenuItem.Name = "chunksToolStripMenuItem";
            this.chunksToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.chunksToolStripMenuItem.Text = "&Chunks";
            this.chunksToolStripMenuItem.Click += new System.EventHandler(this.chunksToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.toolStripSeparator4,
            this.aboutBiomePainterToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(216, 24);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for &Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(213, 6);
            // 
            // aboutBiomePainterToolStripMenuItem
            // 
            this.aboutBiomePainterToolStripMenuItem.Name = "aboutBiomePainterToolStripMenuItem";
            this.aboutBiomePainterToolStripMenuItem.Size = new System.Drawing.Size(216, 24);
            this.aboutBiomePainterToolStripMenuItem.Text = "&About Biome Painter";
            this.aboutBiomePainterToolStripMenuItem.Click += new System.EventHandler(this.aboutBiomePainterToolStripMenuItem_Click);
            // 
            // lstRegions
            // 
            this.lstRegions.FormattingEnabled = true;
            this.lstRegions.HorizontalScrollbar = true;
            this.lstRegions.ItemHeight = 16;
            this.lstRegions.Location = new System.Drawing.Point(12, 77);
            this.lstRegions.Name = "lstRegions";
            this.lstRegions.Size = new System.Drawing.Size(240, 468);
            this.lstRegions.TabIndex = 1;
            this.lstRegions.SelectedIndexChanged += new System.EventHandler(this.lstRegions_SelectedIndexChanged);
            // 
            // chkShowMap
            // 
            this.chkShowMap.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkShowMap.Checked = true;
            this.chkShowMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowMap.Image = global::BiomePainter.Properties.Resources.map;
            this.chkShowMap.Location = new System.Drawing.Point(6, 21);
            this.chkShowMap.Name = "chkShowMap";
            this.chkShowMap.Size = new System.Drawing.Size(40, 40);
            this.chkShowMap.TabIndex = 3;
            this.toolTip.SetToolTip(this.chkShowMap, "Map (1)");
            this.chkShowMap.UseVisualStyleBackColor = true;
            this.chkShowMap.CheckedChanged += new System.EventHandler(this.chkShowMap_CheckedChanged);
            // 
            // chkShowBiomes
            // 
            this.chkShowBiomes.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkShowBiomes.Checked = true;
            this.chkShowBiomes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowBiomes.Image = global::BiomePainter.Properties.Resources.biomes;
            this.chkShowBiomes.Location = new System.Drawing.Point(52, 21);
            this.chkShowBiomes.Name = "chkShowBiomes";
            this.chkShowBiomes.Size = new System.Drawing.Size(40, 40);
            this.chkShowBiomes.TabIndex = 4;
            this.toolTip.SetToolTip(this.chkShowBiomes, "Biomes (2)");
            this.chkShowBiomes.UseVisualStyleBackColor = true;
            this.chkShowBiomes.CheckedChanged += new System.EventHandler(this.chkShowBiomes_CheckedChanged);
            // 
            // chkShowSelection
            // 
            this.chkShowSelection.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkShowSelection.Checked = true;
            this.chkShowSelection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowSelection.Image = global::BiomePainter.Properties.Resources.selection;
            this.chkShowSelection.Location = new System.Drawing.Point(6, 67);
            this.chkShowSelection.Name = "chkShowSelection";
            this.chkShowSelection.Size = new System.Drawing.Size(40, 40);
            this.chkShowSelection.TabIndex = 5;
            this.toolTip.SetToolTip(this.chkShowSelection, "Selection (5)");
            this.chkShowSelection.UseVisualStyleBackColor = true;
            this.chkShowSelection.CheckedChanged += new System.EventHandler(this.chkShowSelection_CheckedChanged);
            // 
            // chkShowToolTips
            // 
            this.chkShowToolTips.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkShowToolTips.Checked = true;
            this.chkShowToolTips.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowToolTips.Image = global::BiomePainter.Properties.Resources.tooltips;
            this.chkShowToolTips.Location = new System.Drawing.Point(98, 21);
            this.chkShowToolTips.Name = "chkShowToolTips";
            this.chkShowToolTips.Size = new System.Drawing.Size(40, 40);
            this.chkShowToolTips.TabIndex = 6;
            this.toolTip.SetToolTip(this.chkShowToolTips, "Tool Tips (3)");
            this.chkShowToolTips.UseVisualStyleBackColor = true;
            this.chkShowToolTips.CheckedChanged += new System.EventHandler(this.chkShowToolTips_CheckedChanged);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Image = global::BiomePainter.Properties.Resources.select_all;
            this.btnSelectAll.Location = new System.Drawing.Point(6, 21);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(40, 40);
            this.btnSelectAll.TabIndex = 7;
            this.toolTip.SetToolTip(this.btnSelectAll, "Select all (Ctrl+A)");
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Image = global::BiomePainter.Properties.Resources.select_none;
            this.btnSelectNone.Location = new System.Drawing.Point(52, 21);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(40, 40);
            this.btnSelectNone.TabIndex = 8;
            this.toolTip.SetToolTip(this.btnSelectNone, "Select none (Ctrl+Shift+A)");
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // btnInvertSelection
            // 
            this.btnInvertSelection.Image = global::BiomePainter.Properties.Resources.select_invert;
            this.btnInvertSelection.Location = new System.Drawing.Point(98, 21);
            this.btnInvertSelection.Name = "btnInvertSelection";
            this.btnInvertSelection.Size = new System.Drawing.Size(40, 40);
            this.btnInvertSelection.TabIndex = 9;
            this.toolTip.SetToolTip(this.btnInvertSelection, "Invert selection (Ctrl+I)");
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
            this.groupBox1.Controls.Add(this.chkShowPopulate);
            this.groupBox1.Controls.Add(this.chkShowBrush);
            this.groupBox1.Controls.Add(this.chkShowChunkBoundaries);
            this.groupBox1.Controls.Add(this.chkShowMap);
            this.groupBox1.Controls.Add(this.chkShowBiomes);
            this.groupBox1.Controls.Add(this.chkShowToolTips);
            this.groupBox1.Controls.Add(this.chkShowSelection);
            this.groupBox1.Location = new System.Drawing.Point(777, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 115);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Layers";
            // 
            // chkShowPopulate
            // 
            this.chkShowPopulate.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkShowPopulate.Image = global::BiomePainter.Properties.Resources.chunks_populated;
            this.chkShowPopulate.Location = new System.Drawing.Point(144, 21);
            this.chkShowPopulate.Name = "chkShowPopulate";
            this.chkShowPopulate.Size = new System.Drawing.Size(40, 40);
            this.chkShowPopulate.TabIndex = 9;
            this.toolTip.SetToolTip(this.chkShowPopulate, "Chunks that will be populated with trees and ores the next time they are loaded i" +
        "n Minecraft. Alter from the Edit menu. (4)");
            this.chkShowPopulate.UseVisualStyleBackColor = true;
            this.chkShowPopulate.CheckedChanged += new System.EventHandler(this.chkShowPopulate_CheckedChanged);
            // 
            // chkShowBrush
            // 
            this.chkShowBrush.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkShowBrush.Checked = true;
            this.chkShowBrush.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowBrush.Image = global::BiomePainter.Properties.Resources.brush;
            this.chkShowBrush.Location = new System.Drawing.Point(52, 67);
            this.chkShowBrush.Name = "chkShowBrush";
            this.chkShowBrush.Size = new System.Drawing.Size(40, 40);
            this.chkShowBrush.TabIndex = 8;
            this.toolTip.SetToolTip(this.chkShowBrush, "Brush (6)");
            this.chkShowBrush.UseVisualStyleBackColor = true;
            this.chkShowBrush.CheckedChanged += new System.EventHandler(this.chkShowBrush_CheckedChanged);
            // 
            // chkShowChunkBoundaries
            // 
            this.chkShowChunkBoundaries.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkShowChunkBoundaries.Image = global::BiomePainter.Properties.Resources.chunk_boundaries;
            this.chkShowChunkBoundaries.Location = new System.Drawing.Point(98, 67);
            this.chkShowChunkBoundaries.Name = "chkShowChunkBoundaries";
            this.chkShowChunkBoundaries.Size = new System.Drawing.Size(40, 40);
            this.chkShowChunkBoundaries.TabIndex = 7;
            this.toolTip.SetToolTip(this.chkShowChunkBoundaries, "Chunk Boundaries (7)");
            this.chkShowChunkBoundaries.UseVisualStyleBackColor = true;
            this.chkShowChunkBoundaries.CheckedChanged += new System.EventHandler(this.chkShowChunkBoundaries_CheckedChanged);
            // 
            // btnSelectChunks
            // 
            this.btnSelectChunks.Image = global::BiomePainter.Properties.Resources.select_chunks;
            this.btnSelectChunks.Location = new System.Drawing.Point(144, 21);
            this.btnSelectChunks.Name = "btnSelectChunks";
            this.btnSelectChunks.Size = new System.Drawing.Size(40, 40);
            this.btnSelectChunks.TabIndex = 13;
            this.toolTip.SetToolTip(this.btnSelectChunks, "Expand selection to chunks");
            this.btnSelectChunks.UseVisualStyleBackColor = true;
            this.btnSelectChunks.Click += new System.EventHandler(this.btnSelectChunks_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSelectAll);
            this.groupBox2.Controls.Add(this.btnSelectChunks);
            this.groupBox2.Controls.Add(this.btnSelectNone);
            this.groupBox2.Controls.Add(this.btnInvertSelection);
            this.groupBox2.Location = new System.Drawing.Point(777, 334);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(198, 71);
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
            this.groupBox3.Location = new System.Drawing.Point(777, 228);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(198, 104);
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
            this.groupBox4.Controls.Add(this.lblMagnification);
            this.groupBox4.Controls.Add(this.trackMagnification);
            this.groupBox4.Location = new System.Drawing.Point(777, 148);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(198, 78);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Zoom";
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
            this.trackMagnification.LargeChange = 1;
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
            this.groupBox5.Location = new System.Drawing.Point(258, 576);
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
            this.btnReplace.Size = new System.Drawing.Size(75, 25);
            this.btnReplace.TabIndex = 5;
            this.btnReplace.Text = "Replace";
            this.toolTip.SetToolTip(this.btnReplace, "Replace instances of the first biome with the second within the selected area.");
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
            this.btnFill.Location = new System.Drawing.Point(186, 20);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(75, 25);
            this.btnFill.TabIndex = 2;
            this.btnFill.Text = "Fill";
            this.toolTip.SetToolTip(this.btnFill, "Fill the selected area with the selected biome or biomes from the selected versio" +
        "n of Minecraft.");
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
            this.lblStatus.Location = new System.Drawing.Point(258, 546);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(512, 27);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSaveRegion
            // 
            this.btnSaveRegion.Image = global::BiomePainter.Properties.Resources.save;
            this.btnSaveRegion.Location = new System.Drawing.Point(18, 31);
            this.btnSaveRegion.Name = "btnSaveRegion";
            this.btnSaveRegion.Size = new System.Drawing.Size(40, 40);
            this.btnSaveRegion.TabIndex = 4;
            this.toolTip.SetToolTip(this.btnSaveRegion, "Save the current region (Ctrl + S)");
            this.btnSaveRegion.UseVisualStyleBackColor = true;
            this.btnSaveRegion.Click += new System.EventHandler(this.btnSaveRegion_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.Image = global::BiomePainter.Properties.Resources.paste;
            this.btnPaste.Location = new System.Drawing.Point(110, 31);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(40, 40);
            this.btnPaste.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnPaste, "Paste biomes (Ctrl+V)");
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Image = global::BiomePainter.Properties.Resources.copy;
            this.btnCopy.Location = new System.Drawing.Point(64, 31);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(40, 40);
            this.btnCopy.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnCopy, "Copy biomes in selection (Ctrl+C)");
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.Image = global::BiomePainter.Properties.Resources.redo;
            this.btnRedo.Location = new System.Drawing.Point(202, 31);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(40, 40);
            this.btnRedo.TabIndex = 1;
            this.toolTip.SetToolTip(this.btnRedo, "Redo (Ctrl+Y)");
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Image = global::BiomePainter.Properties.Resources.undo;
            this.btnUndo.Location = new System.Drawing.Point(156, 31);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(40, 40);
            this.btnUndo.TabIndex = 0;
            this.toolTip.SetToolTip(this.btnUndo, "Undo (Ctrl+Z)");
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 400;
            this.toolTip.AutoPopDelay = 8000;
            this.toolTip.InitialDelay = 400;
            this.toolTip.ReshowDelay = 80;
            // 
            // btnRegionLeft
            // 
            this.btnRegionLeft.Image = global::BiomePainter.Properties.Resources.left;
            this.btnRegionLeft.Location = new System.Drawing.Point(6, 21);
            this.btnRegionLeft.Name = "btnRegionLeft";
            this.btnRegionLeft.Size = new System.Drawing.Size(40, 40);
            this.btnRegionLeft.TabIndex = 0;
            this.toolTip.SetToolTip(this.btnRegionLeft, "Load the region left of the current one. (Ctrl + Left)");
            this.btnRegionLeft.UseVisualStyleBackColor = true;
            this.btnRegionLeft.Click += new System.EventHandler(this.btnRegionLeft_Click);
            // 
            // btnRegionUp
            // 
            this.btnRegionUp.Image = global::BiomePainter.Properties.Resources.up;
            this.btnRegionUp.Location = new System.Drawing.Point(52, 21);
            this.btnRegionUp.Name = "btnRegionUp";
            this.btnRegionUp.Size = new System.Drawing.Size(40, 40);
            this.btnRegionUp.TabIndex = 1;
            this.toolTip.SetToolTip(this.btnRegionUp, "Load the region above the current one. (Ctrl + Up)");
            this.btnRegionUp.UseVisualStyleBackColor = true;
            this.btnRegionUp.Click += new System.EventHandler(this.btnRegionUp_Click);
            // 
            // btnRegionRight
            // 
            this.btnRegionRight.Image = global::BiomePainter.Properties.Resources.right;
            this.btnRegionRight.Location = new System.Drawing.Point(98, 21);
            this.btnRegionRight.Name = "btnRegionRight";
            this.btnRegionRight.Size = new System.Drawing.Size(40, 40);
            this.btnRegionRight.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnRegionRight, "Load the region right of the current one. (Ctrl + Right)");
            this.btnRegionRight.UseVisualStyleBackColor = true;
            this.btnRegionRight.Click += new System.EventHandler(this.btnRegionRight_Click);
            // 
            // btnRegionDown
            // 
            this.btnRegionDown.Image = global::BiomePainter.Properties.Resources.down;
            this.btnRegionDown.Location = new System.Drawing.Point(144, 21);
            this.btnRegionDown.Name = "btnRegionDown";
            this.btnRegionDown.Size = new System.Drawing.Size(40, 40);
            this.btnRegionDown.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnRegionDown, "Load the region below the current one. (Ctrl + Down)");
            this.btnRegionDown.UseVisualStyleBackColor = true;
            this.btnRegionDown.Click += new System.EventHandler(this.btnRegionDown_Click);
            // 
            // btnRegionJump
            // 
            this.btnRegionJump.Image = global::BiomePainter.Properties.Resources.jump;
            this.btnRegionJump.Location = new System.Drawing.Point(190, 21);
            this.btnRegionJump.Name = "btnRegionJump";
            this.btnRegionJump.Size = new System.Drawing.Size(40, 40);
            this.btnRegionJump.TabIndex = 4;
            this.toolTip.SetToolTip(this.btnRegionJump, "Jump to the region that contains certain absolute x, z block coordinates. (Ctrl +" +
        " G)");
            this.btnRegionJump.UseVisualStyleBackColor = true;
            this.btnRegionJump.Click += new System.EventHandler(this.btnRegionJump_Click);
            // 
            // btnAddbyBlocks
            // 
            this.btnAddbyBlocks.Location = new System.Drawing.Point(17, 52);
            this.btnAddbyBlocks.Name = "btnAddbyBlocks";
            this.btnAddbyBlocks.Size = new System.Drawing.Size(75, 25);
            this.btnAddbyBlocks.TabIndex = 1;
            this.btnAddbyBlocks.Text = "Add";
            this.toolTip.SetToolTip(this.btnAddbyBlocks, "Add areas covered by the selected block to the selection.");
            this.btnAddbyBlocks.UseVisualStyleBackColor = true;
            this.btnAddbyBlocks.Click += new System.EventHandler(this.btnAddorRemovebyBlocks_Click);
            // 
            // btnRemovebyBlocks
            // 
            this.btnRemovebyBlocks.Location = new System.Drawing.Point(98, 52);
            this.btnRemovebyBlocks.Name = "btnRemovebyBlocks";
            this.btnRemovebyBlocks.Size = new System.Drawing.Size(75, 25);
            this.btnRemovebyBlocks.TabIndex = 2;
            this.btnRemovebyBlocks.Text = "Remove";
            this.toolTip.SetToolTip(this.btnRemovebyBlocks, "Remove areas covered by the selected block from the selection.");
            this.btnRemovebyBlocks.UseVisualStyleBackColor = true;
            this.btnRemovebyBlocks.Click += new System.EventHandler(this.btnAddorRemovebyBlocks_Click);
            // 
            // btnRemovebyBiomes
            // 
            this.btnRemovebyBiomes.Location = new System.Drawing.Point(98, 52);
            this.btnRemovebyBiomes.Name = "btnRemovebyBiomes";
            this.btnRemovebyBiomes.Size = new System.Drawing.Size(75, 25);
            this.btnRemovebyBiomes.TabIndex = 2;
            this.btnRemovebyBiomes.Text = "Remove";
            this.toolTip.SetToolTip(this.btnRemovebyBiomes, "Remove areas covered by the selected biome from the selection.");
            this.btnRemovebyBiomes.UseVisualStyleBackColor = true;
            this.btnRemovebyBiomes.Click += new System.EventHandler(this.btnAddorRemovebyBiomes_Click);
            // 
            // btnAddbyBiomes
            // 
            this.btnAddbyBiomes.Location = new System.Drawing.Point(17, 52);
            this.btnAddbyBiomes.Name = "btnAddbyBiomes";
            this.btnAddbyBiomes.Size = new System.Drawing.Size(75, 25);
            this.btnAddbyBiomes.TabIndex = 1;
            this.btnAddbyBiomes.Text = "Add";
            this.toolTip.SetToolTip(this.btnAddbyBiomes, "Add areas covered by the selected biome to the selection.");
            this.btnAddbyBiomes.UseVisualStyleBackColor = true;
            this.btnAddbyBiomes.Click += new System.EventHandler(this.btnAddorRemovebyBiomes_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnRegionJump);
            this.groupBox7.Controls.Add(this.btnRegionDown);
            this.groupBox7.Controls.Add(this.btnRegionRight);
            this.groupBox7.Controls.Add(this.btnRegionUp);
            this.groupBox7.Controls.Add(this.btnRegionLeft);
            this.groupBox7.Location = new System.Drawing.Point(12, 565);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(240, 72);
            this.groupBox7.TabIndex = 20;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Switch Regions";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnRemovebyBlocks);
            this.groupBox8.Controls.Add(this.btnAddbyBlocks);
            this.groupBox8.Controls.Add(this.cmbBlockType);
            this.groupBox8.Location = new System.Drawing.Point(777, 407);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(198, 83);
            this.groupBox8.TabIndex = 21;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Select by Block";
            // 
            // cmbBlockType
            // 
            this.cmbBlockType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBlockType.FormattingEnabled = true;
            this.cmbBlockType.Location = new System.Drawing.Point(6, 22);
            this.cmbBlockType.Name = "cmbBlockType";
            this.cmbBlockType.Size = new System.Drawing.Size(178, 24);
            this.cmbBlockType.TabIndex = 0;
            // 
            // imgRegion
            // 
            this.imgRegion.Location = new System.Drawing.Point(258, 31);
            this.imgRegion.Name = "imgRegion";
            this.imgRegion.Size = new System.Drawing.Size(512, 512);
            this.imgRegion.TabIndex = 2;
            this.imgRegion.ZoomEvent += new BitmapSelector.BitmapSelector.ZoomEventHandler(this.imgRegion_ZoomEvent);
            this.imgRegion.BrushDiameterChanged += new BitmapSelector.BitmapSelector.BrushDiameterEventHandler(this.imgRegion_BrushDiameterChanged);
            this.imgRegion.SelectionChanged += new System.EventHandler(this.imgRegion_SelectionChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnRemovebyBiomes);
            this.groupBox9.Controls.Add(this.btnAddbyBiomes);
            this.groupBox9.Controls.Add(this.cmbBiomeType);
            this.groupBox9.Location = new System.Drawing.Point(777, 492);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(198, 83);
            this.groupBox9.TabIndex = 22;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Select by Biome";
            // 
            // cmbBiomeType
            // 
            this.cmbBiomeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBiomeType.FormattingEnabled = true;
            this.cmbBiomeType.Location = new System.Drawing.Point(6, 22);
            this.cmbBiomeType.Name = "cmbBiomeType";
            this.cmbBiomeType.Size = new System.Drawing.Size(178, 24);
            this.cmbBiomeType.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(982, 643);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnSaveRegion);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
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
            this.Icon = global::BiomePainter.Properties.Resources.icon;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Biome Painter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBrushDiameter)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackMagnification)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.CheckBox chkShowChunkBoundaries;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chunksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.CheckBox chkShowBrush;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox chkShowPopulate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem setChunksInSelectionToBePopulatedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unsetChunksInSelectionToBePopulatedToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnRegionLeft;
        private System.Windows.Forms.Button btnRegionDown;
        private System.Windows.Forms.Button btnRegionRight;
        private System.Windows.Forms.Button btnRegionUp;
        private System.Windows.Forms.Button btnRegionJump;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnRemovebyBlocks;
        private System.Windows.Forms.Button btnAddbyBlocks;
        private System.Windows.Forms.ComboBox cmbBlockType;
        private System.Windows.Forms.Button btnSaveRegion;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btnRemovebyBiomes;
        private System.Windows.Forms.Button btnAddbyBiomes;
        private System.Windows.Forms.ComboBox cmbBiomeType;
    }
}

