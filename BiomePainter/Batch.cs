using System;
using System.Drawing;
using System.Windows.Forms;

namespace BiomePainter
{
    public partial class Batch : Form
    {
        private const String PROMPTFILL = "This will fill all regions in the {1} dimension of the world {0} with {2}.";
        private const String PROMPTREPLACE = "This will replace all instances of {2} with {3} in all regions in the {1} dimension of the world {0}.";

        private const String BIOMESTRING = "the biome {0}";
        private const String MCSTRING = "biomes from {0}";

        public Batch(String regionDir, String worldName, String dimensionName, bool replace, Object biome1, Object biome2, long worldSeed)
        {
            InitializeComponent();
            lblPrompt.MaximumSize = new Size(this.Width - lblPrompt.Left * 2, 0);
            if (!replace)
            {
                String b = String.Format(biome1 is BiomeType ? BIOMESTRING : MCSTRING, biome1.ToString());
                lblPrompt.Text = String.Format(PROMPTFILL, worldName, dimensionName, b);
            }
            else
            {
                String b1 = String.Format(BIOMESTRING, biome1.ToString());
                String b2 = String.Format(biome2 is BiomeType ? BIOMESTRING : MCSTRING, biome2.ToString());
                lblPrompt.Text = String.Format(PROMPTREPLACE, worldName, dimensionName, b1, b2);
            }
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
    }
}
