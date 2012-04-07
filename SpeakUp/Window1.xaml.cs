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
using System.Windows.Shapes;

namespace SpeakUp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            WordPrediction predictor = new WordPrediction();
            double x;
            Trie xx;
            predictor.EN_Trie.ContainStringInTrie("love", out x,out xx);
            xx.ContainStringInTrie("children", out x);
            List<string> tmp = predictor.Predict("I drink be");
        }
    }
}
