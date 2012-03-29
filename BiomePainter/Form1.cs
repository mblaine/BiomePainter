using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BitmapSelector;
using Minecraft;

namespace BiomePainter
{
    public partial class Form1 : Form
    {
        private World world = null;
        private RegionFile region = null;
        private String lastPath = String.Format("{0}{1}.minecraft{1}saves", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Path.DirectorySeparatorChar);

        private int lastSelectedRegionIndex = -1;

        private const String NEEDTOSAVEMSG = "Biomes for the current region have been modified. Do you want to save your changes?";
        private const int SELECTIONLAYER = 0;
        private const int CHUNKLAYER = 1;
        private const int BIOMELAYER = 2;
        private const int MAPLAYER = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            imgRegion.Layers.Add(new BitmapSelector.Layer(512, 512, 0.5f, true, false)); //chunk boundaries
            imgRegion.Layers.Add(new BitmapSelector.Layer(512, 512, 0.5f)); //biome
            imgRegion.Layers.Add(new BitmapSelector.Layer(512, 512, 1.0f)); //map

            World.RenderChunkBoundaries(imgRegion.Layers[CHUNKLAYER].Image);

            List<String> names = new List<String>();
            foreach (Biome b in Enum.GetValues(typeof(Biome)))
            {
                if (b == Biome.Unspecified)
                    continue;

                names.Add(b.ToString());
            }

            names.Sort();
            String[] temp = names.ToArray();
            
            cmbFill.Items.AddRange(temp);
            cmbReplace1.Items.AddRange(temp);
            cmbReplace2.Items.AddRange(temp);

            String[] versions = { "Minecraft Beta 1.7.3", "Minecraft Beta 1.8.1", "Minecraft 1.0.0", "Minecraft 1.1.0", "Minecraft 1.2.4" };

            cmbFill.Items.AddRange(versions);
            cmbReplace2.Items.AddRange(versions);

            cmbFill.SelectedIndex = 0;
            cmbReplace1.SelectedIndex = 0;
            cmbReplace2.SelectedIndex = 0;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (region != null && region.Dirty)
            {
                DialogResult res = MessageBox.Show(this, NEEDTOSAVEMSG, "Save", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    lblStatus.Text = "Writing region file";

                    UpdateStatus("Writing region file");
                    region.Write();
                    UpdateStatus("");
                    
                }
                else if (res == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void ResetControls()
        {
            lstRegions.Items.Clear();
            lastSelectedRegionIndex = -1;
            trackMagnification.Value = 1;
            lblMagnification.Text = "Magnification: 1x";
            imgRegion.Reset();
            region = null;
            world = null;
        }

        private void UpdateStatus(String status)
        {
            lblStatus.Text = status;
            lblStatus.Refresh();
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

        #region Menus

        private void openWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (region != null && region.Dirty)
            {
                DialogResult res = MessageBox.Show(this, NEEDTOSAVEMSG, "Save", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    UpdateStatus("Writing region file");
                    region.Write();
                    UpdateStatus("");
                }
                else if (res == DialogResult.Cancel)
                    return;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = lastPath;
            dialog.Filter = "Minecraft level (*.dat)|*.dat";
            dialog.RestoreDirectory = false;

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            ResetControls();

            lastPath = Path.GetDirectoryName(dialog.FileName);
            world = new World(dialog.FileName);

            String[] regions = world.GetRegionPaths();
            if (regions.Length == 0)
            {
                MessageBox.Show(this, "No region files (*.mca) found. Be sure to convert a world to the Anvil format by first opening it in Minecraft 1.2 or later.", "Open", MessageBoxButtons.OK);
                world = null;
                return;
            }
            foreach(String r in regions)
                lstRegions.Items.Add(RegionFile.ToString(r));
        }

        private void closeWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (region != null && region.Dirty)
            {
                DialogResult res = MessageBox.Show(this, NEEDTOSAVEMSG, "Save", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    UpdateStatus("Writing region file");
                    region.Write();
                    UpdateStatus("");
                }
                else if (res == DialogResult.Cancel)
                    return;
            }

            ResetControls();
        }

        private void saveCurrentRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (region == null)
                return;

            UpdateStatus("Writing region file");
            region.Write();
            UpdateStatus("");
        }

        private void reloadCurrentRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(region == null)
                return;
            else if(region.Dirty)
            {
                DialogResult res = MessageBox.Show(this, NEEDTOSAVEMSG, "Save", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    UpdateStatus("Writing region file");
                    region.Write();
                    UpdateStatus("");
                }
                else if (res == DialogResult.Cancel)
                    return;
            }

            UpdateStatus("Reading region file");
            region = new RegionFile(region.Path);
            UpdateStatus("Generating terrain map");
            world.RenderRegion(region, imgRegion.Layers[MAPLAYER].Image);
            UpdateStatus("Generating biome map");
            world.RenderRegionBiomes(region, imgRegion.Layers[BIOMELAYER].Image, imgRegion.ToolTips);
            UpdateStatus("");
            imgRegion.Redraw();
        }

        private void aboveCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;
            TrySwitchRegion(region.Coords.X, region.Coords.Z - 1);
        }

