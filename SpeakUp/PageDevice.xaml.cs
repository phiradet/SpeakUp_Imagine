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
using System.Diagnostics;
using System.Windows.Markup;
using System.IO;
using System.Xml;

namespace SpeakUp
{
    /// <summary>
    /// Interaction logic for PageDevice.xaml
    /// </summary>
    public partial class PageDevice : UserControl
    {
        PageDeviceInfo pageDeviceInfo;
        public Grid gridBackup;
        public Grid gridBackup2;
        public Grid gridBackupNavBar;
        public MainWindow mainWindow;
        Button backButton;
        int isPageAddDevice;
        PageAddDevice pageAddDevice;

        public PageDevice(MainWindow m)
        {
            InitializeComponent();
            mainWindow = m;
            gridBackup = (Grid)((Grid)((Grid)gridReal.Children[0])).Children[0];

            loadDeviceData();
        }

        void loadDeviceData()
        {
            for (int i = 0; i < 10; i++)
            {
                Grid gridTemp = new Grid();
                gridTemp.ShowGridLines = false;
                gridTemp.Width = 400;
                gridTemp.Height = 640;
                gridTemp.Background = new SolidColorBrush(Colors.White);
                Button b = new Button();
                b.Template = (ControlTemplate)this.Resources["buttonDevice"];
                b.Cursor = Cursors.Hand;
                //ImageSource s = new BitmapImage(new Uri("../../Green-Mobile-Phone.jpg", UriKind.Relative));
                //b.Background = new ImageBrush(s);
                //b.Content = i.ToString();
                b.Click += new RoutedEventHandler(b_Click);
                b.Width = 300;
                b.Height = 300;
                //b.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                //b.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                b.Margin = new Thickness(50, 100, 50, 240);
                gridTemp.Children.Add(b);

                TextBox textBox = new TextBox();
                textBox.Template = (ControlTemplate)this.Resources["TextBoxDevice"];
                textBox.Text = "Device Pizaro ...";
                textBox.FontSize = 20;
                textBox.Foreground = new SolidColorBrush(Colors.White);
                textBox.TextAlignment = TextAlignment.Left;
                textBox.Margin = new Thickness(100, 250, 100, 0);
                textBox.Width = 200;
                textBox.Height = 60;
                gridTemp.Children.Add(textBox);

                deviceShowPanel.Children.Add(gridTemp);
            }
        }


        void b_Click(object sender, RoutedEventArgs e)
        {
            //////// Zoom Animation //////////////
            ssViewer.StopFlick();
            Point point = ((Button)sender).TransformToAncestor(ssViewer).Transform(new Point(0, 0));

            gridZoom.Visibility = Visibility.Visible;
            vb.Visual = ssViewer;
            RectAnimation r = new RectAnimation();

            r.To = new Rect(new Point(point.X - 130, point.Y), new Size(350, 150));
            r.EasingFunction = new BackEase()
            {
                EasingMode = EasingMode.EaseInOut,
                Amplitude = 0.3
            };
            r.Duration = new Duration(TimeSpan.Parse("0:0:0.5"));

            Storyboard.SetTargetName(r, "vb");
            Storyboard.SetTargetProperty(r, new PropertyPath(VisualBrush.ViewboxProperty));
            Storyboard myStoryboard = new Storyboard();
            myStoryboard.Completed += new EventHandler(myStoryboard_Completed);
            myStoryboard.Children.Add(r);
            myStoryboard.Begin(this);

            /////////////////////////////////////////////////
            //////////// Change NavBar Animation////////////////////////
            gridBackupNavBar = (Grid)((Grid)((Grid)mainWindow.navBar.Children[0])).Children[0];
            Debug.WriteLine(gridBackupNavBar.Margin);
            ThicknessAnimation t = new ThicknessAnimation();
            t.To = new Thickness(0, -100, 0, 0);
            t.Duration = new Duration(TimeSpan.Parse("0:0:0.2"));
            t.EasingFunction = new ExponentialEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            Storyboard.SetTarget(t, mainWindow.navBar.Children[0]);
            Storyboard.SetTargetProperty(t, new PropertyPath(Grid.MarginProperty));
            Storyboard s1 = new Storyboard();
            s1.Completed += new EventHandler(s1_Completed);
            s1.Children.Add(t);
            s1.Begin();
            
        }

