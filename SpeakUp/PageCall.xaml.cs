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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;
using System.Media;

namespace SpeakUp
{
    /// <summary>
    /// Interaction logic for PageCall.xaml
    /// </summary>
    public partial class PageCall : UserControl
    {
        public SoundPlayer soundClickNum = new SoundPlayer(@"ClickNum.wav");
        public SoundPlayer soundClickCtrl = new SoundPlayer(@"ClickCtrl.wav");
        public SoundPlayer soundBeep = new SoundPlayer(@"beep.wav");
        public string number = "";
        public bool mouse_state = false;

        //================================================
        //+++++++++ BLUETOOTH CONNECTION +++++++++++++++++
        public BTdevice selectedDevice;
        public BTconnect client;
        List<BTdevice> tmp;
        string errMsg = "";
        public delegate void AddListItemDelegate(String myString);
        public AddListItemDelegate showNotificationDelegate;
        //================================================

        public PageCall()
        {
            InitializeComponent();
        }

        public PageCall(BTdevice selectedDevice, BTconnect BTclient)
        {
            InitializeComponent();
            //this.WindowState = WindowState.Maximized;
            showNotificationDelegate = new AddListItemDelegate(ShowNotification);
            this.client = BTclient;
            this.selectedDevice = selectedDevice;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void _1_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                if (numBox.Text == "Number")
                {
                    numBox.Text = "";
                }
                number += "1";
                numBox.Text = number;
                soundClickNum.Play();
            }
            else
            {
                soundBeep.Play();
            }

            if (selectedDevice.deviceCallState == Constants.ONGOING)
            {
                DTMF("1");
            }
        }

