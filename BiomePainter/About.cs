using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BiomePainter
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, System.EventArgs e)
        {
            lblAbout.Text = String.Format("Biome Painter version {0}", FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);

            List<String> lines = new List<String>();
            try
            {
                using(StreamReader readMe = new StreamReader("README.txt"))
                {
                    bool prevLineBlank = false;
                    while (!readMe.EndOfStream)
                    {
                        String l = readMe.ReadLine().Trim();

                        if (l.CompareTo("Biome Painter") == 0)
                        {
                            prevLineBlank = true;
                        }
                        else if (l.Length == 0)
                        {
                            if (!prevLineBlank)
                            {
                                lines.Add(l);
                                prevLineBlank = true;
                            }
                        }
                        else
                        {
                            lines.Add(l);
                            prevLineBlank = false;
                        }
                    }
                    readMe.Close();
                }
            }
            catch(FileNotFoundException)
            {
                txtReadMe.Text = "README.txt not found. Sorry about that.";
                return;
            }

            for(int i = 0; i < lines.Count; i++)
            {
                if (Regex.IsMatch(lines[i], "<+[^>]+>+"))
                {
                    lines[i] = String.Format(@"\b{0}\b0\par", Regex.Match(lines[i], "<+([^>]+)>+").Groups[1].Value);
                }
                else if (Regex.IsMatch(lines[i], @"^\*"))
                {
                    lines[i] = String.Format(@"{{\*\pn\pnlvlblt\pnf1\pnindent0{{\pntxtb\'B7}}}} {0}\par\pard", Regex.Match(lines[i], @"^\*(.*)$").Groups[1].Value);
                }
                else
                {
                    lines[i] = String.Format(@"{0}\par", lines[i]);
                }
            }
            
            String s = String.Format(@"{{\rtf1{0}}}", String.Join("\r\n", lines));
            txtReadMe.Rtf = s;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnViewLicense_Click(object sender, EventArgs e)
        {
            if(File.Exists("LICENSE.txt"))
                Process.Start("LICENSE.txt");
        }
    }
}
