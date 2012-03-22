using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Minecraft;
using BitmapSelector;

namespace BiomePainter
{
    public partial class Form1 : Form
    {
        private World world = null;
        private RegionFile region = null;
        private String lastPath = String.Format("{0}{1}.minecraft{1}saves", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Path.DirectorySeparatorChar);

        private int lastSelectedRegionIndex = -1;

        private const String NEEDTOSAVEMSG = "Biomes for the current region have been modified. Do you want to save your changes?";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            imgRegion.Layers.Add(new BitmapSelector.Layer(512, 512, 0.5f));
            imgRegion.Layers.Add(new BitmapSelector.Layer(512, 512, 1.0f));

            trackPanHorizontal.Maximum = 512 / imgRegion.OffsetStep;
            trackPanVertical.Maximum = 512 / imgRegion.OffsetStep;

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

            String[] versions = { "Minecraft Beta 1.7.3", "Minecraft Beta 1.8.1" };

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
            trackPanHorizontal.Value = 0;
            trackPanVertical.Value = 0;
            lblMagnification.Text = "Magnification: 1x";
            imgRegion.Reset();
            region = null;
            world = null;
        }

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
            dialog.Filter = "(*.dat)|*.dat";
            dialog.RestoreDirectory = false;

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            ResetControls();

            lastPath = dialog.FileName;
            world = new World(lastPath);

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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
            world.RenderRegion(region, imgRegion.Layers[2].Image);
            UpdateStatus("Generating biome map");
            world.RenderRegionBiomes(region, imgRegion.Layers[1].Image, imgRegion.ToolTips);
            UpdateStatus("");
            imgRegion.Redraw();
            lastSelectedRegionIndex = lstRegions.SelectedIndex;
        }

        private void chkShowMap_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.Layers[2].Visable = chkShowMap.Checked;
            imgRegion.Redraw();
        }

        private void chkShowBiomes_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.Layers[1].Visable = chkShowBiomes.Checked;
            imgRegion.Redraw();
        }

        private void chkShowToolTips_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.ShowToolTips = chkShowToolTips.Checked;
        }

        private void chkShowSelection_CheckedChanged(object sender, EventArgs e)
        {
            imgRegion.Layers[0].Visable = chkShowSelection.Checked;
            imgRegion.Redraw();
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
            imgRegion.InvertSelection();
            imgRegion.Redraw();
        }

        private void btnSelectChunks_Click(object sender, EventArgs e)
        {
            UpdateStatus("Expanding selection");
            World.SelectChunks(imgRegion.Layers[0].Image, imgRegion.SelectionColor);
            UpdateStatus("");
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

        private void trackBrushDiameter_Scroll(object sender, EventArgs e)
        {
            lblBrushDiameter.Text = String.Format("Brush Diameter: {0}", trackBrushDiameter.Value);
            imgRegion.BrushDiameter = trackBrushDiameter.Value;
        }

        private void trackMagnification_Scroll(object sender, EventArgs e)
        {
            imgRegion.Zoom(trackMagnification.Value, imgRegion.OffsetX, imgRegion.OffsetY);
            imgRegion.Redraw();
            lblMagnification.Text = String.Format("Magnification: {0}x", trackMagnification.Value);
            trackPanHorizontal.Value = imgRegion.OffsetX / imgRegion.OffsetStep;
            trackPanVertical.Value = imgRegion.OffsetY / imgRegion.OffsetStep;
        }

        private void trackPanHorizontal_Scroll(object sender, EventArgs e)
        {
            imgRegion.Zoom(imgRegion.Magnification, imgRegion.OffsetStep * trackPanHorizontal.Value, imgRegion.OffsetY);
            imgRegion.Redraw();
        }
 
        private void trackPanVertical_Scroll(object sender, EventArgs e)
        {
            imgRegion.Zoom(imgRegion.Magnification, imgRegion.OffsetX, imgRegion.OffsetStep * trackPanVertical.Value);
            imgRegion.Redraw();
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            if (world == null || region == null)
                return;

            UpdateStatus("Filling selected area");
            Biome b;
            if (Enum.TryParse<Biome>((String)cmbFill.SelectedItem, out b))
            {
                world.Fill(region, imgRegion.Layers[0].Image, imgRegion.SelectionColor, b);
            }
            else
            {
                BiomeUtil util = null;
                switch ((String)cmbFill.SelectedItem)
                {
                    case "Minecraft Beta 1.7.3":
                        util = new Minecraft.B17.BiomeGenBase(world.Seed);
                        break;
                    default:
                    case "Minecraft Beta 1.8.1":
                        util = new Minecraft.B18.WorldChunkManager(world.Seed);
                        break;
                }
                world.Fill(region, imgRegion.Layers[0].Image, imgRegion.SelectionColor, util);
            }

            imgRegion.ToolTips = new String[imgRegion.Width, imgRegion.Height];
            UpdateStatus("Generating biome map");
            world.RenderRegionBiomes(region, imgRegion.Layers[1].Image, imgRegion.ToolTips);
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
                world.Replace(region, imgRegion.Layers[0].Image, imgRegion.SelectionColor, (Biome)Enum.Parse(typeof(Biome), (String)cmbReplace1.SelectedItem), b);
            }
            else
            {
                BiomeUtil util = null;
                switch ((String)cmbReplace2.SelectedItem)
                {
                    case "Minecraft Beta 1.7.3":
                        util = new Minecraft.B17.BiomeGenBase(world.Seed);
                        break;
                    default:
                    case "Minecraft Beta 1.8.1":
                        util = new Minecraft.B18.WorldChunkManager(world.Seed);
                        break;
                }
                world.Replace(region, imgRegion.Layers[0].Image, imgRegion.SelectionColor, (Biome)Enum.Parse(typeof(Biome), (String)cmbReplace1.SelectedItem), util);
            }

            imgRegion.ToolTips = new String[imgRegion.Width, imgRegion.Height];
            UpdateStatus("Generating biome map");
            world.RenderRegionBiomes(region, imgRegion.Layers[1].Image, imgRegion.ToolTips);
            UpdateStatus("");
            imgRegion.Redraw();
        }

        private void UpdateStatus(String status)
        {
            lblStatus.Text = status;
            lblStatus.Refresh();
        }
    }

}
