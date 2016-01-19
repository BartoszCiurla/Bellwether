using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Bellwether.Commands;
using Bellwether.Models.Models;
using Bellwether.Services.Utility;

namespace Bellwether.ViewModels
{
    public class JokeViewModel:ViewModel
    {
        public ObservableCollection<Models.ViewModels.JokeViewModel> Jokes { get; set; }
        private Models.ViewModels.JokeViewModel _selectedJoke;

        public Models.ViewModels.JokeViewModel SelectedJoke
        {
            get { return _selectedJoke; }
            set
            {
                _selectedJoke = value;
                Read();
                NotifyPropertyChanged();
            }
        }

        private RelayCommand _selectedJokeCommand;
        public RelayCommand SelectedJokeCommand
        {
            get
            {
                return this._selectedJokeCommand ?? (this._selectedJokeCommand = new RelayCommand(() =>
                {

                }));
            }
        }
        public RelayCommand SpeakCommand { get; set;}
        public JokeViewModel()
        {   
            SpeakCommand = new RelayCommand(Read);  
            LoadContent();
        }

        private void LoadContent()
        {
            var jokes = ServiceExecutor.Execute(() => ServiceFactory.JokeService.GetJokes());
            Jokes = new ObservableCollection<Models.ViewModels.JokeViewModel>(jokes.Data);         
        }

        private async void Read()
        {
            await ServiceExecutor.ExecuteAsync(() => ServiceFactory.SpeechSyntesizerService.Read(this.SelectedJoke.JokeContent));
        }
    }
}
