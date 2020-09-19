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
            RunCommand = new RelayCommand(OnRunAsync, CanRun);
        }

        private bool CanRun()
        {
            return true;
        }

        private async void OnRunAsync()
        {
            var generator = IocContainer.Container.GetInstance<RandomWordGenerator>();
            await generator.GenerateRandomWordsResult(BigInteger.Parse("1000000"));
        }

        public RelayCommand RunCommand { get; set; }

        public IDictionaryResult Result { get; set; }
    }
}
