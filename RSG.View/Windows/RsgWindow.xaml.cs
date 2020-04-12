using RSG.Core.Factories;
using RSG.Core.Utilities;
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
            var rndStrs = generator.GenerateRandomStrings(10, 10);
            foreach (var rndStr in rndStrs)
                MessageBox.Show(rndStr);
        }
    }
}
