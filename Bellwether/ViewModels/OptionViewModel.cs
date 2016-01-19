using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Bellwether.Commands;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;

namespace Bellwether.ViewModels
{
    public class OptionViewModel : ViewModel
    {
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
        private bool _isDataSynchronize;

        public bool IsDataSynchronize
        {
            get { return _isDataSynchronize; }
            set
            {
                _isDataSynchronize = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand ChangeLanguageCommand { get; set; }
        public RelayCommand ChangeSynchronize { get; set; }
        public OptionViewModel()
        {
            ChangeLanguageCommand = new RelayCommand(ChangeLanguage);
            ChangeSynchronize = new RelayCommand(ChangeSynchronizeData);
            LoadContent();
        }

        private async void ChangeSynchronizeData()
        {
           var isSync = await
                ServiceExecutor.ExecuteAsync(
                    () => ServiceFactory.ResourceService.SaveValueForKey("SynchronizeData", IsDataSynchronize.ToString()));
            if (!isSync.IsValid)
                IsDataSynchronize = !IsDataSynchronize;
        }

        private async void ChangeLanguage()
        {
            if (BasicLanguageVerification())
            {
                var languageIsChanged = await
                    ServiceExecutor.ExecuteAsyncIfSyncData(
                        () =>
                            ServiceFactory.LanguageManagementService.ChangeApplicationLanguage(
                                SelectedLanguage.LanguageShortName));
                if (languageIsChanged.IsValid)
                    LoadContent();
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
            var langs = ServiceExecutor.Execute(() => ServiceFactory.LanguageService.GetLocalLanguages());
            Languages = new ObservableCollection<BellwetherLanguage>(langs.Data);
        }

        private async void LoadContent()
        {
            LoadLanguages();
            await LoadLanguageContent();
            await LoadApplicationSettings();
        }

        private async Task LoadApplicationSettings()
        {
            SettingsViewModel settings = await ServiceFactory.ResourceService.GetAppSettings();
            IsDataSynchronize = settings.SynchronizeData;
            CurrentLanguage = ServiceFactory.LanguageService.GetLocalLanguageByShortName(settings.ApplicationLanguage);
            SelectedLanguage = CurrentLanguage;
        }

        private async Task LoadLanguageContent()
        {
            var contentKeys = new[] { "Settings", "ChangeLanguage", "ApplyLanguage", "CurrentLanguage", "DataSynchronization" };
            var contentDictionary = await ServiceFactory.ResourceService.GetLanguageContentForKeys(contentKeys);
            TextSettings = contentDictionary["Settings"];
            TextChangeLanguage = contentDictionary["ChangeLanguage"];
            TextApplyLanguage = contentDictionary["ApplyLanguage"];
            TextCurrentLanguage = contentDictionary["CurrentLanguage"];
            TextSynchronizeData = contentDictionary["DataSynchronization"];
        }

        private string _textSettings;
        public string TextSettings { get { return _textSettings; } set { _textSettings = value; NotifyPropertyChanged(); } }
        private string _textCurrentLanguage;
        public string TextCurrentLanguage { get { return _textCurrentLanguage; } set { _textCurrentLanguage = value; NotifyPropertyChanged(); } }
        private string _textChangeLanguage;
        public string TextChangeLanguage { get { return _textChangeLanguage; } set { _textChangeLanguage = value; NotifyPropertyChanged(); } }
        private string _textApplyLanguage;
        public string TextApplyLanguage { get { return _textApplyLanguage; } set { _textApplyLanguage = value; NotifyPropertyChanged(); } }
        private string _textSynchronizeData;
        public string TextSynchronizeData
        {
            get { return _textSynchronizeData; }
            set
            {
                _textSynchronizeData = value;
                NotifyPropertyChanged();
            }
        }


    }
}
