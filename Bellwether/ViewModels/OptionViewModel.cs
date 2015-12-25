using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using Bellwether.Services.Services;

namespace Bellwether.ViewModels
{
    public class OptionViewModel : ViewModel
    {
        private Language _currentLanguage;
        public Language CurreentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                _currentLanguage = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Language> Languages { get; set; }

        public OptionViewModel()
        {
            LanguageService service = new LanguageService();
       
        }

    }
}
