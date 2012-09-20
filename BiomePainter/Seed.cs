using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Minecraft;

namespace BiomePainter
{
    public partial class Seed : Form
    {
        private World world;

        public Seed(World world)
        {
            this.world = world;
            InitializeComponent();

            lblOriginalSeed.Text = world.OriginalSeed.ToString();
            txtNewSeed.Text = world.Seed.ToString();
            this.CancelButton = btnCancel;
        }

        private void Seed_Activated(object sender, EventArgs e)
        {
            txtNewSeed.Focus();
            txtNewSeed.SelectAll();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private int hash(String s)
        {
            int ret = 0;
            for (int i = 0; i < s.Length; i++)
                ret = ret * 31 + s[i];
            return ret;
        }

        private void btnTemporary_Click(object sender, EventArgs e)
        {
            if (!long.TryParse(txtNewSeed.Text, out world.Seed))
                world.Seed = hash(txtNewSeed.Text);
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!long.TryParse(txtNewSeed.Text, out world.Seed))
                world.Seed = hash(txtNewSeed.Text);
            world.Write();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
