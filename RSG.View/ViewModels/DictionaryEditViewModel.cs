using GalaSoft.MvvmLight.CommandWpf;
using RSG.Core.Enums;
using RSG.Core.Models;
using System;
using System.ComponentModel;
using System.Windows;

namespace RSG.View.ViewModels
{
    public class DictionaryEditViewModel
    {
        public DictionaryEditViewModel()
        {
            Export = new RelayCommand(OnExport, CanExport);
        }

        private bool CanExport()
        {
            return true;
        }

        private async void OnExport()
        {
            throw new NotImplementedException();
        }

        public async void LoadEditView()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return;
            }


        }

        public string NumberOfWords { get; set; }

        public RsgDictionary Dictionary { get; set; }

        public string RandomizationType { get; set; }

        public bool UseUppercase { get; set; }
        public bool UseSpacing { get; set; }
        public bool UsePunctuation { get; set; }
        public bool UseAliterations { get; set; }
        public string AliterationsMin { get; set; }
        public string AliterationsMax { get; set; }
        public bool UseNoise { get; set; }
        public string NoiseMin { get; set; }
        public string NoiseMax { get; set; }
        public bool GenerateBook{ get; set; }
        public bool UseMaxThreads { get; set; }
        public bool ShowStats{ get; set; }

        public RelayCommand Export { get; set; }
        public RelayCommand ViewLog { get; set; }
        public RelayCommand RandomizeSettings { get; set; }

    }
}
