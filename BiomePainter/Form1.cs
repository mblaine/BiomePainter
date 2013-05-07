using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BiomePainter.Clipboard;
using BiomePainter.History;
using BiomePainter.BitmapSelector;
using Minecraft;

namespace BiomePainter
{
    public partial class Form1 : Form
    {
        private World world = null;
        private RegionFile region = null;
        private HistoryManager history = null;
        private String lastPath = String.Format("{0}{1}.minecraft{1}saves", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Path.DirectorySeparatorChar);

        private int lastSelectedRegionIndex = -1;
        private Dimension dim = Dimension.Overworld;
        private bool warnedAboutPopulating = false;

        private readonly int SELECTIONLAYER;
        private readonly int BRUSHLAYER;
        private readonly int POPULATELAYER;
        private readonly int CHUNKLAYER;
        private readonly int BIOMELAYER;
        private readonly int MAPLAYER;

        private String[] args;

        public Form1(String[] args)
        {
            InitializeComponent();
            SELECTIONLAYER = imgRegion.SelectionLayerIndex;
            BRUSHLAYER = imgRegion.BrushLayerIndex;
            POPULATELAYER = imgRegion.AddLayer(new Layer(imgRegion.Width, imgRegion.Height, 0.6f, false, false)); //chunks to be populated
            CHUNKLAYER = imgRegion.AddLayer(new Layer(imgRegion.Width, imgRegion.Height, 0.3f, true, false)); //chunk boundaries
            BIOMELAYER = imgRegion.AddLayer(new Layer(imgRegion.Width, imgRegion.Height, 0.5f)); //biome
            MAPLAYER = imgRegion.AddLayer(new Layer(imgRegion.Width, imgRegion.Height, 1.0f)); //map
            this.args = args;
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            menuRedrawTerrainMap.Checked = Settings.RedrawTerrainMap;
            menuBiomeFoliage.Checked = Settings.BiomeFoliage;
            menuTransparency.Checked = Settings.Transparency;
            FillRecentWorldsList();

            RegionUtil.RenderChunkBoundaries(imgRegion.Layers[CHUNKLAYER].Image);

            FillLists();

            history = new HistoryManager(HistoryChange);
            history.RecordSelectionState(imgRegion.Layers[SELECTIONLAYER].Image, "Initial Selection");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            imgRegion.Focus(); //otherwise the undo button was starting highlighted
            if (args != null && args.Length > 0 && !String.IsNullOrWhiteSpace(args[0]))
            {
                OpenWorld(args[0]);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SaveIfNecessary())
                e.Cancel = true;
            else
                Settings.Save();
        }

        private void ResetControls()
        {
            lstRegions.Items.Clear();
            lastSelectedRegionIndex = -1;
            trackMagnification.Value = 1;
            lblMagnification.Text = "Magnification: 1x";
            imgRegion.Reset();
            region = null;
            dim = Dimension.Overworld;
            overworldToolStripMenuItem.Checked = true;
            netherToolStripMenuItem.Checked = false;
            endToolStripMenuItem.Checked = false;
            if (history != null)
                history.Dispose();
            history = new HistoryManager(HistoryChange);
            history.RecordSelectionState(imgRegion.Layers[SELECTIONLAYER].Image, "Initial State");
        }

        public void FillLists()
        {
            List<BiomeType> biomes = new List<BiomeType>();
            foreach (BiomeType b in BiomeType.Biomes)
            {
                if (b == null || b.ID == 255)
                    continue;

                biomes.Add(b);
            }

            biomes.Sort();
            BiomeType[] temp = biomes.ToArray();

            cmbFill.Items.Clear();
            cmbReplace1.Items.Clear();
            cmbReplace2.Items.Clear();
            cmbBiomeType.Items.Clear();
            cmbBlockType.Items.Clear();

            cmbFill.Items.AddRange(temp);
            cmbReplace1.Items.AddRange(temp);
            cmbReplace2.Items.AddRange(temp);
            cmbBiomeType.Items.AddRange(temp);

            String[] versions = { "Minecraft 1.5.2", "MC 1.5.2 (Large biomes)", "Minecraft 1.4.7", "MC 1.4.7 (Large biomes)", "Minecraft 1.3.2", "MC 1.3.2 (Large biomes)", "Minecraft 1.2.5", "Minecraft 1.1.0", "Minecraft 1.0.0", "Minecraft Beta 1.8.1", "Minecraft Beta 1.7.3", "Minecraft Beta 1.3_01" };

            cmbFill.Items.AddRange(versions);
            cmbReplace2.Items.AddRange(versions);

            cmbFill.SelectedIndex = 0;
            cmbReplace1.SelectedIndex = 0;
            cmbReplace2.SelectedIndex = 0;
            cmbBiomeType.SelectedIndex = 0;

            cmbBlockType.Items.AddRange(new String[] { "Cacti & Dead Bushes", "Dirt & Grass", "Flowers & Tall Grass", "Gravel", "Lily Pads & Vines", "Leaves & Logs", "Ice", "Sand", "Snow", "Stone", "Water", "Input Block ID" });
            cmbBlockType.SelectedIndex = 0;

        }

