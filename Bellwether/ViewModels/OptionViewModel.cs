using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Bellwether.Commands;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
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
            ChangeLanguageCommand = new RelayCommand(ChangeLanguage);
            LoadContent();
        }

        private BellwetherLanguage _currentLanguage;
        public BellwetherLanguage CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                _currentLanguage = value;
                NotifyPropertyChanged();
            }
        }
        private BellwetherLanguage _selectedLanguage;
        public BellwetherLanguage SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<BellwetherLanguage> Languages { get; set; }
        public RelayCommand ChangeLanguageCommand { get; set; }

        private async void ChangeLanguage()
        {
            if (BasicLanguageVerification())
            {
                AppLanguageSettingsViewModel languageSettings = await _resourceService.GetApplicationLanguageSettings();
                var vesionLanguageFile =
                        await
                            _languageService.GetLanguageFile(languageSettings.GetLanguageFileApiUrl + SelectedLanguage.Id);
                if (vesionLanguageFile != null)
                {
                    await _resourceService.ChangeLanguage(vesionLanguageFile, SelectedLanguage);
                    LoadContent();
                }
            }
        }

        private bool BasicLanguageVerification()
        {
            if (_selectedLanguage == null)
                return false;
            if (_currentLanguage == null)
                return false;
            if (_selectedLanguage == _currentLanguage)
                return false;
            return true;
        }
        private void LoadLanguages()
        {
            Languages = new ObservableCollection<BellwetherLanguage>();
            _languageService.GetLocalLanguages().ToList().ForEach(x =>
            {
                Languages.Add(x);
            });
        }
       
        private async void LoadContent()
        {
            LoadLanguages();
            await LoadLangStaticData();
            await LoadAppStaticData();
        }

        private async Task LoadAppStaticData()
        {
            var requriedKeysAndValue = await _resourceService.GetApplicationLanguageSettings();
            string currLang = requriedKeysAndValue.ApplicationLanguage;
            CurrentLanguage =
                _languageService
                    .GetLocalLanguages()
                    .FirstOrDefault(x => x.LanguageShortName == currLang);
            SelectedLanguage = CurrentLanguage;
        }

        private async Task LoadLangStaticData()
        {
            var lang = new List<string> {"Settings", "ChangeLanguage", "ApplyLanguage", "CurrentLanguage" };
            var requiredKeysAndvalues = await _resourceService.GetLanguageContentScenario(lang);
            TextSettings = requiredKeysAndvalues["Settings"];
            TextChangeLanguage = requiredKeysAndvalues["ChangeLanguage"];
            TextApplyLanguage = requiredKeysAndvalues["ApplyLanguage"];
            TextCurrentLanguage = requiredKeysAndvalues["CurrentLanguage"];
        }

        private string _textSettings;
        public string TextSettings { get { return _textSettings; } set { _textSettings = value; NotifyPropertyChanged(); } }
        private string _textCurrentLanguage;
        public string TextCurrentLanguage { get { return _textCurrentLanguage; } set { _textCurrentLanguage = value; NotifyPropertyChanged(); } }
        private string _textChangeLanguage;
        public string TextChangeLanguage { get { return _textChangeLanguage; } set { _textChangeLanguage = value; NotifyPropertyChanged(); } }
        private string _textApplyLanguage;
        public string TextApplyLanguage { get { return _textApplyLanguage; } set { _textApplyLanguage = value; NotifyPropertyChanged(); } }


    }
}
