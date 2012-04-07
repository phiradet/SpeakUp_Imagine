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

namespace SpeakUp
{
    /// <summary>
    /// Interaction logic for PageChat.xaml
    /// </summary>
    public partial class PageChat : UserControl
    {
        //++++++++++++++ Language Processing ++++++++++++
        WordPrediction predictor = new WordPrediction();
        //+++++++++++++++++++++++++++++++++++++++++++++++
        public PageChat()
        {
            InitializeComponent();
        }

        private void speakBtn_Click(object sender, RoutedEventArgs e)
        {
            string currSentence = inputTextBox.Text;
        }


        private void selectPredictedItem(int itemNum)
        {
            string selectedItem = autoCompleteBox.Items[itemNum].ToString();
            string currSentence = inputTextBox.Text;
            //inputTextBox.Text = WordPrediction.ApplyPredictWord(selectedItem,currSentence);
        }

        private void inputTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
            }
            else
            {
                string currSentence = inputTextBox.Text.ToLower();
                if (currSentence.Length < 2)
                    return;
                List<string> predictedItem = predictor.Predict(currSentence);
                autoCompleteBox.Items.Clear();
                foreach (string currPredictedItem in predictedItem)
                {
                    autoCompleteBox.Items.Add(currPredictedItem);
                }
            }
        }

        private void inputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