        private void UpdateStatus(String status)
        {
            lblStatus.Text = status;
            lblStatus.Refresh();
        }

        private void HistoryChange(String undoDescription, String redoDescription)
        {
            if (undoDescription != null)
            {
                undoToolStripMenuItem.Text = "&Undo " + undoDescription;
                undoToolStripMenuItem.Enabled = true;
                toolTip.SetToolTip(btnUndo, String.Format("Undo {0} (Ctrl+Z)", undoDescription));
                btnUndo.Enabled = true;
            }
            else
            {
                undoToolStripMenuItem.Text = "&Undo";
                undoToolStripMenuItem.Enabled = false;
                toolTip.SetToolTip(btnUndo, "Undo (Ctrl+Z)");
                btnUndo.Enabled = false;
            }

            if (redoDescription != null)
            {
                redoToolStripMenuItem.Text = "&Redo " + redoDescription;
                redoToolStripMenuItem.Enabled = true;
                toolTip.SetToolTip(btnRedo, String.Format("Redo {0} (Ctrl+Y)", redoDescription));
                btnRedo.Enabled = true;
            }
            else
            {
                redoToolStripMenuItem.Text = "&Redo";
                redoToolStripMenuItem.Enabled = false;
                toolTip.SetToolTip(btnRedo, "Redo (Ctrl+Y)");
                btnRedo.Enabled = false;
            }
        }

        private void TrySwitchRegion(int x, int z)
        {
            int i = lstRegions.FindString(String.Format("Region {0}, {1} ::", x, z));
            if (i == ListBox.NoMatches)
            {
                MessageBox.Show(this, "Sorry, that region does not exist yet.", "Load", MessageBoxButtons.OK);
            }
            else
            {
                lstRegions.SelectedIndex = i;
            }
        }

        //return false if stop, go back, don't do whatever
        private bool SaveIfNecessary()
        {
            if (region == null)
                return true;

            history.SetDirtyFlags(region);

            if (region.Dirty)
            {
                DialogResult res = MessageBox.Show(this, "The current region has been modified. Do you want to save your changes?", "Save", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    UpdateStatus("Writing region file");
                    region.Write();
                    UpdateStatus("");
                    history.SetLastSaveActions();
                    return true;
                }
                else if (res == DialogResult.Cancel)
                    return false;
            }

            return true;
        }

        private void SwitchDimension(Dimension newDim)
        {
            overworldToolStripMenuItem.Checked = dim == Dimension.Overworld;
            netherToolStripMenuItem.Checked = dim == Dimension.Nether;
            endToolStripMenuItem.Checked = dim == Dimension.End;

            if (world == null)
                return;
            if (!SaveIfNecessary())
                return;
            if (newDim == dim)
                return;
            
            ResetControls();

            dim = newDim;
            overworldToolStripMenuItem.Checked = dim == Dimension.Overworld;
            netherToolStripMenuItem.Checked = dim == Dimension.Nether;
            endToolStripMenuItem.Checked = dim == Dimension.End;

            String[] regions = world.GetRegionPaths(dim);
            foreach (String r in regions)
                lstRegions.Items.Add(RegionFile.ToString(r));
            return;

        }

