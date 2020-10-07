using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RSG.Core.Enums;
using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Services;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace RSG.View.ViewModels
{
    public class DictionaryEditViewModel : ViewModelBase
    {
        private readonly IRandomWordGenerator randomWordGenerator;
        private int currentProgress;
        private string iterations;

        public DictionaryEditViewModel(IRandomWordGenerator randomWordGenerator)
        {
            ViewLogCommand = new RelayCommand(RunViewLog, CanExecuteViewLog);
            RandomizeSettingsCommand = new RelayCommand(RunRandomizeSettings, CanExecuteRandomizeSettings);
            GenerateCommand = new RelayCommand(RunGenerate, CanExecuteGenerate);
            this.randomWordGenerator = randomWordGenerator;
            this.randomWordGenerator.GenerateChanged += RandomWordGenerator_GenerateChanged;
            this.randomWordGenerator.GenerateCompleted += RandomWordGenerator_GenerateCompleted;
        }

        private async void RandomWordGenerator_GenerateCompleted(object sender, DictionaryEventArgs e)
        {
            if (e.Cancelled is true || (!(e.Error is null)))
            {
                return;
            }

            if (!(e.Result is null))
            {
                var nonGeneratedWords = e.Result.Words.GetNumberOfOccurencesFor("aardvark");
                var generatedWords = (int)e.Result.Words.Count - nonGeneratedWords;
                var perc = (double)generatedWords / (int)e.Result.Words.Count;
                var str = ($"{nonGeneratedWords} out of {e.Result.Words.Count} were not generated successfully. {perc.ToString("P", CultureInfo.InvariantCulture)} successfully generated!");
                var c = true;
                CurrentProgress = 0;
            }

        }

        private async void RandomWordGenerator_GenerateChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            Task.Run(() => { Thread.Sleep(RandomProvider.Random.Value.Next(100, 500)); CurrentProgress = e.ProgressPercentage; });
        }

        private async void RunGenerate()
        {
            await randomWordGenerator.GenerateAsync(Iterations.ToBigInteger());
        }

        private bool CanExecuteGenerate()
        {
            return (!(randomWordGenerator is null) && (!(Iterations is null)) && CurrentProgress == 0);
        }

        private void RunRandomizeSettings()
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteRandomizeSettings()
        {
            return true;
        }

        private void RunViewLog()
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteViewLog()
        {
            return true;
        }

        public RsgDictionary SelectedDictionary { get; set; }
        public ObservableCollection<IRsgDictionary> Dictionaries { get; set; }
        public RandomizationType SelectedRandomizationType { get; set; }
        public IEnumerable<RandomizationType> RandomizationTypes { get; set; }

        public bool UseUppercase { get; set; }
        public bool UseSpacing { get; set; }
        public bool UsePunctuation { get; set; }
        public bool UseAliterations { get; set; }
        public bool UseNoise { get; set; }
        public bool UseMaxThreads { get; set; }

        public string Iterations
        {
            get => iterations;
            set
            {
                Set(nameof(Iterations), ref iterations, value);
                GenerateCommand.RaiseCanExecuteChanged();
            }
        }

        public int CurrentProgress
        {
            get => currentProgress;
            set
            {
                Set(nameof(CurrentProgress), ref currentProgress, value);
            }
        }

        public RelayCommand ViewLogCommand { get; set; }
        public RelayCommand RandomizeSettingsCommand { get; set; }
        public RelayCommand GenerateCommand { get; set; }

    }
}
