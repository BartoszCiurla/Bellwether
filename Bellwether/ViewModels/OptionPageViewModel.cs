using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Bellwether.Commands;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;

namespace Bellwether.ViewModels
{
    public class OptionPageViewModel : ViewModel
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
        private VoiceViewModel _selectedVoice;
        public VoiceViewModel SelectedVoice
        {
            get { return _selectedVoice; }
            set { _selectedVoice = value; NotifyPropertyChanged(); }
        }

        private VoiceViewModel _currentVoice;
        public VoiceViewModel CurrentVoice
        {
            get
            {
                return _currentVoice;                
            }
            set
            {
                _currentVoice = value;
                NotifyPropertyChanged();
            }
        }
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
        public ObservableCollection<BellwetherLanguage> Languages { get; set; }
        public ObservableCollection<VoiceViewModel> Voices { get; set; }
        public RelayCommand ChangeLanguageCommand { get; set; }
        public RelayCommand ChangeSynchronizeCommand { get; set; }
        public RelayCommand ChangeVoiceCommand { get; set; }
        public OptionPageViewModel()
        {
            Languages = new ObservableCollection<BellwetherLanguage>();
            Voices = new ObservableCollection<VoiceViewModel>();      
            ChangeLanguageCommand = new RelayCommand(ChangeLanguage);
            ChangeSynchronizeCommand = new RelayCommand(ChangeSynchronizeData);
            ChangeVoiceCommand = new RelayCommand(ChangeVoice);
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
                    await LoadLanguageContent();
            }
        }

        private async void ChangeVoice()
        {
            if(SelectedVoice.VoiceId != CurrentVoice.VoiceId) { 
                await ServiceFactory.ResourceService.SaveValueForKey("ApplicationVoiceId",SelectedVoice.VoiceId);
                CurrentVoice = SelectedVoice;
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
            langs.Data.ToList().ForEach(x =>
            {
                Languages.Add(x);
            });
        }

        private async void LoadContent()
        {            
            LoadLanguages();
            await LoadLanguageContent();
            await LoadApplicationSettings();
            LoadVoices();
        }

        private async Task LoadApplicationSettings()
        {
            SettingsViewModel settings = await ServiceFactory.ResourceService.GetAppSettings();
            IsDataSynchronize = settings.SynchronizeData;
            CurrentLanguage = ServiceFactory.LanguageService.GetLocalLanguageByShortName(settings.ApplicationLanguage);
            SelectedLanguage = CurrentLanguage;            
            CurrentVoice = LoadVoice(settings.ApplicationVoiceId);
            SelectedVoice = CurrentVoice;
        }

        private VoiceViewModel LoadVoice(string voiceId)
        {
            var voices = SpeechSynthesizer.AllVoices;
            var voice = voices.FirstOrDefault(x => x.Id == voiceId);
            return VoiceExists(voice) ? new VoiceViewModel { Voice = voice } : new VoiceViewModel { Voice = GetFirstVoiceByLanguage(SelectedLanguage.LanguageShortName) };
        }
        private VoiceInformation GetFirstVoiceByLanguage(string languageShortName)
        {
            return SpeechSynthesizer.AllVoices.FirstOrDefault(x => x.Language.Contains(languageShortName));
        }
        private bool VoiceExists(VoiceInformation voice)
        {
            return voice != null;
        }

        private void LoadVoices()
        {
            SpeechSynthesizer.AllVoices.ToList().ForEach(x =>
            {
                Voices.Add(new VoiceViewModel {Voice = x});
            });
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
