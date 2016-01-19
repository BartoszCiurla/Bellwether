using System.Collections.Generic;
using System.Linq;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.IntegrationGameService
{
    public interface IIntegrationGameService
    {
        IntegrationGameViewModel[] GetIntegrationGames();
    }
    public class IntegrationGameService:IIntegrationGameService
    {
        public IntegrationGameViewModel[] GetIntegrationGames()
        {
            var games = new List<IntegrationGameViewModel>();
            RepositoryFactory.Context.IntegrationGames.ToList().ForEach(x =>
            {
                //var gameFeatureDetailsName = GetGameFeatureDetailName(x.Id).ToArray();
                games.Add(new IntegrationGameViewModel
                {
                    Id = x.Id,
                    GameName = x.IntegrationGameName,
                    GameDescription = x.IntegrationGameDescription,
                    //CategoryGame = gameFeatureDetailsName[0],
                    //PaceOfPlay = gameFeatureDetailsName[1],
                    //NumberOfPlayer = gameFeatureDetailsName[2],
                    //PreparationFun = gameFeatureDetailsName[3]
                });
            });
            return games.ToArray();
        }
        private IEnumerable<string> GetGameFeatureDetailName(int integrationGameId)
        {
            return
                RepositoryFactory.Context.IntegrationGameFeatures.ToList().Where(
                    x => x.IntegrationGame.Id == integrationGameId)
                    .OrderBy(x => x.GameFeature.Id)
                    .Select(x => x.GameFeatureDetail.GameFeatureDetailName);
        }
    }
}