        private void FillRecentWorldsList(bool justClear = false)
        {
            if (justClear)
            {
                DialogResult res = MessageBox.Show(this, "Are you sure you want to clear the list of recent worlds?", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
                if (res != DialogResult.Yes)
                    return;
            }

            for (int i = menuRecentWorlds.DropDownItems.Count - 1; i >= 0; i--)
            {
                if (menuRecentWorlds.DropDownItems[i] != menuRecentWorldsSeparator && menuRecentWorlds.DropDownItems[i] != menuClearRecentWorlds)
                    menuRecentWorlds.DropDownItems.RemoveAt(i);
            }

            if (justClear)
            {
                Settings.ClearRecentWorlds();
                return;
            }

            List<String> worlds = new List<string>();

            for (int i = 0; i < Settings.RecentWorlds.Count; i++)
            {
                worlds.Add(String.Format("&{0} {1} ({2})", worlds.Count + 1, Regex.Replace(Settings.RecentWorlds[i].Path, @"^.*([\/\\][^\/\\]+[\/\\]level.dat)$", "...$1"), Settings.RecentWorlds[i].Name));
            }

            for (int i = 0; i < worlds.Count; i++)
            {
                menuRecentWorlds.DropDownItems.Insert(i, new ToolStripMenuItem(worlds[i], null, menuRecentWorldItem_Click));
            }

        }

        private void OpenWorld(String path)
        {
            ResetControls();

            try
            {
                world = new World(path);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(this, String.Format("The file \"{0}\" could not be found. Unable to open world.", path), "Open", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                world = null;
                return;
            }
            catch (Exception)
            {
                MessageBox.Show(this, String.Format("The file \"{0}\" could not be parsed. Please be sure that it is a valid Minecraft level.dat.", path), "Open", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                world = null;
                return;
            }

            lastPath = Path.GetDirectoryName(path);

            String[] regions = world.GetRegionPaths();
            if (regions.Length == 0)
            {
                MessageBox.Show(this, "No region files (*.mca) found in the main, overworld dimension. If the world is older than March 2012 open it in Minecraft 1.2 or later to convert to the Anvil format. Otherwise try switching dimensions from the file menu.", "Open", MessageBoxButtons.OK);
                this.Text = "Biome Painter";
            }
            else
            {
                foreach (String r in regions)
                    lstRegions.Items.Add(RegionFile.ToString(r));
            }

            Settings.AddRecentWorld(path, world.WorldName);
            FillRecentWorldsList();
            this.Text = world.WorldName + " - Biome Painter";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D1:
                    chkShowMap.Checked = !chkShowMap.Checked;
                    return true;
                case Keys.D2:
                    chkShowBiomes.Checked = !chkShowBiomes.Checked;
                    return true;
                case Keys.D3:
                    chkShowToolTips.Checked = !chkShowToolTips.Checked;
                    return true;
                case Keys.D4:
                    chkShowPopulate.Checked = !chkShowPopulate.Checked;
                    return true;
                case Keys.D5:
                    chkShowSelection.Checked = !chkShowSelection.Checked;
                    return true;
                case Keys.D6:
                    chkShowBrush.Checked = !chkShowBrush.Checked;
                    return true;
                case Keys.D7:
                    chkShowChunkBoundaries.Checked = !chkShowChunkBoundaries.Checked;
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        #region Menus

        private void openWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SaveIfNecessary())
                return;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = lastPath;
            dialog.Filter = "Minecraft level (*.dat)|*.dat";
            dialog.RestoreDirectory = false;

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            OpenWorld(dialog.FileName);
        }

        private void closeWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SaveIfNecessary())
                return;

            world = null;
            this.Text = "Biome Painter";
            ResetControls();
        }

        private void saveCurrentRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSaveRegion_Click(this, null);
        }

        private void reloadCurrentRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;