        private void _2_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                if (numBox.Text == "Number")
                {
                    numBox.Text = "";
                }
                number += "2";
                numBox.Text = number;
                soundClickNum.Play();
            }
            else
            {
                soundBeep.Play();
            }

            if (selectedDevice.deviceCallState == Constants.ONGOING)
            {
                DTMF("2");
            }
        }

        private void _3_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                if (numBox.Text == "Number")
                {
                    numBox.Text = "";
                }
                number += "3";
                numBox.Text = number;
                soundClickNum.Play();
            }
            else
            {
                soundBeep.Play();
            }

            if (selectedDevice.deviceCallState == Constants.ONGOING)
            {
                DTMF("3");
            }
        }

        private void _4_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                if (numBox.Text == "Number")
                {
                    numBox.Text = "";
                }
                number += "4";
                numBox.Text = number;
                soundClickNum.Play();
            }
            else
            {
                soundBeep.Play();
            }

            if (selectedDevice.deviceCallState == Constants.ONGOING)
            {
                DTMF("4");
            }
        }

        private void _5_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                if (numBox.Text == "Number")
                {
                    numBox.Text = "";
                }
                number += "5";
                numBox.Text = number;
                soundClickNum.Play();
            }
            else
            {
                soundBeep.Play();
            }

            if (selectedDevice.deviceCallState == Constants.ONGOING)
            {
                DTMF("5");
            }
        }

        private void _6_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                if (numBox.Text == "Number")
                {
                    numBox.Text = "";
                }
                number += "6";
                numBox.Text = number;
                soundClickNum.Play();
            }
            else
            {
                soundBeep.Play();
            }

            if (selectedDevice.deviceCallState == Constants.ONGOING)
            {
                DTMF("6");
            }
        }

        private void _7_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                if (numBox.Text == "Number")
                {
                    numBox.Text = "";
                }
                number += "7";
                numBox.Text = number;
                soundClickNum.Play();
            }
            else
            {
                soundBeep.Play();
            }

            if (selectedDevice.deviceCallState == Constants.ONGOING)
            {
                DTMF("7");
            }
        }

        private void _8_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                if (numBox.Text == "Number")
                {
                    numBox.Text = "";
                }
                number += "8";
                numBox.Text = number;
                soundClickNum.Play();
            }
            else
            {
                soundBeep.Play();
            }

            if (selectedDevice.deviceCallState == Constants.ONGOING)
            {
                DTMF("8");
            }
        }

        private void _9_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                if (numBox.Text == "Number")
                {
                    numBox.Text = "";
                }
                number += "9";
                numBox.Text = number;
                soundClickNum.Play();
            }
            else
            {
                soundBeep.Play();
            }

            if (selectedDevice.deviceCallState == Constants.ONGOING)
            {
                DTMF("9");
            }
        }

        private void _0_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                if (numBox.Text == "Number")
                {
                    numBox.Text = "";
                }
                number += "0";
                numBox.Text = number;
                soundClickNum.Play();
            }
            else
            {
                soundBeep.Play();
            }

            if (selectedDevice.deviceCallState == Constants.ONGOING)
            {
                DTMF("0");
            }
        }

        private void _star_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                if (numBox.Text == "Number")
                {
                    numBox.Text = "";
                }
                number += "*";
                numBox.Text = number;
                soundClickNum.Play();
            }
            else
            {
                soundBeep.Play();
            }

            if (selectedDevice.deviceCallState == Constants.ONGOING)
            {
                DTMF("*");
            }
        }

        private void _sharp_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                if (numBox.Text == "Number")
                {
                    numBox.Text = "";
                }
                number += "#";
                numBox.Text = number;
                soundClickNum.Play();
            }
            else
            {
                soundBeep.Play();
            }

            if (selectedDevice.deviceCallState == Constants.ONGOING)
            {
                DTMF("#");
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (numBox.Text == "Number")
            {
                numBox.Text = "";
            }
            else
            {
                string temp = "";
                if (numBox.Text.Length == 0)
                {
                    soundBeep.Play();
                }
                else
                {
                    for (int i = 0; i < numBox.Text.Length - 1; i++)
                    {
                        temp += numBox.Text[i];
                    }
                    numBox.Text = temp;
                    number = temp;
                    soundClickCtrl.Play();
                }
            }
        }

        private void numBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (numBox.Text == "Number")
            {
                numBox.Text = "";
            }
        }

        private void numBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            number = numBox.Text;
        }

        private void Setting_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouse_state = true;
        }

        private void Setting_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (mouse_state)
            {
                //Setting setting_page = new Setting();
                //this.Close();
                //setting_page.Show();
            }
        }

        private void Call_Click(object sender, RoutedEventArgs e)
        {
            if (!client.DialNumber(numBox.Text, ref errMsg))
                MessageBox.Show("Error");
            else
                MessageBox.Show("Finish");
        }

        private void Redial_Click(object sender, RoutedEventArgs e)
        {
            if (!client.RedialPhone(ref errMsg))
                MessageBox.Show("Error");
            else
                MessageBox.Show("Finish");
        }

        private void EndCall_Click(object sender, RoutedEventArgs e)
        {
            if (!client.RejectCall(ref errMsg))
                MessageBox.Show("Error");
            else
                MessageBox.Show("Finish");
        }

        #region BLUETOOTH CONNECTION
        void ShowNotification(String rawInput)
        {
            string[] splInput = rawInput.Split('#');
            string data = splInput[0];
            //data = data.Trim();
            notifyLabel.Content = data;
            if (data == Constants.CallIncomming || data == Constants.CallOnGoing || data == Constants.CallOutGoing || data == Constants.CallTerminated || data == Constants.CallSCOrelease)
                selectedDevice.UpdatePhoneState(rawInput);
            notifyLabel.Content += "Status:" + selectedDevice.deviceCallState;
        }

        public void InitializeServerThread()
        {
            UDPserver serverThread = new UDPserver(this);
            Thread oThread = new Thread(new ThreadStart(serverThread.Run));
            oThread.IsBackground = true;
            try
            {
                oThread.Start();
            }
            catch (Exception e)
            {
                Debug.WriteLine("thread alreadey exist!");
            }
        }

        public void InitHFP()
        {
            if (!this.client.ConnectHFP(this.selectedDevice.deviceAddr, ref errMsg))
                MessageBox.Show(errMsg);
            else
                MessageBox.Show("Finish");
        }

        void TerminateHFP()
        {
            if (!client.TerminateHFP(ref errMsg))
                MessageBox.Show(errMsg);
            else
                MessageBox.Show("Finish");
        }

        void DTMF(string inputKey)
        {
            if (!client.DTMF(inputKey, ref errMsg))
                MessageBox.Show(errMsg);
            else
                MessageBox.Show("Finish");
        }
        #endregion
    }
}