        void s1_Completed(object sender, EventArgs e)
        {
            Grid g = (Grid)mainWindow.navBar.Children[0];
            g.Children.Clear();

            backButton = new Button();
            backButton.Template = (ControlTemplate)mainWindow.Resources["ButtonNavBar"];
            backButton.FontSize = 26.667;
            backButton.Cursor = Cursors.Hand;
            backButton.Margin = new Thickness(-100, 83, 1200, 0);
            backButton.Width = 100;
            backButton.Height = 50;
            backButton.Foreground = new SolidColorBrush(Colors.White);
            backButton.Content = "Back";
            backButton.Click += new RoutedEventHandler(backButton_Click);
            g.Children.Add(backButton);

            ThicknessAnimation t = new ThicknessAnimation();
            t.To = new Thickness(50, 83, 1200, 0);
            t.Duration = new Duration(TimeSpan.Parse("0:0:0.2"));
            t.EasingFunction = new ExponentialEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            Storyboard.SetTarget(t, backButton);
            Storyboard.SetTargetProperty(t, new PropertyPath(Button.MarginProperty));
            Storyboard s1 = new Storyboard();
            s1.Children.Add(t);
            s1.Begin();
 
        }

        void backButton_Click(object sender, RoutedEventArgs e)
        {
            backToPageDevice();
        }

        public void backToPageDevice()
        {
            ThicknessAnimation t = new ThicknessAnimation();
            t.To = new Thickness(-100, 83, 1200, 0);
            t.Duration = new Duration(TimeSpan.Parse("0:0:0.2"));
            t.EasingFunction = new ExponentialEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            Storyboard.SetTarget(t, backButton);
            Storyboard.SetTargetProperty(t, new PropertyPath(Button.MarginProperty));
            Storyboard s2 = new Storyboard();
            s2.Completed += new EventHandler(s2_Completed);
            s2.Children.Add(t);
            s2.Begin();

            Grid g = (Grid)gridReal.Children[0];

            if (isPageAddDevice == 0)
            {
                g.Children.Clear();
                g.Children.Add(gridBackup);
                Canvas.SetZIndex(gridZoom, 0);
                ((Storyboard)this.Resources["ZoomOut"]).Begin();
            }
            else
            {
                isPageAddDevice = 0;
                pageAddDevice.callPageDevice();
            }
        }

        void s2_Completed(object sender, EventArgs e)
        {
            Grid g = (Grid)((Grid)mainWindow.navBar.Children[0]);
            g.Children.Clear();
            g.Children.Add(gridBackupNavBar);

            ThicknessAnimation t = new ThicknessAnimation();
            t.To = new Thickness(0, 0, 0, 0);
            t.Duration = new Duration(TimeSpan.Parse("0:0:0.2"));
            t.EasingFunction = new ExponentialEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            Storyboard.SetTarget(t, g);
            Storyboard.SetTargetProperty(t, new PropertyPath(Grid.MarginProperty));
            Storyboard s1 = new Storyboard();
            s1.Children.Add(t);
            s1.Begin();
        }

        void myStoryboard_Completed(object sender, EventArgs e)
        {
            Grid g = (Grid)((Grid)gridReal.Children[0]);
            pageDeviceInfo = new PageDeviceInfo(this);
            pageDeviceInfo.vb.Visual = gridZoom;
            g.Children.Clear();
            g.Children.Add(pageDeviceInfo);

            Canvas.SetZIndex(gridZoom, -1);
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {            
            gridZoom.Visibility = Visibility.Hidden;
        }

        private void buttonAddDevice_Click(object sender, RoutedEventArgs e)
        {
            Grid g = (Grid)((Grid)gridReal.Children[0]);
            isPageAddDevice = 1;
            gridBackupNavBar = (Grid)((Grid)((Grid)mainWindow.navBar.Children[0])).Children[0];
            Debug.WriteLine(gridBackupNavBar.Margin);
            ThicknessAnimation t = new ThicknessAnimation();
            t.To = new Thickness(0, -100, 0, 0);
            t.Duration = new Duration(TimeSpan.Parse("0:0:0.2"));
            t.EasingFunction = new ExponentialEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            Storyboard.SetTarget(t, mainWindow.navBar.Children[0]);
            Storyboard.SetTargetProperty(t, new PropertyPath(Grid.MarginProperty));
            Storyboard s1 = new Storyboard();
            s1.Completed += new EventHandler(s1_Completed);
            s1.Children.Add(t);
            s1.Begin();


            pageAddDevice = new PageAddDevice(mainWindow, this);

            g.Children.Add(pageAddDevice);
            pageAddDevice.Margin = new Thickness(0, 640, 0, 0);
            ThicknessAnimation t2 = new ThicknessAnimation();
            t2.Duration = new Duration(TimeSpan.Parse("0:0:0.5"));
            t2.To = new Thickness(0);
            t2.EasingFunction = new BackEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            Storyboard.SetTarget(t2, pageAddDevice);
            Storyboard.SetTargetProperty(t2, new PropertyPath(Grid.MarginProperty));
            Storyboard s2 = new Storyboard();
            s2.Children.Add(t2);
            s2.Begin();
            
        }

       
    }
}
