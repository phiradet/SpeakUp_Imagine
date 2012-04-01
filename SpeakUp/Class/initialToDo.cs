using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeechLib;

namespace SpeakUp
{
    class initialToDo
    {
        /// <summary>
        /// for text to speech
        public static Dictionary<string, int> MicrosoftTTSModelINdex;
        public static SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
        public static SpVoice Voice = new SpVoice();
        public static string strVoice = "";
        public int TTSindex = -1;
        /// </summary>
        /// 

        public initialToDo()
        {
            ///for text to speech initial
            ///
            MicrosoftTTSModelINdex = new Dictionary<string, int>();
            foreach (SpeechLib.ISpeechObjectToken sot in Voice.GetVoices("", ""))
            {
                TTSindex++;
                strVoice = sot.GetDescription(0);     //'The token's name
                MicrosoftTTSModelINdex.Add(strVoice, TTSindex);
            }

        }
    }
}
