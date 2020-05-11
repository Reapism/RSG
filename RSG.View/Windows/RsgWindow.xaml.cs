using Microsoft.Extensions.DependencyInjection;
using RSG.Core;
using RSG.Core.Factories;
using RSG.Core.Interfaces;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace RSG.View.Windows
{
    /// <summary>
    /// Interaction logic for RsgWindow.xaml
    /// </summary>
    public partial class RsgWindow : Window
    {
        public RsgWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var characters = new CharacterSetServiceFactory().Create();
            var generator = new RandomStringGenerator(characters);
            var result = generator.GenerateRandomStrings(10, 10);
            
            foreach (var rndStr in result.Strings)
                MessageBox.Show(rndStr);
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {

            
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            var service = App.Container.Provider.GetService<DictionaryService>();
            var dictionary = service. GetSelectedDictionary();
            MessageBox.Show($"{dictionary.Name} - {dictionary.Description}");
        }
    }
}
