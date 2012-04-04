using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;

namespace SpeakUp
{
    /// <summary>
    /// Interaction logic for PageSettings.xaml
    /// </summary>
    public partial class PageSettings : UserControl
    {
        public SoundPlayer soundClickCtrl = new SoundPlayer(System.IO.Path.Combine(Constants.appResourcePath,@"Audio\ClickCtrl.wav"));
        public SoundPlayer soundTextChanged = new SoundPlayer(System.IO.Path.Combine(Constants.appResourcePath, @"Audio\ClickNum.wav")); 
        public bool mouse_state = false;
        public bool male_state = false;
        public bool female_state = false;
        public string testText = "";
        public int speed = 0;
        public int pitch = 0;
        public int vol = 0;
        public int gender = 0; //0=male , 1= female
        public int bps = 0;
        TtsTestVoice tts;
        initialToDo init;
        public PageSettings()
        {
            InitializeComponent();
            Utility.LoadXML4ConfigVoice(); //Load old voice configure
            tts = new TtsTestVoice();
            init = new initialToDo();

            //this.WindowState = WindowState.Maximized;
            
            speed = Utility.configure.speed;
            pitch = Utility.configure.pitch;
            vol = Utility.configure.volume;
            gender = Utility.configure.gender;
            initGender();

            Speed.Value = speed;
            Pitch.Value = pitch;
            Volume.Value = vol;
            
        }

        private void Dial_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouse_state = true;
        }

        private void Dial_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (mouse_state)
            {
                //call call_p = new call();
                //this.Close();
                //mouse_state = false;
                //call_p.Show();
            }
        }

        private void male_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            male_state = true;
        }

        private void female_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            female_state = true;
        }

        private void male_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (male_state)
            {
                SettingData(0);
                male_state = false;
                soundClickCtrl.Play();
            }
        }

        private void female_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (female_state)
            {
                SettingData(1);
                female_state = false;
                soundClickCtrl.Play();
            }
        }

        private void SettingData(int newGender)
        {
            //GENDER
            if (gender == 0 && newGender == 0) // male to male
            {
                ;
            }
            else if (gender == 0 && newGender == 1) // male to female
            {
                rectangle.Margin = new Thickness(168, rectangle.Margin.Top, rectangle.Margin.Right, rectangle.Margin.Bottom);
                Storyboard FtoM = (Storyboard)FindResource("MaleToFemale");
                BeginStoryboard(FtoM);
                gender = 1;
            }
            else if (gender == 1 && newGender == 0) // female to male
            {
                rectangle.Margin = new Thickness(168, rectangle.Margin.Top, rectangle.Margin.Right, rectangle.Margin.Bottom);
                Storyboard FtoM = (Storyboard)FindResource("FemaleToMale");
                BeginStoryboard(FtoM);
                gender = 0;
            }
            else // female to female
            {
                ;
            }
        }

        private void testSound_Click(object sender, RoutedEventArgs e)
        {
            soundClickCtrl.Play();
            tts.GetConfig(speed, pitch, vol, gender, bps);
            
            tts.SubmitText(testText);
        }

        private void TestBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TestBox.Text == "Your text here")
            {
                TestBox.Text = "";
            }
            else
            {
                testText = TestBox.Text;
            }
        }

        private void TestBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            testText = TestBox.Text;
            soundTextChanged.Play();
        }


        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vol = Convert.ToInt32(Volume.Value);
            if (VolumeLabel != null)
            {
                VolumeLabel.Text = vol + "";
            }
        }

        private void Pitch_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            pitch = Convert.ToInt32(Pitch.Value);
            if (PitchLabel != null)
            {
                PitchLabel.Text = pitch + "";
            }
        }

        private void Speed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            speed = Convert.ToInt32(Speed.Value);
            if (SpeedLabel!= null)
            {
                SpeedLabel.Text = speed+"" ;
            }
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Utility.CreateXML4ConfigVoice(speed, pitch, vol, gender, bps);
                //MessageBox.Show("success");
            }
            catch
            {
                MessageBox.Show("error");
            }
            Utility.LoadXML4ConfigVoice();
        }
        private void initGender()
        {
            if (gender == 0)//male
            {
                rectangle.Margin = new Thickness(168, rectangle.Margin.Top, rectangle.Margin.Right, rectangle.Margin.Bottom);
            }
            else//female
            {
                rectangle.Margin = new Thickness(298, rectangle.Margin.Top, rectangle.Margin.Right, rectangle.Margin.Bottom);
            }
        }
    }
}