        private void belowCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;
            TrySwitchRegion(region.Coords.X, region.Coords.Z + 1);
        }

        private void leftOfCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;
            TrySwitchRegion(region.Coords.X - 1, region.Coords.Z);
        }

        private void rightOfCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;
            TrySwitchRegion(region.Coords.X + 1, region.Coords.Z);
        }

        private void loadRegionByCoordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (world == null)
                return;

            String msg = "Type absolute x and z block coordinates (x, z) to load the region that contains the specified point.";
            String input = "";
            while (true)
            {
                input = Microsoft.VisualBasic.Interaction.InputBox(msg, "Load", input);
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

        private void aboutBiomePainterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog(this);
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

        private void chkShowChunkBoundaries_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.Layers[CHUNKLAYER].Visible = chkShowChunkBoundaries.Checked;
            imgRegion.Redraw();
        }

        private void chkShowSelection_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.Layers[SELECTIONLAYER].Visible = chkShowSelection.Checked;
            imgRegion.Redraw();
        }

        private void radRoundBrush_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.Brush = BrushType.Round;
        }

        private void radSquareBrush_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.Brush = BrushType.Square;
        }

        #endregion

        #region Buttons

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
            World.SelectChunks(imgRegion.Layers[SELECTIONLAYER].Image, imgRegion.SelectionColor);
            UpdateStatus("");
            imgRegion.Redraw();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {

        }

        private void btnRedo_Click(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {

        }

        private void btnPaste_Click(object sender, EventArgs e)
        {

        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;

            UpdateStatus("Filling selected area");
            Biome b;
            if (Enum.TryParse<Biome>((String)cmbFill.SelectedItem, out b))
            {
                world.Fill(region, imgRegion.Layers[SELECTIONLAYER].Image, imgRegion.SelectionColor, b);
            }
            else
            {
                BiomeUtil util = null;
                switch ((String)cmbFill.SelectedItem)
                {
                    case "Minecraft Beta 1.7.3":
                        util = new Minecraft.B17.BiomeGenBase(world.Seed);
                        break;
                    case "Minecraft Beta 1.8.1":
                        util = new Minecraft.B18.WorldChunkManager(world.Seed);
                        break;
                    case "Minecraft 1.0.0":
                        util = new Minecraft.F10.WorldChunkManager(world.Seed);
                        break;
                    case "Minecraft 1.1.0":
                        util = new Minecraft.F11.WorldChunkManager(world.Seed);
                        break;
                    case "Minecraft 1.2.4":
                    default:
                        util = new Minecraft.F12.WorldChunkManager(world.Seed);
                        break;
                }
                world.Fill(region, imgRegion.Layers[SELECTIONLAYER].Image, imgRegion.SelectionColor, util);
            }

            imgRegion.ToolTips = new String[imgRegion.Width, imgRegion.Height];
            UpdateStatus("Generating biome map");
            world.RenderRegionBiomes(region, imgRegion.Layers[BIOMELAYER].Image, imgRegion.ToolTips);
            UpdateStatus("");
            imgRegion.Redraw();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;

            UpdateStatus("Replacing in selected area");
            Biome b;
            if (Enum.TryParse<Biome>((String)cmbReplace2.SelectedItem, out b))
            {
                world.Replace(region, imgRegion.Layers[SELECTIONLAYER].Image, imgRegion.SelectionColor, (Biome)Enum.Parse(typeof(Biome), (String)cmbReplace1.SelectedItem), b);
            }
            else
            {
                BiomeUtil util = null;
                switch ((String)cmbReplace2.SelectedItem)
                {
                    case "Minecraft Beta 1.7.3":
                        util = new Minecraft.B17.BiomeGenBase(world.Seed);
                        break;
                    case "Minecraft Beta 1.8.1":
                        util = new Minecraft.B18.WorldChunkManager(world.Seed);
                        break;
                    case "Minecraft 1.0.0":
                        util = new Minecraft.F10.WorldChunkManager(world.Seed);
                        break;
                    case "Minecraft 1.1.0":
                        util = new Minecraft.F11.WorldChunkManager(world.Seed);
                        break;
                    case "Minecraft 1.2.4":
                    default:
                        util = new Minecraft.F12.WorldChunkManager(world.Seed);
                        break;
                }
                world.Replace(region, imgRegion.Layers[SELECTIONLAYER].Image, imgRegion.SelectionColor, (Biome)Enum.Parse(typeof(Biome), (String)cmbReplace1.SelectedItem), util);
            }

            imgRegion.ToolTips = new String[imgRegion.Width, imgRegion.Height];
            UpdateStatus("Generating biome map");
            world.RenderRegionBiomes(region, imgRegion.Layers[BIOMELAYER].Image, imgRegion.ToolTips);
            UpdateStatus("");
            imgRegion.Redraw();
        }

        #endregion

        #region Other event handlers

        private void lstRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (world == null || lstRegions.SelectedIndex == lastSelectedRegionIndex)
                return;

            if (region != null && region.Dirty)
            {
                DialogResult res = MessageBox.Show(this, NEEDTOSAVEMSG, "Save", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    UpdateStatus("Writing region file");
                    region.Write();
                    UpdateStatus("");
                }
                else if (res == DialogResult.Cancel)
                {
                    lstRegions.SelectedIndex = lastSelectedRegionIndex;
                    return;
                }
            }

            Match m = Regex.Match(lstRegions.SelectedItem.ToString(), @"Region (-?\d+), (-?\d+)");
            String path = String.Format("{0}{1}r.{2}.{3}.mca", world.RegionDir, Path.DirectorySeparatorChar, m.Groups[1].Value, m.Groups[2].Value);

            UpdateStatus("Reading region file");
            region = new RegionFile(path);
            imgRegion.Reset();
            UpdateStatus("Generating terrain map");
            world.RenderRegion(region, imgRegion.Layers[MAPLAYER].Image);
            UpdateStatus("Generating biome map");
            world.RenderRegionBiomes(region, imgRegion.Layers[BIOMELAYER].Image, imgRegion.ToolTips);
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

        #endregion
    }

}
