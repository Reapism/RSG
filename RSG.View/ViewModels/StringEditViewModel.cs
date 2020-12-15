using GalaSoft.MvvmLight;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Interfaces.Services;
using System;
using System.Numerics;
using System.Linq;
using RSG.Core.Models;

namespace RSG.View.ViewModels
{
    public class StringEditViewModel : ViewModelBase
    {
        private readonly IStringConfiguration stringConfiguration;
        private readonly ICharacterSetService characterSetService;

        public StringEditViewModel(IStringConfiguration stringConfiguration, ICharacterSetService characterSetService)
        {
            this.stringConfiguration = stringConfiguration;
            this.characterSetService = characterSetService;
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
                        stringConfiguration.Characters.Add($"NewSet{characterSetToAdd}", new SingleCharacterSet(characterSetToAdd, true));
                    }
                }
            }
        }
    }
}