            UpdateStatus("Reading region file");
            region = new RegionFile(region.Path);
            history.RecordBiomeState(region, "Reload Biomes");
            history.RecordPopulateState(region, "Reload Populate Flags");
            history.SetLastSaveActions();
            UpdateStatus("Generating terrain map");
            RegionUtil.RenderRegion(region, imgRegion.Layers[MAPLAYER].Image);
            UpdateStatus("Generating biome map");
            RegionUtil.RenderRegionBiomes(region, imgRegion.Layers[BIOMELAYER].Image, imgRegion.ToolTips);
            UpdateStatus("");
            RegionUtil.RenderRegionChunkstobePopulated(region, imgRegion.Layers[POPULATELAYER].Image);
            imgRegion.Redraw();
        }

        private void aboveCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRegionUp_Click(this, null);
        }

        private void belowCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRegionDown_Click(this, null);
        }

        private void leftOfCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRegionLeft_Click(this, null);
        }

        private void rightOfCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRegionRight_Click(this, null);
        }

        private void loadRegionByCoordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRegionJump_Click(this, null);
        }

        private void overworldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchDimension(Dimension.Overworld);
        }

        private void netherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchDimension(Dimension.Nether);
        }

        private void endToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchDimension(Dimension.End);
        }

        private void menuClearRecentWorlds_Click(object sender, EventArgs e)
        {
            FillRecentWorldsList(true);
        }

        private void menuRecentWorldItem_Click(object sender, EventArgs e)
        {
            if (!SaveIfNecessary())
                return;

            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            String path = Settings.RecentWorlds[int.Parse(Regex.Match(item.Text, @"^&(\d+) ").Groups[1].Value) - 1].Path;
            if (File.Exists(path))
                OpenWorld(path);
            else
            {
                DialogResult res = MessageBox.Show(this, String.Format("Unable to open \"{0}\", file not found. Would you like to remove it from the recent worlds list?", path), "Open", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
                if (res == DialogResult.Yes)
                {
                    Settings.RemoveRecentWorld(path);
                    FillRecentWorldsList();
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnUndo_Click(this, null);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRedo_Click(this, null);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCopy_Click(this, null);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPaste_Click(this, null);
        }

        private void setChunksInSelectionToBePopulatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;

            if (!warnedAboutPopulating)
            {
                DialogResult res = MessageBox.Show(this, String.Format("Setting a chunk to be popluated by Minecraft means the next time that chunk is loaded in Minecraft it will be filled with trees, snow cover, water, lava, and ores depending on the biome(s) the chunk is in.{0}{0}If that chunk has already been populated or already has player-made structures in it, you may find it clogged with more foliage than you wanted. Also smooth stone in your structures may be replaced with ores, dirt, or gravel. I strongly suggest you make a backup copy of your world before using this feature.{0}{0}Are you sure you want to proceed? As always, changes can be undone and won't take effect until you save the region.", Environment.NewLine), "DANGER", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (res == DialogResult.Cancel)
                    return;
                warnedAboutPopulating = true;
            }

            if (!chkShowPopulate.Checked)
                chkShowPopulate.Checked = true;

            RegionUtil.SetChunkstobePopulated(region, imgRegion.Layers[SELECTIONLAYER].Image, imgRegion.SelectionColor, 0);
            RegionUtil.RenderRegionChunkstobePopulated(region, imgRegion.Layers[POPULATELAYER].Image);
            imgRegion.Redraw();
            history.RecordPopulateState(region, "Set Populate Flags");
        }

        private void unsetChunksInSelectionToBePopulatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;

            if (!chkShowPopulate.Checked)
                chkShowPopulate.Checked = true;

            RegionUtil.SetChunkstobePopulated(region, imgRegion.Layers[SELECTIONLAYER].Image, imgRegion.SelectionColor, 1);
            RegionUtil.RenderRegionChunkstobePopulated(region, imgRegion.Layers[POPULATELAYER].Image);
            imgRegion.Redraw();
            history.RecordPopulateState(region, "Unset Populate Flags");
        }

        private void batchFillEntireWorldWithSelectedBiomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (world == null)
                return;

            Batch form = new Batch(world.GetRegionDirectory(dim), Path.GetFileName(world.WorldDir), dim.ToString(), false, cmbFill.SelectedItem, null, world.Seed);
            form.ShowDialog(this);
            form.Dispose();
        }

        private void batchReplaceEntireWorldWithSelectedBiomesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (world == null)
                return;

            Batch form = new Batch(world.GetRegionDirectory(dim), Path.GetFileName(world.WorldDir), dim.ToString(), true, cmbReplace1.SelectedItem, cmbReplace2.SelectedItem, world.Seed);
            form.ShowDialog(this);
            form.Dispose();
        }

        private void editWorldSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (world == null)
                return;
            Seed form = new Seed(world);
            form.ShowDialog(this);
            form.Dispose();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSelectAll_Click(this, null);
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSelectNone_Click(this, null);
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnInvertSelection_Click(this, null);
        }

        private void chunksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSelectChunks_Click(this, null);
        }

        private void menuRedrawTerrainMap_Click(object sender, EventArgs e)
        {
            Settings.RedrawTerrainMap = menuRedrawTerrainMap.Checked;
        }

        private void menuBiomeFoliage_Click(object sender, EventArgs e)
        {
            Settings.BiomeFoliage = menuBiomeFoliage.Checked;
        }

        private void menuTransparency_Click(object sender, EventArgs e)
        {
            Settings.Transparency = menuTransparency.Checked;
        }

        private void editCustomBlockColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Blocks.user.txt");
            if (!File.Exists(path))
            {
                using (File.Create(path))
                {
                }
            }
            Process.Start(path);
        }

        private void editCustomBiomeDefinitionsAndColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Biomes.user.txt");
            if (!File.Exists(path))
            {
                using (File.Create(path))
                {
                }
            }
            Process.Start(path);
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Update().ShowDialog(this);
        }

        private void aboutBiomePainterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About form = new About();
            form.ShowDialog(this);
            form.Dispose();
        }

        #endregion

        #region Checkboxes

        private void chkShowMap_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.Layers[MAPLAYER].Visible = chkShowMap.Checked;
            imgRegion.Redraw();
        }

        private void chkShowBiomes_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.Layers[BIOMELAYER].Visible = chkShowBiomes.Checked;
            imgRegion.Redraw();
        }

        private void chkShowToolTips_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.ShowToolTips = chkShowToolTips.Checked;
        }

        private void chkShowPopulate_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.Layers[POPULATELAYER].Visible = chkShowPopulate.Checked;
            imgRegion.Redraw();
        }

        private void chkShowChunkBoundaries_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.Layers[CHUNKLAYER].Visible = chkShowChunkBoundaries.Checked;
            imgRegion.Redraw();
        }

        private void chkShowBrush_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.Layers[BRUSHLAYER].Visible = chkShowBrush.Checked;
            imgRegion.Redraw();
        }

        private void chkShowSelection_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.Layers[SELECTIONLAYER].Visible = chkShowSelection.Checked;
            imgRegion.Redraw();
        }

        private void radRoundBrush_CheckedChanged(object sender, EventArgs e)
        {
            if (radRoundBrush.Checked)
            {
                imgRegion.Brush = BrushType.Round;
                imgRegion.CancelCustomBrush();
            }
        }

        private void radSquareBrush_CheckedChanged(object sender, EventArgs e)
        {
            if (radSquareBrush.Checked)
            {
                imgRegion.Brush = BrushType.Square;
                imgRegion.CancelCustomBrush();
            }
        }

        private void radRectangleSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (radRectangleSelect.Checked)
            {
                imgRegion.Brush = BrushType.Rectangle;
                imgRegion.CancelCustomBrush();
            }
        }

        private void radFill_CheckedChanged(object sender, EventArgs e)
        {
            if (radFill.Checked)
            {
                imgRegion.Brush = BrushType.Fill;
                imgRegion.CancelCustomBrush();
            }
        }
        #endregion

        #region Buttons

        private void btnRegionLeft_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;
            TrySwitchRegion(region.Coords.X - 1, region.Coords.Z);
        }

        private void btnRegionUp_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;
            TrySwitchRegion(region.Coords.X, region.Coords.Z - 1);

        }

        private void btnRegionRight_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;
            TrySwitchRegion(region.Coords.X + 1, region.Coords.Z);
        }

        private void btnRegionDown_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;
            TrySwitchRegion(region.Coords.X, region.Coords.Z + 1);
        }

        private void btnRegionJump_Click(object sender, EventArgs e)
        {
            if (world == null)
                return;

            String msg = "Type absolute x and z block coordinates (x, z) to load the region that contains the specified point.";
            String input = "0 0";
            while (true)
            {
                input = InputBox.Show(msg, "Load", input);
                if (input.Length == 0)
                    return;

                Match m = Regex.Match(input, @"([-\+]?\d+)(?:[,\s]+)([-\+]?\d+)");
                if (m.Groups.Count < 3)
                {
                    msg = "Unable to parse coordinates. Please try again or click cancel.";
                }
                else
                {
                    int x, z;
                    if (!Int32.TryParse(m.Groups[1].Value, out x) || !Int32.TryParse(m.Groups[2].Value, out z))
                    {
                        msg = "Unable to parse coordinates. Please try again or click cancel.";
                    }
                    else
                    {
                        Coord c = new Coord(x, z);
                        c.AbsolutetoRegion();
                        TrySwitchRegion(c.X, c.Z);
                        return;
                    }
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            imgRegion.SelectAll();
            imgRegion.Redraw();
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            imgRegion.SelectNone();
            imgRegion.Redraw();
        }

        private void btnInvertSelection_Click(object sender, EventArgs e)
        {
            UpdateStatus("Inverting selection");
            imgRegion.InvertSelection();
            UpdateStatus("");
            imgRegion.Redraw();
        }

        private void btnSelectChunks_Click(object sender, EventArgs e)
        {
            UpdateStatus("Expanding selection");
            RegionUtil.SelectChunks(imgRegion.Layers[SELECTIONLAYER].Image, imgRegion.SelectionColor);
            UpdateStatus("");
            imgRegion.Redraw();
            history.RecordSelectionState(imgRegion.Layers[SELECTIONLAYER].Image, "Select Chunks");
        }

        private void btnAddorRemovebyBlocks_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;
            int[] blockIds = null;
            bool add = sender == btnAddbyBlocks ? true : false;
            switch ((String)cmbBlockType.SelectedItem)
            {
                case "Cacti & Dead Bushes":
                    blockIds = new int[] { 81, 32 };
                    break;
                case "Dirt & Grass":
                    blockIds = new int[] { 2, 3 };
                    break;
                case "Flowers & Tall Grass":
                    blockIds = new int[] { 31, 37, 38 };
                    break;
                case "Gravel":
                    blockIds = new int[] { 13 };
                    break;
                case "Lily Pads & Vines":
                    blockIds = new int[] { 111, 106 };
                    break;
                case "Leaves & Logs":
                    blockIds = new int[] { 17, 18 };
                    break;
                case "Ice":
                    blockIds = new int[] { 79 };
                    break;
                case "Sand":
                    blockIds = new int[] { 12 };
                    break;
                case "Snow":
                    blockIds = new int[] { 78, 80 };
                    break;
                case "Stone":
                    blockIds = new int[] { 1 };
                    break;
                case "Water":
                    blockIds = new int[] { 8, 9 };
                    break;
                case "Input Block ID":
                    {
                        String msg = String.Format("Type the ID number of the block type you want to {0}.{1}See http://www.minecraftwiki.net/wiki/Data_values for more info.", add ? "add to the selection" : "remove from the selection", Environment.NewLine);
                        String input = "";
                        while (true)
                        {
                            input = InputBox.Show(msg, add ? "Add to Selection" : "Remove From Selection", input);
                            if (input.Length == 0)
                                break;

                            Match m = Regex.Match(input, @"^(\d+)$");
                            if (m.Groups.Count < 2)
                            {
                                msg = "Unable to parse block ID. Please try again or click cancel.";
                            }
                            else
                            {
                                int id;
                                if (!Int32.TryParse(m.Groups[1].Value, out id))
                                {
                                    msg = "Unable to parse block ID. Please try again or click cancel.";
                                }
                                else
                                {
                                    blockIds = new int[] { id };
                                    break;
                                }
                            }
                        }
                    }
                    break;
            }
            if (blockIds != null)
            {
                UpdateStatus(add ? "Adding to selection" : "Removing from selection");
                RegionUtil.AddorRemoveBlocksSelection(region, imgRegion.Layers[SELECTIONLAYER].Image, imgRegion.SelectionColor, blockIds, add);
                UpdateStatus("");
                imgRegion.Redraw();
                history.RecordSelectionState(imgRegion.Layers[SELECTIONLAYER].Image, add ? "Select by Block" : "Deselect by Block");
            }
        }

        private void btnAddorRemovebyBiomes_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;
            bool add = sender == btnAddbyBiomes ? true : false;
            UpdateStatus(add ? "Adding to selection" : "Removing from selection");
            RegionUtil.AddorRemoveBiomesSelection(region, imgRegion.Layers[SELECTIONLAYER].Image, imgRegion.SelectionColor, ((BiomeType)cmbBiomeType.SelectedItem).ID, add);
            UpdateStatus("");
            imgRegion.Redraw();
            history.RecordSelectionState(imgRegion.Layers[SELECTIONLAYER].Image, add ? "Select by Biome" : "Deselect by Biome");
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            String[,] tooltips = imgRegion.ToolTips;
            history.Undo(imgRegion.Layers[SELECTIONLAYER].Image, region, Settings.RedrawTerrainMap ? imgRegion.Layers[MAPLAYER].Image : null, imgRegion.Layers[BIOMELAYER].Image, ref tooltips, imgRegion.Layers[POPULATELAYER].Image, UpdateStatus);
            imgRegion.ToolTips = tooltips;
            imgRegion.Redraw();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            String[,] tooltips = imgRegion.ToolTips;
            history.Redo(imgRegion.Layers[SELECTIONLAYER].Image, region, Settings.RedrawTerrainMap ? imgRegion.Layers[MAPLAYER].Image : null, imgRegion.Layers[BIOMELAYER].Image, ref tooltips, imgRegion.Layers[POPULATELAYER].Image, UpdateStatus);
            imgRegion.ToolTips = tooltips;
            imgRegion.Redraw();
        }

        private void btnSaveRegion_Click(object sender, EventArgs e)
        {
            if (region == null)
                return;

            history.SetDirtyFlags(region);
            UpdateStatus("Writing region file");
            region.Write();
            UpdateStatus("");
            history.SetLastSaveActions();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;
            ClipboardManager.Copy(region, imgRegion.Layers[SELECTIONLAYER].Image, imgRegion.SelectionColor);
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;

            Bitmap paste = ClipboardManager.StartPaste();
            if (paste != null)
            {
                imgRegion.SetCustomBrush(paste);
            }
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;

            UpdateStatus("Filling selected area");
            RegionUtil.Fill(region, imgRegion.Layers[SELECTIONLAYER].Image, imgRegion.SelectionColor, cmbFill.SelectedItem, world.Seed);
            if (Settings.RedrawTerrainMap)
            {
                UpdateStatus("Generating terrain map");
                RegionUtil.RenderRegion(region, imgRegion.Layers[MAPLAYER].Image);
            }
            UpdateStatus("Generating biome map");
            RegionUtil.RenderRegionBiomes(region, imgRegion.Layers[BIOMELAYER].Image, imgRegion.ToolTips);
            UpdateStatus("");
            imgRegion.Redraw();
            history.RecordBiomeState(region, "Fill Biomes");
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;

            UpdateStatus("Replacing in selected area");
            RegionUtil.Replace(region, imgRegion.Layers[SELECTIONLAYER].Image, imgRegion.SelectionColor, ((BiomeType)cmbReplace1.SelectedItem).ID, cmbReplace2.SelectedItem, world.Seed);
            if (Settings.RedrawTerrainMap)
            {
                UpdateStatus("Generating terrain map");
                RegionUtil.RenderRegion(region, imgRegion.Layers[MAPLAYER].Image);
            }
            UpdateStatus("Generating biome map");
            RegionUtil.RenderRegionBiomes(region, imgRegion.Layers[BIOMELAYER].Image, imgRegion.ToolTips);
            UpdateStatus("");
            imgRegion.Redraw();
            history.RecordBiomeState(region, "Replace Biomes");
        }

        #endregion

        #region Other event handlers

        private void lstRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (world == null || lstRegions.SelectedIndex == lastSelectedRegionIndex)
                return;

            if (!SaveIfNecessary())
            {
                lstRegions.SelectedIndex = lastSelectedRegionIndex;
                return;
            }

            history.FilterOutType(typeof(BiomeAction));
            history.FilterOutType(typeof(PopulateAction));

            Match m = Regex.Match(lstRegions.SelectedItem.ToString(), @"Region (-?\d+), (-?\d+)");
            int x = int.Parse(m.Groups[1].Value);
            int z = int.Parse(m.Groups[2].Value);
            String pathFormat = String.Format("{0}{1}r.{{0}}.{{1}}.mca", world.GetRegionDirectory(dim), Path.DirectorySeparatorChar);

            UpdateStatus("Reading region file");
            region = new RegionFile(String.Format(pathFormat, x, z));
            history.RecordBiomeState(region, "Initial Biomes");
            history.RecordPopulateState(region, "Initial Populate Flags");
            history.SetLastSaveActions();
            imgRegion.Reset();
            UpdateStatus("Generating terrain map");
            RegionUtil.RenderRegion(region, imgRegion.Layers[MAPLAYER].Image, false);
            UpdateStatus("Generating biome map");
            RegionUtil.RenderRegionBiomes(region, imgRegion.Layers[BIOMELAYER].Image, imgRegion.ToolTips, false);
            UpdateStatus("");
            RegionUtil.RenderRegionChunkstobePopulated(region, imgRegion.Layers[POPULATELAYER].Image, false);
            imgRegion.Redraw();

            RegionFile[,] surrounding = new RegionFile[3, 3];
            UpdateStatus("Reading surrounding chunks");
            surrounding[0, 0] = new RegionFile(String.Format(pathFormat, x - 1, z - 1), 30, 31, 30, 31);
            surrounding[1, 0] = new RegionFile(String.Format(pathFormat, x, z - 1), 0, 31, 30, 31);
            surrounding[2, 0] = new RegionFile(String.Format(pathFormat, x + 1, z - 1), 0, 1, 30, 31);
            surrounding[0, 1] = new RegionFile(String.Format(pathFormat, x - 1, z), 30, 31, 0, 31);
            surrounding[1, 1] = null;
            surrounding[2, 1] = new RegionFile(String.Format(pathFormat, x + 1, z, 0, 1, 0, 31));
            surrounding[0, 2] = new RegionFile(String.Format(pathFormat, x - 1, z + 1), 30, 31, 0, 1);
            surrounding[1, 2] = new RegionFile(String.Format(pathFormat, x, z + 1), 0, 31, 0, 1);
            surrounding[2, 2] = new RegionFile(String.Format(pathFormat, x + 1, z + 1), 0, 1, 0, 1);
            UpdateStatus("Generating map for surrounding chunks");
            RegionUtil.RenderSurroundingRegions(surrounding, imgRegion.Layers[MAPLAYER].Image, imgRegion.Layers[BIOMELAYER].Image, imgRegion.ToolTips, imgRegion.Layers[POPULATELAYER].Image);
            UpdateStatus("");
            imgRegion.Redraw();

            lastSelectedRegionIndex = lstRegions.SelectedIndex;
        }

        private void trackBrushDiameter_Scroll(object sender, EventArgs e)
        {
            lblBrushDiameter.Text = String.Format("Brush Diameter: {0}", trackBrushDiameter.Value);
            imgRegion.BrushDiameter = trackBrushDiameter.Value;
        }

        private void trackMagnification_Scroll(object sender, EventArgs e)
        {
            imgRegion.Zoom(trackMagnification.Value, imgRegion.OffsetX, imgRegion.OffsetY);
        }

        private void imgRegion_ZoomEvent(object sender, ZoomEventArgs e)
        {
            lblMagnification.Text = String.Format("Magnification: {0}x", e.NewMagnification);
            trackMagnification.Value = e.NewMagnification;
        }

        private void imgRegion_BrushDiameterChanged(object sender, BrushDiameterEventArgs e)
        {
            lblBrushDiameter.Text = String.Format("Brush Diameter: {0}", e.NewBrushDiameter);
            trackBrushDiameter.Value = e.NewBrushDiameter;
        }

        private void imgRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            history.RecordSelectionState(imgRegion.Layers[SELECTIONLAYER].Image, e.Description);
        }


        private void imgRegion_CustomBrushClick(object sender, CustomBrushClickEventArgs e)
        {
            if (ClipboardManager.Paste(region, e.MouseX - RegionUtil.OFFSETX, e.MouseY - RegionUtil.OFFSETY))
            {
                if (Settings.RedrawTerrainMap)
                {
                    UpdateStatus("Generating terrain map");
                    RegionUtil.RenderRegion(region, imgRegion.Layers[MAPLAYER].Image);
                }
                UpdateStatus("Generating biome map");
                RegionUtil.RenderRegionBiomes(region, imgRegion.Layers[BIOMELAYER].Image, imgRegion.ToolTips);
                UpdateStatus("");
                imgRegion.Redraw();
                history.RecordBiomeState(region, "Paste Biomes");
            }
        }
        #endregion
    }

}
