using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RSG.Core.Interfaces;
using RSG.Core.Utilities;
using System.Numerics;

namespace RSG.View.ViewModels
{
    public class DictionaryDetailsViewModel : ViewModelBase
    {
        public DictionaryDetailsViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            RunCommand = new RelayCommand<RandomWordGenerator>(OnRunAsync, CanRun);
        }

        private bool CanRun(RandomWordGenerator randomWordGenerator)
        {
            return true;
        }

        private async void OnRunAsync(RandomWordGenerator generator)
        {
            await generator.GenerateRandomWordsResult(BigInteger.Parse("1000000"));
        }

        public RelayCommand<RandomWordGenerator> RunCommand { get; set; }

        public IDictionaryResult Result { get; set; }
    }
}
