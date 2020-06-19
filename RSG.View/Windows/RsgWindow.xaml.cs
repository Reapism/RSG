using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;
using RSG.View.Managers;
using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RSG.View.Windows
{
    /// <summary>
    /// Interaction logic for RsgWindow.xaml
    /// </summary>
    public partial class RsgWindow : Window
    {
        private DictionaryService dictionaryService;
        private RandomWordGenerator randomWordGenerator;
        private RandomStringGenerator randomStringGenerator;
        public RsgWindow()
        {
            InitializeComponent();
            InitializeDependencies();
            InitalizeEvents();
        }

        private void InitializeDependencies()
        {
            dictionaryService = App.Container.Provider.GetService<DictionaryService>();
            randomWordGenerator = App.Container.Provider.GetService<RandomWordGenerator>();
            randomStringGenerator = App.Container.Provider.GetService<RandomStringGenerator>();
        }

        private void InitalizeEvents()
        {
            randomWordGenerator.GenerateRandomWordsResultCompleted += Generator_GenerateRandomWordsResultCompleted;
        }

        private async void dictionary_Click(object sender, RoutedEventArgs e)
        {
            Navigation.SelectedIndex = 1;
            // must try with even number of words
            // if it divides evenly, last partition calculation will be 0, and needs logic to understand that
            var dictionary = await dictionaryService.GetSelectedDictionary();
            var numberOfWords = BigInteger.Parse("100003");
            DebugUtility.Write((nameof(dictionary_Click), $"Dictionary: {dictionary.Name} : Generating {numberOfWords} number of words"));
            await randomWordGenerator.GenerateRandomWordsResult(numberOfWords);
            
        }

        private async void NavigationMenu_Click(object sender, RoutedEventArgs e)
        {
            var selectedMenuButton = sender as Button;

            if (string.IsNullOrEmpty(selectedMenuButton.Content.ToString()))
            {
                throw new Exception($"{sender.GetType().Name} must have a content matching a Page");
            }
            Navigation.SelectedIndex = int.Parse(selectedMenuButton.Content.ToString());
            
        }

        private void Generator_GenerateRandomWordsResultCompleted(object sender, GenerateRandomWordsResultEventArgs e)
        {
            if (e.Error != null || e.Cancelled)
            {
                MessageBox.Show("The operation has errored out.");
                return;
            }

            var typeName = sender.GetType().Name;
        }

        private void Dictionary_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var result = randomStringGenerator.GenerateRandomStringsResult(5, 10);
            foreach (string s in result.Strings)
                MessageBox.Show(s);
        }
    }
}
