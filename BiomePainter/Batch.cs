using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Minecraft;

namespace BiomePainter
{
    public partial class Batch : Form
    {
        private const String PROMPTFILL = "This will fill all regions in the {1} dimension of the world {0} with {2}.";
        private const String PROMPTREPLACE = "This will replace all instances of {2} with {3} in all regions in the {1} dimension of the world {0}.";

        private const String BIOMESTRING = "the biome {0}";
        private const String MCSTRING = "biomes from {0}";

        private String regionDir;
        private bool replace;
        private Object biome1;
        private Object biome2;
        private long worldSeed;

        private delegate void CallbackString(String s);
        private delegate void CallbackIntInt(int i, int m);

        private Thread worker = null;
        private Mutex mutex = null;

        public Batch(String regionDir, String worldName, String dimensionName, bool replace, Object biome1, Object biome2, long worldSeed)
        {
            this.regionDir = regionDir;
            this.replace = replace;
            this.biome1 = biome1;
            this.biome2 = biome2;
            this.worldSeed = worldSeed;

            InitializeComponent();

            int before = lblPrompt3.Bottom;
            lblPrompt.MaximumSize = new Size(this.Width - lblPrompt.Left * 2, 0);
            lblPrompt2.MaximumSize = lblPrompt.MaximumSize;
            lblPrompt3.MaximumSize = lblPrompt.MaximumSize;
            if (!replace)
            {
                String b = String.Format(biome1 is BiomeType ? BIOMESTRING : MCSTRING, biome1.ToString());
                lblPrompt.Text = String.Format(PROMPTFILL, worldName, dimensionName, b);
                btnGo.Text = "Fill All Regions";
            }
            else
            {
                String b1 = String.Format(BIOMESTRING, biome1.ToString());
                String b2 = String.Format(biome2 is BiomeType ? BIOMESTRING : MCSTRING, biome2.ToString());
                lblPrompt.Text = String.Format(PROMPTREPLACE, worldName, dimensionName, b1, b2);
                btnGo.Text = "Replace All Regions";
            }

            lblPrompt2.Top = lblPrompt.Bottom + 10;
            lblPrompt3.Top = lblPrompt2.Bottom + 10;

            this.Height = this.Height + (lblPrompt3.Bottom - before);

            this.CancelButton = btnCancel;

        }

        private void Batch_Activated(object sender, EventArgs e)
        {
            btnCancel.Focus();
        }

        private void Batch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (worker != null && worker.IsAlive)
            {
                DialogResult res = MessageBox.Show(this, "Work hasn't completed yet. Are you sure you want to cancel?", "Biome Painter", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (res == DialogResult.Yes)
                {
                    btnCancel.Enabled = false;
                    mutex.WaitOne();
                    worker.Abort();
                    mutex.ReleaseMutex();
                    mutex.Dispose();
                    return;
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
            else if(mutex != null)
            {
                mutex.WaitOne();
                mutex.ReleaseMutex();
                mutex.Dispose();
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

        private void btnGo_Click(object sender, EventArgs e)
        {
            btnGo.Enabled = false;
            mutex = new Mutex();
            worker = new Thread(new ThreadStart(Run));
            worker.Start();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Run()
        {

            String[] paths = Directory.GetFiles(regionDir, "*.mca", SearchOption.TopDirectoryOnly);
            String format = String.Format("{{0}} region {{1}} of {0}", paths.Length);
            int count = 0;
            foreach (String path in paths)
            {
                UpdateStatus(String.Format(format, "Reading", count));
                UpdateProgress(count, paths.Length);
                RegionFile region = new RegionFile(path);
                UpdateStatus(String.Format(format, replace ? "Replacing" : "Filling", count));
                if (!replace)
                {
                    RegionUtil.Fill(region, null, Color.Black, biome1, worldSeed);
                }
                else
                {
                    RegionUtil.Replace(region, null, Color.Black, ((BiomeType)biome1).ID, biome2, worldSeed);
                }
                UpdateStatus(String.Format(format, "Saving", count));
                mutex.WaitOne();
                region.Write(true);
                mutex.ReleaseMutex();
                count++;
            }
            UpdateProgress(paths.Length, paths.Length);
            UpdateStatus("Done");
        }

        private void UpdateStatus(String s)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackString(UpdateStatus), new Object[] { s });
                return;
            }

            lblStatus.Text = s;
        }

        private void UpdateProgress(int value, int max)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackIntInt(UpdateProgress), new Object[] { value, max });
                return;
            }

            if (prgProgress.Maximum != max)
                prgProgress.Maximum = max;
            prgProgress.Value = value;
        }
    }
}
