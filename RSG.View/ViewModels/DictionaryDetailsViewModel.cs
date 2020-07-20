using RSG.Core.Interfaces;
using System.ComponentModel;

namespace RSG.View.ViewModels
{
    public class DictionaryDetailsViewModel : INotifyPropertyChanged
    {
        public DictionaryDetailsViewModel()
        {

        }

        public IDictionaryResult Result { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
