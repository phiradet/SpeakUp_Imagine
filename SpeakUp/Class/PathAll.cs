using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakUp
{
    class PathAll
    {
         private PathAll()
         { 
         }

        public const string _configPath = @"./Data_xml/config.xml";
        public const string _predialogPath = @"./Predialog/Predialog.wav";
        public const string _thaiTextTTsPath = @"C:\ProgramFiles\jRajaHTS\thaitext.txt";
        public const string _jRajaPath = @"C:\ProgramFiles\jRajaHTS\jRaja_cmd.exe";
    }
}
