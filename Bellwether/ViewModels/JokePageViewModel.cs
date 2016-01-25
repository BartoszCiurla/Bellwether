using System.Collections.ObjectModel;
using System.Linq;
using Bellwether.Commands;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;

namespace Bellwether.ViewModels
{
    public class JokePageViewModel:ViewModel
    {       
        public ObservableCollection<JokeViewModel> Jokes { get; set; }
        private JokeViewModel _selectedJoke;
        public JokeViewModel SelectedJoke
        {
            get { return _selectedJoke; }
            set
            {
                _selectedJoke = value;
                NotifyPropertyChanged();
            }
        }
        private string _speakerStatus;
        public string SpeakerStatus
        {
            get { return _speakerStatus; }
            set { _speakerStatus = value; NotifyPropertyChanged(); }
        }  
        public RelayCommand ReadCommand { get; set; }
        public JokePageViewModel()
        {
            Jokes = new ObservableCollection<JokeViewModel>();
            ReadCommand = new RelayCommand(Read);
            SpeakerStatus = ServiceFactory.SpeechSyntesizerService.GetSpeakerStatus() ? TextStop : TextRead;
            LoadContent();
            LoadLanguageContent();
        }
        public async void Read()
        {
            SpeakerStatus =
                await
                    ServiceFactory.SpeechSyntesizerService.ValidateSpeakerAndSpeak(
                        SelectedJoke.JokeContent)
                    ? TextStop : TextRead;
        }
        private void LoadContent()
        {
            var jokes = ServiceExecutor.Execute(() => ServiceFactory.JokeService.GetJokes());
            if (jokes.IsValid)
            {
                jokes.Data.ToList().ForEach(x =>
                {
                    Jokes.Add(x);
                });
                SelectedJoke = Jokes[0];
            }      
        }

        private async void LoadLanguageContent()
        {
            var contentKey = new[] { "JokesHeader", "JokeCategory", "AvailableJokes", "Stop", "Read" };
            var contentDictionary = await ServiceFactory.ResourceService.GetLanguageContentForKeys(contentKey);
            TextPageTittle = contentDictionary["JokesHeader"];
            TextJokeCategoryName = contentDictionary["JokeCategory"];
            TextAvailableJokes = contentDictionary["AvailableJokes"];
            TextStop = contentDictionary["Stop"];
            TextRead = contentDictionary["Read"];
            SpeakerStatus = TextRead;
        }

        private string _textPageTittle;
        public string TextPageTittle { get { return _textPageTittle; } set { _textPageTittle = value; NotifyPropertyChanged(); } }
        private string _textJokeCategoryName;
        public string TextJokeCategoryName { get { return _textJokeCategoryName; } set { _textJokeCategoryName = value;NotifyPropertyChanged(); } }
        private string _textAvailableJokes;
        public string TextAvailableJokes { get { return _textAvailableJokes; } set { _textAvailableJokes = value;NotifyPropertyChanged(); } }
        private string _textRead;
        public string TextRead { get { return _textRead; } set { _textRead = value;NotifyPropertyChanged(); } }
        private string _textStop;
        public string TextStop { get { return _textStop; } set { _textStop = value;NotifyPropertyChanged(); } }
    }
}
