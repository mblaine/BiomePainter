using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace BiomePainter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
            Application.ThreadException += ThreadException;
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception)
                LogException((Exception)e.ExceptionObject);
        }

        static void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            LogException(e.Exception);
        }

        static void LogException(Exception ex)
        {
            try
            {
                DateTime dt = DateTime.Now;
                String assembly = Assembly.GetExecutingAssembly().Location;
                String path = String.Format("{0}{1}exception-{2:yyyy-MM-dd-HH-mm-ss}.txt", Path.GetDirectoryName(assembly), Path.DirectorySeparatorChar, dt);
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    StringBuilder sb = new StringBuilder();    
                    sb.AppendFormat("Biome Painter version {0}, modified {1:M/d/yyy h:mm:ss tt}{2}", FileVersionInfo.GetVersionInfo(assembly).FileVersion, File.GetLastWriteTime(assembly), Environment.NewLine);
                    sb.AppendFormat("Time: {0:M/d/yyy h:mm:ss tt}{1}{1}", dt, Environment.NewLine);

                    Regex pattern = new Regex(@"^(.* in )(.*)(\:line.*)$");
                    String[] lines = ex.ToString().Split('\r', '\n');
                    foreach (String line in lines)
                    {
                        if (line.Length > 0)
                        {
                            //remove full paths from trace
                            Match m = pattern.Match(line);
                            if (m.Success)
                                sb.AppendFormat("{0}{1}{2}{3}", m.Groups[1].Value, Path.GetFileName(m.Groups[2].Value), m.Groups[3].Value, Environment.NewLine);
                            else
                                sb.AppendLine(line);
                        }
                    }
                    
                    sw.Write(sb.ToString());
                    sw.Close();
                }
                
                MessageBox.Show(null, String.Format("Biome Painter has encountered an unexpected error. The log has been saved to \"{0}\". To open the log, click \"Help\". Otherwise click \"OK\" to continue, though it may be best to close and restart the application.", path), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0, path);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error saving error log: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
