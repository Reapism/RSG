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
        private RandomWordGenerator randomWordGenerator;

        public RsgWindow()
        {
            InitializeComponent();
            InitializeDependencies();
            InitalizeEvents();
            this.dictionaryService = dictionaryService;
            this.randomWordGenerator = randomWordGenerator;
        }

        private void InitializeDependencies()
        {
            dictionaryService = App.Container.Provider.GetService<DictionaryService>();
            randomWordGenerator = App.Container.Provider.GetService<RandomWordGenerator>();
        }

        private void InitalizeEvents()
        {
            randomWordGenerator.GenerateRandomWordsResultCompleted += Generator_GenerateRandomWordsResultCompleted;
        }
        private async void autoGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var kvp in DebugUtility.debugKvp)
            {
                listBox.Items.Add($"{kvp.Key}:{kvp.Value.Item1}, {kvp.Value.Item2}");
            }
        }

        private async void dictionary_Click(object sender, RoutedEventArgs e)
        {
            // must try with even number of words
            // if it divides evenly, last partition calculation will be 0, and needs logic to understand that
            var dictionary = await dictionaryService.GetSelectedDictionary();
            var numberOfWords = BigInteger.Parse("100003");
            DebugUtility.Write((nameof(dictionary_Click), $"Dictionary: {dictionary.Name} : Generating {numberOfWords} number of words"));
            await randomWordGenerator.GenerateRandomWordsResult(numberOfWords);

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
            
            listBox.Items.Add($"Dictionary: {e.Result.Dictionary.Name} : Generating {e.Result.Words.Count} number of words");

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
