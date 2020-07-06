using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Configuration;
using RSG.Core.Extensions;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;
using RSG.View.Managers;
using RSG.View.Views;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
        private PageManager pageManager;

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
            pageManager = App.Container.Provider.GetService<PageManager>();
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

            var numberOfWords = BigInteger.Parse("100003");
            await randomWordGenerator.GenerateRandomWordsResult(numberOfWords);

        }

        private async void NavigationMenu_Click(object sender, RoutedEventArgs e)
        {
            var selectedMenuButton = sender as Button;

            pageManager.SetNavigationalMenuTabPage(Navigation, selectedMenuButton.Tag.ToString());
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var result = randomStringGenerator.GenerateRandomStringsResult(5, 10);
            foreach (string s in result.Strings)
                MessageBox.Show(s);
        }
    }
}
