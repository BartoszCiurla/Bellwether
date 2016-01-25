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
            var contentKey = new[] {"IntegrationgGamesContent","JokesContent"};
            var contentDictionary = await ServiceFactory.ResourceService.GetLanguageContentForKeys(contentKey);
            TextIntegrationGames = contentDictionary["IntegrationgGamesContent"];
            TextJokes = contentDictionary["JokesContent"];
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
    }
}
