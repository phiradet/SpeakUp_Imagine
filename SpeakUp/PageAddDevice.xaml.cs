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
    /// Interaction logic for PageAddDevice.xaml
    /// </summary>
    public partial class PageAddDevice : UserControl
    {
        MainWindow main;
        PageDevice pageDevice;
        PageDevice newPageDevice;
        public PageAddDevice(MainWindow m, PageDevice p)
        {
            InitializeComponent();
            main = m;
            pageDevice = p;
        }

        public void callPageDevice(){
            this.Background = new SolidColorBrush(Colors.White);
            newPageDevice = new PageDevice(main);
            grid_PageAddDevice.Children.Add(newPageDevice);
            newPageDevice.Margin = new Thickness(0, -640, 0, 0);
            newPageDevice.buttonAddDevice.Opacity = 0;

            ThicknessAnimation t = new ThicknessAnimation();
            t.Duration = new Duration(TimeSpan.Parse("0:0:0.5"));
            t.To = new Thickness(0,0,0,0);
            t.EasingFunction = new BackEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            Storyboard.SetTarget(t, newPageDevice);
            Storyboard.SetTargetProperty(t, new PropertyPath(Grid.MarginProperty));

            Storyboard ss = new Storyboard();
            ss.Completed += new EventHandler(ss_Completed);
            ss.Children.Add(t);
            

            ss.Begin();
        }

        void ss_Completed(object sender, EventArgs e)
        {
            DoubleAnimation d = new DoubleAnimation();
            d.Duration = new Duration(TimeSpan.Parse("0:0:0.5"));
            d.To = 1;
            Storyboard.SetTarget(d, newPageDevice.buttonAddDevice);
            Storyboard.SetTargetProperty(d, new PropertyPath(Button.OpacityProperty));
            Storyboard s = new Storyboard();
            s.Children.Add(d);
            s.Begin();
        }

        private void buttonAddDevice_Click(object sender, RoutedEventArgs e)
        {
            saveDeviceData();
        }

        private void saveDeviceData()
        {
            MessageBox.Show("Saved");
            pageDevice.backToPageDevice();
        }
    }
}
