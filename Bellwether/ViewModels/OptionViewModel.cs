//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Threading.Tasks;
//using Bellwether.Commands;
//using Bellwether.Models.Models;
//using Bellwether.Repositories.Entities;
//using Bellwether.Repositories.Factories;
//using Bellwether.Services.Factories;
//using Bellwether.Services.Services;
//using Bellwether.Services.Utility;

//namespace Bellwether.ViewModels
//{
//    public class OptionViewModel : ViewModel
//    {
//        private readonly ILanguageService _languageService;
//        private readonly IResourceService _resourceService;
//        private IJokeService _jokeService;
//        public OptionViewModel()
//        {
//            _languageService = ServiceFactory.LanguageService;
//            _resourceService = ServiceFactory.ResourceService;
//            _jokeService = ServiceFactory.JokeService;
//            ChangeLanguageCommand = new RelayCommand(ChangeLanguage);
//            LoadContent();
//        }

//        private BellwetherLanguageDao _currentLanguage;
//        public BellwetherLanguageDao CurrentLanguage
//        {
//            get { return _currentLanguage; }
//            set
//            {
//                _currentLanguage = value;
//                NotifyPropertyChanged();
//            }
//        }
//        private BellwetherLanguageDao _selectedLanguage;
//        public BellwetherLanguageDao SelectedLanguage
//        {
//            get { return _selectedLanguage; }
//            set
//            {
//                _selectedLanguage = value;
//                NotifyPropertyChanged();
//            }
//        }
//        public ObservableCollection<BellwetherLanguageDao> Languages { get; set; }
//        public RelayCommand ChangeLanguageCommand { get; set; }

//        private async void ChangeLanguage()
//        {
//            if (BasicLanguageVerification())
//            {
//                var resource = await _resourceService.GetApplicationApiUrl();
//                var vesionLanguageFile =
//                        await
//                            _languageService.GetLanguageFile(resource.GetLanguageFile + SelectedLanguage.Id);
//                if (vesionLanguageFile != null)
//                {
//                    var mandatoryVersion = await ServiceFactory.WebBellwetherVersionService.GetVersionForLanguage(resource.GetVersion + SelectedLanguage.Id);
//                    await _resourceService.ChangeLanguage(vesionLanguageFile, new BellwetherLanguage {Id = SelectedLanguage.Id,LanguageShortName = SelectedLanguage.LanguageShortName,LanguageName = SelectedLanguage.LanguageName,LanguageVersion = Convert.ToDouble(mandatoryVersion.LanguageVersion)});
//                    OldRepositoryFactory.JokeRepository.RemoveJokes();
//                    OldRepositoryFactory.JokeCategoryRepository.RemoveJokeCategories();
//                    await _jokeService.CheckAndFillJokeCategories(resource.GetJokeCategories + SelectedLanguage.Id);
//                    await
//                        _resourceService.ChangeJokeCategoryVersion(mandatoryVersion.JokeCategoryVersion);
                    
//                    await _jokeService.CheckAndFillJokes(resource.GetJokes + SelectedLanguage.Id);
//                    await _resourceService.ChangeJokeVersion(mandatoryVersion.JokeVersion);
//                    LoadContent();
//                }
//            }
//        }

//        private bool BasicLanguageVerification()
//        {
//            if (_selectedLanguage == null)
//                return false;
//            if (_currentLanguage == null)
//                return false;
//            if (_selectedLanguage == _currentLanguage)
//                return false;
//            return true;
//        }
//        private void LoadLanguages()
//        {
//            Languages = new ObservableCollection<BellwetherLanguageDao>(_languageService.GetLocalLanguages()); 
//        }
       
//        private async void LoadContent()
//        {
//            LoadLanguages();
//            await LoadLangStaticData();
//            await LoadAppStaticData();
//        }

//        private async Task LoadAppStaticData()
//        {
//            var requriedKeysAndValue = await _resourceService.GetApplicationSettings();
//            string currLang = requriedKeysAndValue.AppLanguage;
//            CurrentLanguage =
//                _languageService
//                    .GetLocalLanguages()
//                    .FirstOrDefault(x => x.LanguageShortName == currLang);
//            SelectedLanguage = CurrentLanguage;
//        }

//        private async Task LoadLangStaticData()
//        {
//            var lang = new List<string> {"Settings", "ChangeLanguage", "ApplyLanguage", "CurrentLanguage" };
//            var requiredKeysAndvalues = await _resourceService.GetLanguageContentScenario(lang);
//            TextSettings = requiredKeysAndvalues["Settings"];
//            TextChangeLanguage = requiredKeysAndvalues["ChangeLanguage"];
//            TextApplyLanguage = requiredKeysAndvalues["ApplyLanguage"];
//            TextCurrentLanguage = requiredKeysAndvalues["CurrentLanguage"];
//        }

//        private string _textSettings;
//        public string TextSettings { get { return _textSettings; } set { _textSettings = value; NotifyPropertyChanged(); } }
//        private string _textCurrentLanguage;
//        public string TextCurrentLanguage { get { return _textCurrentLanguage; } set { _textCurrentLanguage = value; NotifyPropertyChanged(); } }
//        private string _textChangeLanguage;
//        public string TextChangeLanguage { get { return _textChangeLanguage; } set { _textChangeLanguage = value; NotifyPropertyChanged(); } }
//        private string _textApplyLanguage;

//        public string TextApplyLanguage { get { return _textApplyLanguage; } set { _textApplyLanguage = value; NotifyPropertyChanged(); } }


//    }
//}
