using RSG.Core.Utilities;
using RSG.View.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RSG.View.Views
{
    /// <summary>
    /// Interaction logic for DictionaryDetailsView.xaml
    /// </summary>
    public partial class DictionaryDetailsView : UserControl
    {
        private readonly DictionaryDetailsViewModel viewModel;
        private readonly RandomWordGenerator randomWordGenerator;

        public DictionaryDetailsView(RandomWordGenerator generator)
        {
            try
            {
                InitializeComponent();

            }
            catch (Exception e)
            {
                throw e;
            }

            viewModel = DataContext as DictionaryDetailsViewModel;
            this.RunCommand.Click += RunButton_Click;
            this.randomWordGenerator = generator;
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            randomWordGenerator.GenerateRandomWordsResultCompleted += randomWordGenerator_GenerateRandomWordsResultCompleted;
            randomWordGenerator.GenerateRandomWordsResultProgressChanged += randomWordGenerator_GenerateRandomWordsResultProgressChanged;
        }

        private void randomWordGenerator_GenerateRandomWordsResultProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress.Value = e.ProgressPercentage;
        }

        private void randomWordGenerator_GenerateRandomWordsResultCompleted(object sender, GenerateRandomWordsResultEventArgs e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (!(e.Error is null))
            {
                MessageBox.Show($"Hey, theres an error here: idiot:\n {e.Error.Message}");
            }

            e.Result.Words.PartitionedWords.TryDequeue(out var c);
            MessageBox.Show(c.Values.FirstOrDefault().Word);
        }
    }
}
