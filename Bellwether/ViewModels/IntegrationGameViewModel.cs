using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellwether.Services.Utility;

namespace Bellwether.ViewModels
{
    public class IntegrationGameViewModel:ViewModel
    {
        private Models.ViewModels.IntegrationGameViewModel _selectedIntegrationGame;
        public Models.ViewModels.IntegrationGameViewModel SelectedIntegrationGame
        {
            get { return _selectedIntegrationGame; }
            set
            {
                _selectedIntegrationGame = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Models.ViewModels.IntegrationGameViewModel> IntegrationGames { get; set; }

        public IntegrationGameViewModel()
        {
            LoadContent();
        }

        private void LoadContent()
        {
            var integrationGames =
                ServiceExecutor.Execute(() => ServiceFactory.IntegrationGameService.GetIntegrationGames());
            if(integrationGames.IsValid)
            IntegrationGames = new ObservableCollection<Models.ViewModels.IntegrationGameViewModel>(integrationGames.Data);
        }

    }
}
