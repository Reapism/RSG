using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Models;
using RSG.Core.Services;
using System.Windows;

namespace RSG.View.Windows
{
    /// <summary>
    /// Interaction logic for RsgWindow.xaml
    /// </summary>
    public partial class RsgWindow : Window
    {
        private DictionaryService dictionaryService;

        public RsgWindow()
        {
            InitializeComponent();
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            dictionaryService = App.Container.Provider.GetService<DictionaryService>();
        }

        private void autoGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            RsgDictionary x = dictionaryService.GetSelectedDictionary();

        }

        private void autoGenerateButton_Copy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
