using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeechLib;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Media;
using System.IO;

namespace SpeakUp
{
    class TtsTestVoice
    {
        public static SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
        public static SpVoice Voice = new SpVoice();
        public static string strVoice = "";

        public SpFileStream fileStream;

        int _gender = Constants.MALE;
        int _volume; int _beat;
        int _speed; int _pitch;

        private System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer();

        public TtsTestVoice()
        { 
        }

        public void GetConfig(int speed, int pitch, int volume, int gender, int beat)
        {
            _speed = speed;
            _pitch = pitch;
            _volume = volume;
            _gender = gender;
            
            _beat = beat;
            
        }
        public void EnglishTTS(string inputString)
        {
            try
            {
                Voice.Rate = _speed;
                Voice.Volume = (int)(_volume * 10);
                if (_gender == Constants.MALE) //Need to increase speed of Male because of low sensitive
                {
                    Voice.Voice = Voice.GetVoices("", "").Item(initialToDo.MicrosoftTTSModelINdex[@"Microsoft David Desktop - English (United States)"]);

                }
                else if ((_gender == Constants.FEMALE))
                {
                    Voice.Voice = Voice.GetVoices("", "").Item(initialToDo.MicrosoftTTSModelINdex[@"Microsoft Hazel Desktop - English (Great Britain)"]);
                }

                //if (_pitch != 0)
                {
                    /// text to speech to wav file
                    fileStream = new SpFileStream();
                    fileStream.Open(@"./waveOut/engVoice.wav", SpeechStreamFileMode.SSFMCreateForWrite, false);
                    Voice.AudioOutputStream = fileStream;
                    Voice.Speak(inputString, SpeechVoiceSpeakFlags.SVSFDefault);
                    fileStream.Close();
                    fileStream = null;
                    
                    ///wav file to soundstread
                    ///

                    Utility.ExecuteCommand(@"soundstretch.exe ./waveOut/engVoice.wav ./waveOut/engVoiceFinal.wav -pitch=" + _pitch);

                    myPlayer.SoundLocation = @"./waveOut/engVoiceFinal.wav";
                    myPlayer.Play();
                }
                //else
                //{
                //    Voice.Speak(inputString, SpFlags);
                //}

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + ex.StackTrace.ToString());
            }
        }
        public void ThaiTTS(string inputString)
        {
            Utility.WriteTextFile(inputString);
            ///
            /// how to use
            /// jRaja_cmd.exe -vf thaitext.txt out.wav 0 80
            ///
            try
            {
                Utility.ExecuteCommand(PathAll._jRajaPath+" -vf thaitext.txt out_female.wav "+_speed+" 80");

                if (_gender == Constants.MALE)
                {
                    if (_pitch < -3)
                    {
                        _pitch = -8;
                    }
                    else if (_pitch > 3)
                    {
                        _pitch = -3;
                    }
                    else
                    {
                        _pitch = -5;
                    }
                    Utility.ExecuteCommand(@"soundstretch.exe C:\ProgramFiles\jRajaHTS\out_female.wav ./waveOut/out_male.wav -pitch="+_pitch);
                    string outMale = @"./waveOut/out_male.wav";
                    bool temp = PlayLouder(outMale,_volume);
                    
                    //myPlayer.SoundLocation = @"./waveOut/out_male.wav";
                    //myPlayer.Play();
                }
                else if (_gender == Constants.FEMALE)
                {
                    if (_pitch < -3)
                    {
                        _pitch = -2;
                    }
                    else if ((_pitch > 3) && (_pitch < 7))
                    {
                        _pitch = 3;
                    }
                    else if (_pitch >= 7)
                    {
                        _pitch = 5;
                    }
                    else
                    {
                        _pitch = 0;
                    }
                    Utility.ExecuteCommand(@"soundstretch.exe C:\ProgramFiles\jRajaHTS\out_female.wav ./waveOut/out_female.wav -pitch=" + _pitch);
                    string outFeMale = @"./waveOut/out_female.wav";
                    bool temp = PlayLouder(outFeMale, _volume);


                    //Player sound
                    //myPlayer.SoundLocation = @"C:\ProgramFiles\jRajaHTS\out_female.wav";
                    //myPlayer.Play();

                }
            }
            catch
            {
                MessageBox.Show("jRaja is out of order");
            }
        }

        public void SubmitText(string inputBoxString)
        {
            string inputString = inputBoxString.Trim();
            foreach (char x in inputString)
            {
                if (!(Utility.isEngAlphabet(x)) && !(Utility.isEngSymbol(x)))
                {
                    ThaiTTS(inputString);
                    return;
                }
            }
            EnglishTTS(inputString);
        }
        public bool PlayLouder(string tmpFileName,int soxVol)
        {
            string finalFileName = "myAmplifiedFile.wav";
            string soxEXE = @"sox.exe";
            int tempSoxVol = (int)(soxVol / 2);
            string soxArgs = "-v " + tempSoxVol + " ";

            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo = new System.Diagnostics.ProcessStartInfo();
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = soxEXE;
                process.StartInfo.Arguments = string.Format("{0} {1} {2}",
                                         soxArgs, tmpFileName, finalFileName);
                process.Start();
                process.WaitForExit();
                int exitCode = process.ExitCode;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return false;
            }
            /*-------------------------------------------------------------
             * play it using SoundPlayer
             *-------------------------------------------------------------*/
            try
            {
                SoundPlayer simpleSound = new SoundPlayer(@finalFileName);
                simpleSound.PlaySync();
                FileInfo readFile = new FileInfo(finalFileName);
                string finalDestination = "./" + readFile.Name;
                readFile.MoveTo(finalDestination);
            }
            catch (Exception e)
            {
                string errmsg = e.Message;
                return false;
            }
            finalFileName = "";
            tmpFileName = "";

            return true;
        }
    }
}
