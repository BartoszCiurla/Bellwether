using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bellwether.Commands;
using Bellwether.Models.Models;
using Bellwether.Services;
using Bellwether.Services.Services;

namespace Bellwether.ViewModels
{
    public class OptionViewModel : ViewModel
    {
        private readonly ILanguageService _languageService;
        private readonly IResourceService _resourceService;
        private readonly IVersionService _versionService;
        public OptionViewModel(ILanguageService languageService, IResourceService resourceService)
        {
            _languageService = languageService;
            _resourceService = resourceService;
            _versionService = new VersionService(_languageService, _resourceService);
            Languages = new ObservableCollection<BellwetherLanguage>();
            ChangeLanguageCommand = new RelayCommand(ChangeLanguage);
            LoadLanguages();
            LoadCurrentLanguage();
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
            bool selectedNotLangEqualsToCurrentLang = _selectedLanguage.Id != _currentLanguage.Id;
            if (selectedNotLangEqualsToCurrentLang)
            {
                await _versionService.SetNewLanguage(_selectedLanguage.Id);
                LoadCurrentLanguage();
            }
            else
            {
                //tutaj jakas notyfikacja powinna isć ale to jeszcze nie czas 
            }                   
        }

        private void LoadLanguages()
        {
            _languageService.GetLocalLanguages().ToList().ForEach(x =>
            {
                Languages.Add(x);
            });
        }
        private async void LoadCurrentLanguage()
        {
            string currentLangShortName = await _resourceService.GetApplicationLanguage();
            CurrentLanguage = Languages.FirstOrDefault(x => x.LanguageShortName == currentLangShortName);
            SelectedLanguage = CurrentLanguage;
        }


    }
}
