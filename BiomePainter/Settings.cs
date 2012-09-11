using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace BiomePainter
{
    public struct RecentWorld
    {
        public String Path;
        public String Name;
    }

    public class Settings
    {
        private static bool read = false;

        private static String path = String.Format("{0}{1}settings.xml", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Path.DirectorySeparatorChar);

        private const int MAXWORLDS = 10;

        private static List<RecentWorld> recentWorlds = new List<RecentWorld>(MAXWORLDS);
        public static List<RecentWorld> RecentWorlds
        {
            get { if (!read) Read(); return Settings.recentWorlds; }
        }

        private static bool redrawTerrainMap = false;
        public static bool RedrawTerrainMap
        {
            get { if (!read) Read(); return Settings.redrawTerrainMap; }
            set { if (!read) Read(); Settings.redrawTerrainMap = value; }
        }

        private static bool transparency = true;

        public static bool Transparency
        {
            get { if (!read) Read(); return Settings.transparency; }
            set { if (!read) Read(); Settings.transparency = value; }
        }

        private static bool biomeFoliage = false;
        public static bool BiomeFoliage
        {
            get { if (!read) Read(); return Settings.biomeFoliage; }
            set { if (!read) Read(); Settings.biomeFoliage = value; }
        }

        private Settings()
        {
        }

        private static void Read()
        {
            if (File.Exists(path))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlNodeList worlds = doc.GetElementsByTagName("world");
                for (int i = 0; i < MAXWORLDS && i < worlds.Count; i++)
                {
                    recentWorlds.Add(new RecentWorld { Path = worlds[i].Attributes["path"].Value, Name = worlds[i].Attributes["name"].Value });
                }

                redrawTerrainMap = bool.Parse(doc.SelectSingleNode("/settings/redraw/@value").InnerText);
                transparency = bool.Parse(doc.SelectSingleNode("/settings/transparency/@value").InnerText);
                biomeFoliage = bool.Parse(doc.SelectSingleNode("/settings/biomeFoliage/@value").InnerText);
            }
            read = true;
        }

        public static void Save()
        {
            if(!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, null));
            XmlNode settings = doc.CreateElement("settings");
            XmlNode node = doc.CreateElement("worlds");
            XmlAttribute attr;
            
            for (int i = 0; i < recentWorlds.Count; i++)
            {
                XmlNode world = doc.CreateElement("world");
                attr = doc.CreateAttribute("path");
                attr.Value = recentWorlds[i].Path;
                world.Attributes.Append(attr);

                attr = doc.CreateAttribute("name");
                attr.Value = recentWorlds[i].Name;
                world.Attributes.Append(attr);
                node.AppendChild(world);
            }

            settings.AppendChild(node);

            node = doc.CreateElement("redraw");
            attr = doc.CreateAttribute("value");
            attr.Value = redrawTerrainMap.ToString().ToLower();
            node.Attributes.Append(attr);
            settings.AppendChild(node);

            node = doc.CreateElement("transparency");
            attr = doc.CreateAttribute("value");
            attr.Value = transparency.ToString().ToLower();
            node.Attributes.Append(attr);
            settings.AppendChild(node);

            node = doc.CreateElement("biomeFoliage");
            attr = doc.CreateAttribute("value");
            attr.Value = biomeFoliage.ToString().ToLower();
            node.Attributes.Append(attr);
            settings.AppendChild(node);

            doc.AppendChild(settings);
            doc.Save(path);
        }

        public static void ClearRecentWorlds()
        {
            recentWorlds = new List<RecentWorld>(MAXWORLDS);
        }

        public static void AddRecentWorld(String path, String name)
        {
            recentWorlds.Insert(0, new RecentWorld { Path = path, Name = name });

            for (int i = recentWorlds.Count - 1; i > 0; i--)
            {
                if (recentWorlds[i].Path == path)
                    recentWorlds.RemoveAt(i);
            }

            if (recentWorlds.Count > MAXWORLDS)
                recentWorlds.RemoveRange(MAXWORLDS, recentWorlds.Count - MAXWORLDS);
        }

        public static void RemoveRecentWorld(String path)
        {
            for (int i = recentWorlds.Count - 1; i > 0; i--)
            {
                if (recentWorlds[i].Path == path)
                    recentWorlds.RemoveAt(i);
            }
        }
    }
}
