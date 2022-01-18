using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RSG.Core.Enums;
using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Services;
using RSG.Core.Models;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RSG.View.ViewModels
{
    public class DictionaryEditViewModel : ViewModelBase
    {
        private readonly IGeneratorEvents randomWordGenerator;
        private int currentProgress;
        private string iterations;

        public DictionaryEditViewModel(IGeneratorEvents randomWordGenerator)
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
                CurrentProgress = 0;
            }
        }

        private async void RandomWordGenerator_GenerateChanged(object sender, ProgressChangedEventArgs e)
        {

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
