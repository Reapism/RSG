using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RSG.View.Windows
{
    /// <summary>
    /// Interaction logic for RsgWindow.xaml
    /// </summary>
    public partial class RsgWindow : Window
    {
        private DictionaryService dictionaryService;
        private RandomWordGenerator generator;

        public RsgWindow()
        {
            InitializeComponent();
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            dictionaryService = App.Container.Provider.GetRequiredService<DictionaryService>();
            generator = App.Container.Provider.GetRequiredService<RandomWordGenerator>();
        }

        private async void autoGenerateButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void autoGenerateButton_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void dictionary_Click(object sender, RoutedEventArgs e)
        {
            // must try with even number of words
            // if it divides evenly, last partition calculation will be 0, and needs logic to understand that
            await generator.GenerateRandomWordsResult(BigInteger.Parse("1003"));
            generator.GenerateRandomWordsResultCompleted += Generator_GenerateRandomWordsResultCompleted;
            var a = 10;

            // words are generating on seperate thread.
            // need to incorperate events when each partition finishes
            // and function for checking when all partitions finish.
            // this could give the DictionaryThreadService its use back.
            // Needs research on incorporating events in multithreaded env.
            
            // Gives 10 seconds for the words to catch up, with these iterations, we are looking at around 7500 p/ partition
            //Thread.Sleep(10000);
            //foreach(var c in result.Words.PartitionedWords)
            //{
            //    foreach(var d in c)
            //    {
            //        listBox.Items.Add($"{d.Key}:{d.Value.Word}");
            //    }
            //}
        }

        private void Generator_GenerateRandomWordsResultCompleted(object sender, GenerateRandomWordsResultEvents e)
        {
            if (e.Result == null)
            {
                MessageBox.Show("Is null");
                return;
            }

            foreach (var c in e.Result.Words.PartitionedWords)
            {
                foreach (var d in c)
                {
                    listBox.Items.Add($"{d.Key}:{d.Value.Word}");
                }
            }
        }
    }
}
