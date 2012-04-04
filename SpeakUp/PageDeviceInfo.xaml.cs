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
using System.Windows.Media.Animation;

namespace SpeakUp
{
    /// <summary>
    /// Interaction logic for PageDeviceInfo.xaml
    /// </summary>
    public partial class PageDeviceInfo : UserControl
    {
        PageDevice pageDevice;
        public PageDeviceInfo(PageDevice d,BTdevice selectedDevice)
        {
            InitializeComponent();
            pageDevice = d;

            this.deviceNameTxt.Text = selectedDevice.deviceName;
            this.macAddrTxt.Text = selectedDevice.deviceAddr;
            this.deviceClassTxt.Text = selectedDevice.deviceClass;
            /*ThicknessAnimation t = new ThicknessAnimation();
            t.From = new Thickness(-100, 163, 627, 0);
            t.To = new Thickness(180, 163, 627, 0);
            t.Duration = new Duration(TimeSpan.Parse("0:0:0.2"));
            t.EasingFunction = new BackEase()
            {
                EasingMode = EasingMode.EaseOut
            };

            ThicknessAnimation t2 = new ThicknessAnimation();
            t2.From = new Thickness(-100, 228, 627, 0);
            t2.To = new Thickness(180, 228, 627, 0);
            t2.Duration = new Duration(TimeSpan.Parse("0:0:0.2"));
            t2.EasingFunction = new BackEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            Storyboard.SetTarget(t, textBlockDeviceName);
            Storyboard.SetTargetProperty(t, new PropertyPath(TextBox.MarginProperty));
            Storyboard.SetTarget(t2, textBlockMacAddress);
            Storyboard.SetTargetProperty(t2, new PropertyPath(TextBox.MarginProperty));
            Storyboard s = new Storyboard();
            s.Children.Add(t);
            s.Children.Add(t2);
            s.Begin();*/
        }


    }
}
