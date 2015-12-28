using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bellwether.Commands;
using Bellwether.Models.Models;
using Bellwether.Services.Services;

namespace Bellwether.ViewModels
{
    public class OptionViewModel : ViewModel
    {
        private readonly ILanguageService _languageService;
        private readonly IResourceService _resourceService;
        public OptionViewModel(ILanguageService languageService, IResourceService resourceService)
        {
            _languageService = languageService;
            _resourceService = resourceService;
            Languages = new ObservableCollection<BellwetherLanguage>();
            ChangeLanguageCommand = new RelayCommand(ChangeLanguage);
            LoadLanguages();
            LoadCurrentLanguage();
        }

        private BellwetherLanguage _currentLanguage;
        public BellwetherLanguage BellwetherLanguage
        {
            get { return _currentLanguage; }
            set
            {
                _currentLanguage = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<BellwetherLanguage> Languages { get; set; }
        public RelayCommand ChangeLanguageCommand { get; set; }

        private async void ChangeLanguage()
        {
            string apiUrl = await
                _resourceService.TakeKeyValueFromLocalAppResources("GetLanguageFileApiUrl");
            Dictionary<string, string> languageFile =
                await
                    _languageService.GetLanguageFile(BellwetherLanguage,apiUrl );
            foreach (var VARIABLE in languageFile)
            {
                Debug.WriteLine(VARIABLE.Key + " " + VARIABLE.Value);
            }
        }
        private void LoadLanguages()
        {
            _languageService.GetLanguages().ToList().ForEach(x =>
            {
                Languages.Add(x);
            });
        }
        private async void LoadCurrentLanguage()
        {
            string currentLangShortName = await _resourceService.TakeKeyValueFromLocalAppResources("ApplicationLanguage");
            BellwetherLanguage = Languages.FirstOrDefault(x => x.LanguageShortName == currentLangShortName);
        }


    }
}
