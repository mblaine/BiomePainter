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
        private BiomeUtil biome = null;
        private RegionFile region = null;
        private String lastPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.minecraft\saves";

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
        }

        private void openWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstRegions.Items.Clear();
            trackMagnification.Value = 1;
            trackPanHorizontal.Value = 0;
            trackPanVertical.Value = 0;
            lblMagnification.Text = "Magnification: 1x";
            imgRegion.Reset();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = lastPath;
            dialog.Filter = "(*.dat)|*.dat";
            dialog.RestoreDirectory = false;

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            lastPath = dialog.FileName;
            world = new World(lastPath);

            String[] regions = world.GetRegionPaths();
            foreach(String r in regions)
                lstRegions.Items.Add(RegionFile.ToString(r));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (world == null)
                return;

            Match m = Regex.Match(lstRegions.SelectedItem.ToString(), @"Region (-?\d+), (-?\d+)");
            String path = String.Format("{0}{1}r.{2}.{3}.mca", world.RegionDir, Path.DirectorySeparatorChar, m.Groups[1].Value, m.Groups[2].Value);

            region = new RegionFile(path);
            imgRegion.Reset();
            world.RenderRegion(region, imgRegion.Layers[2].Image);
            world.RenderRegionBiomes(region, imgRegion.Layers[1].Image, imgRegion.ToolTips);
            imgRegion.Redraw();
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
            World.SelectChunks(imgRegion.Layers[0].Image, imgRegion.SelectionColor);
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
    }

}
