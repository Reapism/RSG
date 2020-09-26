using RSG.Core.Utilities;
using System;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace RSG.View.Views
{
    /// <summary>
    /// Interaction logic for DictionaryDetailsView.xaml
    /// </summary>
    public partial class DictionaryDetailsView : UserControl
    {
        private readonly RandomWordGenerator randomWordGenerator;

        public DictionaryDetailsView()
        {
            InitializeComponent();

            randomWordGenerator = IocContainer.Container.GetInstance<RandomWordGenerator>();

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            randomWordGenerator.GenerateCompleted += RandomWordGenerator_GenerateRandomWordsResultCompleted;
            randomWordGenerator.GenerateChanged += RandomWordGenerator_GenerateRandomWordsResultProgressChanged;
            
        }

        private void RandomWordGenerator_GenerateRandomWordsResultProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.Progress.Value = e.ProgressPercentage;
        }

        private void RandomWordGenerator_GenerateRandomWordsResultCompleted(object sender, DictionaryEventArgs e)
        {
            var result = e.Result;

            var nonGeneratedWords = result.Words.GetNumberOfOccurencesFor("aardvark");
            var generatedWords = (int)result.Words.Count - nonGeneratedWords;
            var perc = (double)generatedWords / (int)result.Words.Count;
            RunCommand.IsEnabled = true;
            MessageBox.Show($"{nonGeneratedWords} out of {result.Words.Count} were not generated successfully. {perc.ToString("P",CultureInfo.InvariantCulture)} successfully generated!");
        }

        private async void RunCommand_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RunCommand.IsEnabled = false;
            await randomWordGenerator.Generate(BigInteger.Parse("100004"));
        }
    }
}
