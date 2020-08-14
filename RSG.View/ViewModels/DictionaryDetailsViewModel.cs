using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Interfaces;
using RSG.Core.Utilities;
using System.ComponentModel;
using System.Numerics;
using System.Windows;

namespace RSG.View.ViewModels
{
    public class DictionaryDetailsViewModel : ViewModelBase
    {
        private readonly RandomWordGenerator randomWordGenerator;

        public DictionaryDetailsViewModel()
        {
            this.randomWordGenerator = randomWordGenerator;
        }

        private void InitializeCommands()
        {
            RunCommand = new RelayCommand(OnRunAsync, CanRun);
        }

        private bool CanRun()
        {
            return true;
        }

        private void Generator_GenerateRandomWordsResultProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > 0)
                MessageBox.Show(e.ProgressPercentage.ToString());
        }
        // TODO - move events to code behind. and figure out how to properly use events with MVVM
        private void Generator_GenerateRandomWordsResultCompleted(object sender, GenerateRandomWordsResultEventArgs e)
        {
            if (!e.Cancelled && e.Error != null && e.Result != null)
            {
                Result = e.Result;
            }
        }

        public async void OnRunAsync()
        {
            var generator = App.Container.Provider.GetService<RandomWordGenerator>();
            generator.GenerateRandomWordsResultCompleted += Generator_GenerateRandomWordsResultCompleted;
            generator.GenerateRandomWordsResultProgressChanged += Generator_GenerateRandomWordsResultProgressChanged;
            await generator.GenerateRandomWordsResult(BigInteger.Parse("1000000"));

            //foreach (var c in DebugUtility.debugKvp)
            //{
            //    MessageBox.Show($"{c.Key} - {c.Value}");
            //}

        }
        public RelayCommand RunCommand { get; set; }

        public IDictionaryResult Result { get; set; }

    }
}
