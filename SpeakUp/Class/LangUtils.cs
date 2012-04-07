using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SpeakUp
{
    class LangUtils
    {
        const double LOWEST_UNI_TH = -13.6464100171;
        public static string[] THWordCut(string input)
        {
            if (input.Trim().Length == 0)
                return null;
            string dataInPath = Path.Combine(Constants.appResourcePath, @"swath\data_in");
            string dataOutPath = Path.Combine(Constants.appResourcePath, @"swath\data_out");
            string swathPath = Path.Combine(Constants.appResourcePath, "swath");

            TextWriter tw = new StreamWriter(dataInPath, false, Encoding.GetEncoding("TIS-620"));
            tw.WriteLine(input.Trim());
            tw.Close();

            Utility.ExecuteCommand(String.Format("cd {0}&&swath.exe <data_in >data_out", swathPath));
            string output = "";
            string[] s = System.IO.File.ReadAllLines(dataOutPath, Encoding.GetEncoding("TIS-620"));
            foreach (string tmp in s)
                output += tmp;
            return output.Trim().Split('|');
        }

        public static string Transliterate(string input)
        {
            string cygwinRunExePath = @"c:\cygwin\bin\run.exe";
            string bashFilDirecoty = Path.Combine(Constants.appResourcePath, @"Resource");
            string dataInPath = Path.Combine(Constants.appResourcePath, @"Resource\translit.in");
            string dataOutPath = Path.Combine(Constants.appResourcePath, @"Resource\translit.out");

            if (input.Trim().Length == 0)
                return null;

            TextWriter tw = new StreamWriter(dataInPath, false, Encoding.GetEncoding("TIS-620"));
            tw.WriteLine(input.Trim());
            tw.Close();

            string command = string.Format("cd {0} && {1} -p /usr/bin sh.exe runTransliterateBash.sh", bashFilDirecoty, cygwinRunExePath);

            Utility.ExecuteCommand(command);

            string output = "";
            string[] s = System.IO.File.ReadAllLines(dataOutPath, Encoding.GetEncoding("TIS-620"));
            foreach (string tmp in s)
                output += tmp;
            return output.Trim();
        }

        public static int LanguageDetect(string input)
        {
            //Chinses variable
            const char CHfirstToken = '\u4E00';
            const char CHlastToken = '\u9FFF';

            //Thai variable
            const char THfirstToken = '\u0E00';
            const char THlastToken = '\u0E7F';

            foreach (char currIn in input)
            {
                if (currIn >= CHfirstToken && currIn <= CHlastToken)
                    return Constants._chiLanguage;
                if (currIn >= THfirstToken && currIn <= THlastToken)
                    return Constants._thaLanguage;
            }
            return Constants._engLanguage;
        }

        public static string[] ENTWordCut(string input)
        {
            input = input.Trim();
            if(input[input.Length-1]=='.')
                input = input.Substring(0,input.Length-1);
            return input.Split(' ');
        }
    }
}
