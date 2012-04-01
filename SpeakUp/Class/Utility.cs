using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Collections;
using System.Xml;
using System.IO;//for write file

namespace SpeakUp
{
    class Utility
    {
        public static Configure configure;

        public static bool isEngAlphabet(char i)
        {
            return (i >= 'a' && i <= 'z') || (i >= 'A' && i <= 'Z') || (i >= '0' && i <= '9') || (i == '_' || i == '/' || i == '\\' || i == '-');
        }
        public static bool isEngSymbol(char i)
        {
            return Char.IsSymbol(i) || Char.IsPunctuation(i) || Char.IsWhiteSpace(i) || Char.IsNumber(i);
        }

        public static int ExecuteCommand(string Command)
        {
            int exitCode;
            ProcessStartInfo ProcessInfo = new ProcessStartInfo("cmd.exe", "/C " + Command);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;
            Process Process = Process.Start(ProcessInfo);
            Process.WaitForExit();
            exitCode = Process.ExitCode;
            Process.Close();
            return exitCode;
        }
        public class Configure
        {
            public Int32 speed { get; set; }
            public Int32 pitch { get; set; }
            public Int32 volume { get; set; }
            public Int32 gender { get; set; }
            public Int32 bps { get; set; }
        }
        public static void CreateXML4ConfigVoice(int speed, int pitch, int volume, int gender, int bps)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode xmlRoot, xmlNode, xmlsubRoot;

            xmlRoot = xmldoc.CreateElement("Configure");
            xmldoc.AppendChild(xmlRoot);

            xmlsubRoot = xmldoc.CreateElement("Config");
            xmlRoot.AppendChild(xmlsubRoot);

            xmlNode = xmldoc.CreateElement("Speed");
            xmlsubRoot.AppendChild(xmlNode);
            xmlNode.InnerText = speed.ToString();

            xmlNode = xmldoc.CreateElement("Pitch");
            xmlsubRoot.AppendChild(xmlNode);
            xmlNode.InnerText = pitch.ToString();

            xmlNode = xmldoc.CreateElement("Volume");
            xmlsubRoot.AppendChild(xmlNode);
            xmlNode.InnerText = volume.ToString();

            xmlNode = xmldoc.CreateElement("Gender");
            xmlsubRoot.AppendChild(xmlNode);
            xmlNode.InnerText = gender.ToString();

            xmlNode = xmldoc.CreateElement("Beats-Per-Second");
            xmlsubRoot.AppendChild(xmlNode);
            xmlNode.InnerText = bps.ToString();

            xmldoc.Save(PathAll._configPath);

        }
        public static void LoadXML4ConfigVoice()
        {
            configure = new Configure();

            XmlDocument xmldoc = new XmlDocument();
            string st = PathAll._configPath;

            xmldoc.Load(st);
            XmlNode node;
            node = xmldoc.DocumentElement;
            foreach (XmlNode n in node.ChildNodes)
            {
                if (n.Name == "Config")
                {
                    configure.speed = Convert.ToInt32(n.ChildNodes[0].InnerText);
                    configure.pitch = Convert.ToInt32(n.ChildNodes[1].InnerText);
                    configure.volume = Convert.ToInt32(n.ChildNodes[2].InnerText);
                    configure.gender = Convert.ToInt32(n.ChildNodes[3].InnerText);
                    configure.bps = Convert.ToInt32(n.ChildNodes[4].InnerText);
                }

            }
        }
        public static void WriteTextFile(String inputStringFromTextBox)
        {
            Stream stream = new FileStream(PathAll._thaiTextTTsPath, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8);
            inputStringFromTextBox = inputStringFromTextBox.Trim();
            streamWriter.Write(inputStringFromTextBox);
            streamWriter.Close();
            stream.Close();
        }
    }

}
