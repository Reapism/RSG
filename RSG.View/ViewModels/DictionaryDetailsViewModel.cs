using Microsoft.Extensions.DependencyInjection;
using Prism.Commands;
using RSG.Core.Interfaces;
using RSG.Core.Utilities;
using System.ComponentModel;
using System.Numerics;

namespace RSG.View.ViewModels
{
    public class DictionaryDetailsViewModel : INotifyPropertyChanged
    {
        public DictionaryDetailsViewModel()
        {
            var generator = App.Container.Provider.GetService<RandomWordGenerator>();
            generator.GenerateRandomWordsResultCompleted += Generator_GenerateRandomWordsResultCompleted;
            generator.GenerateRandomWordsResultProgressChanged += Generator_GenerateRandomWordsResultProgressChanged;
            RunCommand = new DelegateCommand(OnRunAsync, CanRun);
        }

        private bool CanRun()
        {
            return true;
        }

        private void Generator_GenerateRandomWordsResultProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void Generator_GenerateRandomWordsResultCompleted(object sender, GenerateRandomWordsResultEventArgs e)
        {
            if (!e.Cancelled && e.Error != null && e.Result != null)
            {
                Result = e.Result;
            }
        }

        public DelegateCommand RunCommand { get; private set; }

        public async void OnRunAsync()
        {
            var generator = App.Container.Provider.GetService<RandomWordGenerator>();
            await generator.GenerateRandomWordsResult(BigInteger.Parse("100000"));
        }

        public IDictionaryResult Result { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
