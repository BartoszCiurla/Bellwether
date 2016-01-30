using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellwether.Services.Utility;

namespace Bellwether.ViewModels
{
    public class HomePageViewModel:ViewModel
    {
        public HomePageViewModel()
        {
            LoadLanguageContent();
        }
        private async void LoadLanguageContent()
        {
            var contentKey = new[] {"IntegrationGamesContent","JokesContent", "IntegrationGamesHeader", "JokesHeader" };
            var contentDictionary = await ServiceFactory.ResourceService.GetLanguageContentForKeys(contentKey);
            TextIntegrationGames = contentDictionary["IntegrationGamesContent"];
            TextJokes = contentDictionary["JokesContent"];
            TextIntegrationGamesHeader = contentDictionary["IntegrationGamesHeader"];
            TextJokesHeader = contentDictionary["JokesHeader"];
        }
        private string _textIntegrationGames;
        public string TextIntegrationGames
        {
            get { return _textIntegrationGames; }
            set { _textIntegrationGames = value;NotifyPropertyChanged(); }
        }

        private string _textJokes;
        public string TextJokes
        {
            get { return _textJokes; }
            set { _textJokes = value;NotifyPropertyChanged(); }
        }

        private string _textIntegrationGamesHeader;
        public string TextIntegrationGamesHeader
        {
            get { return _textIntegrationGamesHeader; }
            set { _textIntegrationGamesHeader = value;NotifyPropertyChanged(); }
        }

        private string _textJokesHeader;

        public string TextJokesHeader
        {
            get { return _textJokesHeader; }
            set { _textJokesHeader = value;NotifyPropertyChanged(); }
        }
    }
}
