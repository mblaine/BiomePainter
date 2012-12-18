using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;

namespace BiomePainter
{
    public partial class Update : Form
    {
        private const int Blocks = 0;
        private const int Biomes = 1;
        private const int Program = 2;

        private bool[] shouldBeEnabled;
        private bool[] readyToGo;

        public Update()
        {
            InitializeComponent();
            this.AcceptButton = btnClose;
            this.CancelButton = btnClose;

            shouldBeEnabled = new bool[3];
            shouldBeEnabled[Blocks] = shouldBeEnabled[Biomes] = shouldBeEnabled[Program] = true;
            readyToGo = new bool[3];
            readyToGo[Blocks] = readyToGo[Biomes] = readyToGo[Program] = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        void enableButtons(bool enable)
        {
            btnBlocks.Enabled = enable ? shouldBeEnabled[Blocks] : false;
            btnBiomes.Enabled = enable ? shouldBeEnabled[Biomes] : false;
            btnProgram.Enabled = enable ? shouldBeEnabled[Program] : false;
            btnClose.Enabled = enable;
        }

        private void btnBlocks_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/mblaine/BiomePainter/contents/BiomePainter/Blocks.default.txt");
            request.Method = "GET";
            request.Headers["Accept-Encoding"] = "gzip,deflate";
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader responseStream = new StreamReader(response.GetResponseStream());
            String json = responseStream.ReadToEnd();
            response.Close();
            responseStream.Close();

            json = json.Replace("\\n", "");

            String content = new Regex("[\"']content[\"']: ?\"([^\"']+)[\"']", RegexOptions.IgnoreCase | RegexOptions.Multiline).Match(json).Groups[1].Value;
            content = Encoding.UTF8.GetString(Convert.FromBase64String(content));
            String sha = new Regex("[\"']sha[\"']: ?\"([^\"']+)[\"']", RegexOptions.IgnoreCase | RegexOptions.Multiline).Match(json).Groups[1].Value;
        }

        private void btnBiomes_Click(object sender, EventArgs e)
        {

        }

        private void btnProgram_Click(object sender, EventArgs e)
        {
            if (readyToGo[Program])
            {
                Process.Start("https://github.com/mblaine/BiomePainter/downloads");
                return;
            }

            enableButtons(false);
            shouldBeEnabled[Program] = false;
            btnProgram.Text = "Checking for program updates.";
            btnProgram.Refresh();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/mblaine/BiomePainter/tags");
                request.Method = "GET";
                request.Headers["Accept-Encoding"] = "gzip,deflate";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader responseStream = new StreamReader(response.GetResponseStream());
                String json = responseStream.ReadToEnd();
                response.Close();
                responseStream.Close();

                int latestMajor = -1;
                int latestMinor = -1;
                int latestSubminor = -1;

                MatchCollection matches = new Regex("[\"']name[\"']:\\s*[\"'](\\d+)\\.(\\d+)\\.(\\d+)[\"']", RegexOptions.Multiline).Matches(json);
                foreach (Match m in matches)
                {
                    int tagMajor = Int32.Parse(m.Groups[1].Value);
                    int tagMinor = Int32.Parse(m.Groups[2].Value);
                    int tagSubminor = Int32.Parse(m.Groups[3].Value);

                    if (tagMajor > latestMajor)
                    {
                        latestMajor = tagMajor;
                        latestMinor = tagMinor;
                        latestSubminor = tagSubminor;
                    }
                    else if (tagMajor == latestMajor && tagMinor > latestMinor)
                    {
                        latestMinor = tagMinor;
                        latestSubminor = tagSubminor;
                    }
                    else if (tagMajor == latestMajor && tagMinor == latestMinor && tagSubminor > latestSubminor)
                    {
                        latestSubminor = tagSubminor;
                    }
                }
                latestMajor = -1;
                if (latestMajor == -1)
                {
                    btnProgram.Text = "Unable to determine latest version. Click to open the list of downloads in your web browser.";
                    shouldBeEnabled[Program] = true;
                    readyToGo[Program] = true;
                    enableButtons(true);
                    return;
                }

                Match match = new Regex("^(\\d+)\\.(\\d+)\\.(\\d+)").Match(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);

                int major, minor, subminor;
                if (match.Groups.Count == 4 && Int32.TryParse(match.Groups[1].Value, out major) && Int32.TryParse(match.Groups[2].Value, out minor) && Int32.TryParse(match.Groups[3].Value, out subminor))
                {
                    if (latestMajor > major || (latestMajor == major && latestMinor > minor) || (latestMajor == major && latestMinor == minor && latestSubminor > subminor))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("Biome Painter {0}.{1}", latestMajor, latestMinor);
                        if (latestSubminor != 0)
                            sb.AppendFormat(".{0}", latestSubminor);
                        sb.Append(" now available! Click to open the list of downloads in your web browser.");
                        btnProgram.Text = sb.ToString();
                        shouldBeEnabled[Program] = true;
                        readyToGo[Program] = true;
                        enableButtons(true);
                        return;
                    }
                    else
                    {
                        btnProgram.Text = "You are running the latest version of Biome Painter. No action is necessary.";
                        shouldBeEnabled[Program] = false;
                        readyToGo[Program] = false;
                        enableButtons(true);
                        return;
                    }
                }
                else
                {
                    btnProgram.Text = "Unable to determine version number. Click to open the list of downloads in your web browser.";
                    shouldBeEnabled[Program] = true;
                    readyToGo[Program] = true;
                    enableButtons(true);
                    return;
                }
            }
            catch (Exception)
            {
                btnProgram.Text = "Unable to determine latest version. Click to open the list of downloads in your web browser.";
                shouldBeEnabled[Program] = true;
                readyToGo[Program] = true;
                enableButtons(true);
                return;
            }
        }
    }
}
