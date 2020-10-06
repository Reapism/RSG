using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RSG.Core.Enums;
using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Models;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RSG.View.ViewModels
{
    public class DictionaryEditViewModel : ViewModelBase
    {
        public DictionaryEditViewModel()
        {
            ViewLog = new RelayCommand(RunViewLog, CanExecuteViewLog);
            RandomizeSettings = new RelayCommand(RunRandomizeSettings, CanExecuteRandomizeSettings);
            Generate = new RelayCommand<RandomWordGenerator>(RunGenerate, CanExecuteGenerate);
        }

        private async void RunGenerate(RandomWordGenerator generator)
        {
            await generator.GenerateAsync(Iterations.ToBigInteger());
        }

        private bool CanExecuteGenerate(RandomWordGenerator generator)
        {
            return (!(generator is null));
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

        public string Iterations { get; set; }
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

        public RelayCommand ViewLog { get; set; }
        public RelayCommand RandomizeSettings { get; set; }
        public RelayCommand<RandomWordGenerator> Generate { get; set; }

    }
}
