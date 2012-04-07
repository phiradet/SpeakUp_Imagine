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

namespace SpeakUp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PageDevice pageDevice;
        private PageCall pageCall;
        private PageChat pageChat;
        private PageSettings pageSettings;

        #region BT connection
        //+++++++++++++++++++++++++++++++++++++++++++
        //++++++++++ BLUETOOTH CONNECTION +++++++++++
        public BTconnect client;
        public BTdevice selectedDevice;
        List<BTdevice> tmp;
        string errMsg = "";
        public delegate void showNotificationDelegate(String myString);
        public showNotificationDelegate showNotificationDelegateObj;
        public Label mNotificationLabel;
        //+++++++++++++++++++++++++++++++++++++++++++
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            //++++++ INITIAL BLUETOOTH CONNCTION +++++
            this.client = new BTconnect();
            mNotificationLabel = this.notificationLabel;
            //++++++++++++++++++++++++++++++++++++++++

            pageDevice = new PageDevice(this);
            pageDevice.Margin = new Thickness(0,0,0,0);
            showPage_grid.Children.Add(pageDevice);

            pageCall = new PageCall();
            pageCall.Margin = new Thickness(1367 +1, 0, 0, 0);
            showPage_grid.Children.Add(pageCall);

            pageChat = new PageChat();
            pageChat.Margin = new Thickness(1366 * 2 + 1, 0, 0, 0);
            pageChat.Width = 1366;
            showPage_grid.Children.Add(pageChat);

            //pageSettings = new PageSettings();
            //pageSettings.Margin = new Thickness(1366 * 3 + 1, 0, 0, 0);
            //pageSettings.Width = 1366;
            //showPage_grid.Children.Add(pageChat);

            ssViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;

            
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ssViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (ssViewer.IsMouseCaptured == false)
                ssViewer.StopFlick();

        }

        private void ssViewer_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ssViewer.IsMouseCaptured == false)
            {
                if (ssViewer.HorizontalOffset <= (1366.0 / 2))
                {
                    DoubleAnimation r = new DoubleAnimation();
                    r.From = ssViewer.HorizontalOffset / (1366.0 * 2);
                    r.To = 0;
                    r.Duration = TimeSpan.Parse("0:0:0.2");
                    r.EasingFunction = new ExponentialEase()
                    {
                        EasingMode = EasingMode.EaseInOut
                    };
                    Storyboard.SetTargetName(r, "Mediator");
                    Storyboard.SetTargetProperty(r, new PropertyPath("ScrollableWidthMultiplier"));
                    Storyboard myStoryboard = new Storyboard();
                    myStoryboard.Completed += new EventHandler(myStoryboard_Completed);
                    myStoryboard.Children.Add(r);
                    myStoryboard.Begin(this);
                    
                }
                else if (ssViewer.HorizontalOffset > (1366.0 / 2) && ssViewer.HorizontalOffset <= (1366.0 + (1366.0 / 2)))
                {
                    DoubleAnimation r = new DoubleAnimation();
                    r.From = ssViewer.HorizontalOffset / (1366.0 * 2);
                    r.To = 0.5;
                    r.Duration = TimeSpan.Parse("0:0:0.2");
                    r.EasingFunction = new ExponentialEase()
                    {
                        EasingMode = EasingMode.EaseInOut
                    };
                    Storyboard.SetTargetName(r, "Mediator");
                    Storyboard.SetTargetProperty(r, new PropertyPath("ScrollableWidthMultiplier"));
                    Storyboard myStoryboard = new Storyboard();
                    myStoryboard.Children.Add(r);
                    myStoryboard.Begin(this);
                    ssViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                }
                else if (ssViewer.HorizontalOffset > (1366.0 + (1366.0 / 2)))
                {
                    DoubleAnimation r = new DoubleAnimation();
                    r.From = ssViewer.HorizontalOffset / (1366.2 * 2);
                    r.To = 1;
                    r.Duration = TimeSpan.Parse("0:0:0.2");
                    r.EasingFunction = new ExponentialEase()
                    {
                        EasingMode = EasingMode.EaseInOut
                    };
                    Storyboard.SetTargetName(r, "Mediator");
                    Storyboard.SetTargetProperty(r, new PropertyPath("ScrollableWidthMultiplier"));
                    Storyboard myStoryboard = new Storyboard();
                    myStoryboard.Children.Add(r);
                    myStoryboard.Begin(this);
                    ssViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                }

            }
        }

        void myStoryboard_Completed(object sender, EventArgs e)
        {
            ssViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
        }

        private void buttonDevice_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation r = new DoubleAnimation();
            r.To = 0;
            r.Duration = TimeSpan.Parse("0:0:0.2");
            r.EasingFunction = new ExponentialEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            Storyboard.SetTargetName(r, "Mediator");
            Storyboard.SetTargetProperty(r, new PropertyPath("ScrollableWidthMultiplier"));
            Storyboard myStoryboard = new Storyboard();
            myStoryboard.Completed += new EventHandler(myStoryboard_Completed);
            myStoryboard.Children.Add(r);
            
            ThicknessAnimation t = new ThicknessAnimation();
            t.To = new Thickness(86, 0, 0, 25.5);
            t.Duration = TimeSpan.Parse("0:0:0.2");
            t.EasingFunction = new ExponentialEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            Storyboard.SetTargetName(t,"rectCursor");
            Storyboard.SetTargetProperty(t, new PropertyPath(Rectangle.MarginProperty));
            myStoryboard.Children.Add(t);
            myStoryboard.Begin(this);
        }

        private void buttonCall_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation r = new DoubleAnimation();
            r.To = 0.5;
            r.Duration = TimeSpan.Parse("0:0:0.2");
            r.EasingFunction = new ExponentialEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            Storyboard.SetTargetName(r, "Mediator");
            Storyboard.SetTargetProperty(r, new PropertyPath("ScrollableWidthMultiplier"));
            Storyboard myStoryboard = new Storyboard();
            myStoryboard.Children.Add(r);

            ThicknessAnimation t = new ThicknessAnimation();
            t.To = new Thickness(222, 0, 0, 25.5);
            t.Duration = TimeSpan.Parse("0:0:0.2");
            t.EasingFunction = new ExponentialEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            Storyboard.SetTargetName(t, "rectCursor");
            Storyboard.SetTargetProperty(t, new PropertyPath(Rectangle.MarginProperty));
            myStoryboard.Children.Add(t);
            myStoryboard.Begin(this);

            ssViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }

        private void buttonSettings_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation r = new DoubleAnimation();
            r.To = 1.5;
            r.Duration = TimeSpan.Parse("0:0:0.2");
            r.EasingFunction = new ExponentialEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            Storyboard.SetTargetName(r, "Mediator");
            Storyboard.SetTargetProperty(r, new PropertyPath("ScrollableWidthMultiplier"));
            Storyboard myStoryboard = new Storyboard();
            myStoryboard.Children.Add(r);

            ThicknessAnimation t = new ThicknessAnimation();
            t.To = new Thickness(335, 0, 0, 25.5);
            t.Duration = TimeSpan.Parse("0:0:0.2");
            t.EasingFunction = new ExponentialEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            Storyboard.SetTargetName(t, "rectCursor");
            Storyboard.SetTargetProperty(t, new PropertyPath(Rectangle.MarginProperty));
            myStoryboard.Children.Add(t);
            myStoryboard.Begin(this);

            ssViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }

        private void buttonChat_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation r = new DoubleAnimation();
            r.To = 1;
            r.Duration = TimeSpan.Parse("0:0:0.2");
            r.EasingFunction = new ExponentialEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            Storyboard.SetTargetName(r, "Mediator");
            Storyboard.SetTargetProperty(r, new PropertyPath("ScrollableWidthMultiplier"));
            Storyboard myStoryboard = new Storyboard();
            myStoryboard.Children.Add(r);

            ThicknessAnimation t = new ThicknessAnimation();
            t.To = new Thickness(335, 0, 0, 25.5);
            t.Duration = TimeSpan.Parse("0:0:0.2");
            t.EasingFunction = new ExponentialEase()
            {
                EasingMode = EasingMode.EaseInOut
            };
            Storyboard.SetTargetName(t, "rectCursor");
            Storyboard.SetTargetProperty(t, new PropertyPath(Rectangle.MarginProperty));
            myStoryboard.Children.Add(t);
            myStoryboard.Begin(this);

            ssViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }


        private void ssViewer_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            if (ssViewer.HorizontalOffset <= (1366.0 / 2))
            {
                DoubleAnimation r = new DoubleAnimation();
                r.From = ssViewer.HorizontalOffset / (1366.0 * 2);
                r.To = 0;
                r.Duration = TimeSpan.Parse("0:0:0.2");
                r.EasingFunction = new ExponentialEase()
                {
                    EasingMode = EasingMode.EaseInOut
                };
                Storyboard.SetTargetName(r, "Mediator");
                Storyboard.SetTargetProperty(r, new PropertyPath("ScrollableWidthMultiplier"));
                Storyboard myStoryboard = new Storyboard();
                myStoryboard.Completed += new EventHandler(myStoryboard_Completed);
                myStoryboard.Children.Add(r);
                myStoryboard.Begin(this);

            }
            else if (ssViewer.HorizontalOffset > (1366.0 / 2) && ssViewer.HorizontalOffset <= (1366.0 + (1366.0 / 2)))
            {
                DoubleAnimation r = new DoubleAnimation();
                r.From = ssViewer.HorizontalOffset / (1366.0 * 2);
                r.To = 0.5;
                r.Duration = TimeSpan.Parse("0:0:0.2");
                r.EasingFunction = new ExponentialEase()
                {
                    EasingMode = EasingMode.EaseInOut
                };
                Storyboard.SetTargetName(r, "Mediator");
                Storyboard.SetTargetProperty(r, new PropertyPath("ScrollableWidthMultiplier"));
                Storyboard myStoryboard = new Storyboard();
                myStoryboard.Children.Add(r);
                myStoryboard.Begin(this);
                ssViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
            else if (ssViewer.HorizontalOffset > (1366.0 + (1366.0 / 2)))
            {
                DoubleAnimation r = new DoubleAnimation();
                r.From = ssViewer.HorizontalOffset / (1366.0 * 2);
                r.To = 1;
                r.Duration = TimeSpan.Parse("0:0:0.2");
                r.EasingFunction = new ExponentialEase()
                {
                    EasingMode = EasingMode.EaseInOut
                };
                Storyboard.SetTargetName(r, "Mediator");
                Storyboard.SetTargetProperty(r, new PropertyPath("ScrollableWidthMultiplier"));
                Storyboard myStoryboard = new Storyboard();
                myStoryboard.Children.Add(r);
                myStoryboard.Begin(this);
                ssViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
        }



    }

    public class ScrollViewerOffsetMediator : FrameworkElement
    {
        /// <summary>
        /// ScrollViewer instance to forward Offset changes on to.
        /// </summary>
        public ScrollViewer ScrollViewer
        {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }
        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register(
                "ScrollViewer",
                typeof(ScrollViewer),
                typeof(ScrollViewerOffsetMediator),
                new PropertyMetadata(OnScrollViewerChanged));
        private static void OnScrollViewerChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var mediator = (ScrollViewerOffsetMediator)o;
            var scrollViewer = (ScrollViewer)(e.NewValue);
            if (null != scrollViewer)
            {
                scrollViewer.ScrollToHorizontalOffset(mediator.HorizontalOffset);
            }
        }

        /// <summary>
        /// VerticalOffset property to forward to the ScrollViewer.
        /// </summary>
        public double HorizontalOffset
        {
            get { return (double)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }
        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.Register(
                "HorizontalOffset",
                typeof(double),
                typeof(ScrollViewerOffsetMediator),
                new PropertyMetadata(OnHorizontalOffsetChanged));
        public static void OnHorizontalOffsetChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var mediator = (ScrollViewerOffsetMediator)o;
            if (null != mediator.ScrollViewer)
            {
                mediator.ScrollViewer.ScrollToHorizontalOffset((double)(e.NewValue));
            }
        }

        /// <summary>
        /// Multiplier for ScrollableHeight property to forward to the ScrollViewer.
        /// </summary>
        /// <remarks>
        /// 0.0 means "scrolled to top"; 1.0 means "scrolled to bottom".
        /// </remarks>
        public double ScrollableWidthMultiplier
        {
            get { return (double)GetValue(ScrollableWidthMultiplierProperty); }
            set { SetValue(ScrollableWidthMultiplierProperty, value); }
        }
        public static readonly DependencyProperty ScrollableWidthMultiplierProperty =
            DependencyProperty.Register(
                "ScrollableWidthMultiplier",
                typeof(double),
                typeof(ScrollViewerOffsetMediator),
                new PropertyMetadata(OnScrollableWidthMultiplierChanged));
        public static void OnScrollableWidthMultiplierChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var mediator = (ScrollViewerOffsetMediator)o;
            var scrollViewer = mediator.ScrollViewer;
            if (null != scrollViewer)
            {
                scrollViewer.ScrollToHorizontalOffset((double)(e.NewValue) * scrollViewer.ScrollableWidth);
            }
        }
    }
}
