using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Interfaces.Services;
using RSG.Core.Models;
using System;
using System.Numerics;

namespace RSG.View.ViewModels
{
    public class StringEditViewModel : ViewModelBase
    {
        private readonly ICharacterSetProvider stringConfiguration;
        private readonly ICharacterSetService characterSetService;
        private readonly IRandomStringGenerator randomStringGenerator;

        public StringEditViewModel(ICharacterSetProvider stringConfiguration, ICharacterSetService characterSetService, IRandomStringGenerator randomStringGenerator)
        {
            this.stringConfiguration = stringConfiguration;
            this.characterSetService = characterSetService;
            this.randomStringGenerator = randomStringGenerator;
            GenerateCommand = new RelayCommand(RunGenerate, CanExecuteGenerate);
            AddCharactersCommand = new RelayCommand(RunGenerate, CanExecuteGenerate);
            RemoveCharactersCommand = new RelayCommand(RunRemoveCharacters, CanExecuteRemoveCharacters);
        }

        private bool CanExecuteGenerate()
        {
            return !string.IsNullOrWhiteSpace(LengthInput) && !string.IsNullOrWhiteSpace(IterationsInput);
        }

        private async void RunGenerate()
        {
            await randomStringGenerator.GenerateAsync(Iterations, Length);
        }

        private bool CanExecuteAddCharacters()
        {
            return !string.IsNullOrWhiteSpace(LengthInput) && !string.IsNullOrWhiteSpace(IterationsInput);
        }

        private async void RunAddCharacters()
        {
            
        }

        private bool CanExecuteRemoveCharacters()
        {
            return !string.IsNullOrWhiteSpace(LengthInput) && !string.IsNullOrWhiteSpace(IterationsInput);
        }

        private async void RunRemoveCharacters()
        {
            await randomStringGenerator.GenerateAsync(Iterations, Length);
        }

        public BigInteger Length { get; set; }
        public BigInteger Iterations { get; set; }
        public string LengthInput { get; set; }
        public string IterationsInput { get; set; }
        public string CharacterList
        {
            get
            {
                Array.Sort(characterSetService.CharacterList);
                return characterSetService.CharacterList.ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;

                foreach (var kvp in stringConfiguration.Characters)
                {
                    if (kvp.Value.Enabled)
                    {
                        var indexOfAnyEnabledCharacter = kvp.Value.Characters.IndexOfAny(value.ToCharArray());
                        var characterSetToAdd = string.Empty;
                        while (indexOfAnyEnabledCharacter != -1)
                        {
                            characterSetToAdd = value.Remove(indexOfAnyEnabledCharacter);
                        }
                        stringConfiguration.Characters.Add($"NewSet{characterSetToAdd}", new CharacterSet(characterSetToAdd, true));
                    }
                }
            }
        }

        public RelayCommand GenerateCommand { get; set; }
        public RelayCommand AddCharactersCommand { get; set; }
        public RelayCommand RemoveCharactersCommand { get; set; }
        public RelayCommand PasswordPresetCommand { get; set; }
        public RelayCommand HashPresetCommand { get; set; }
        public RelayCommand GuidPresetCommand { get; set; }
    }
}
