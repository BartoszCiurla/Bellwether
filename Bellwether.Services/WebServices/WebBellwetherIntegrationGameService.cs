using System.Collections.Generic;
using System.Threading.Tasks;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;
using Newtonsoft.Json;

namespace Bellwether.Services.WebServices
{
    public interface IWebBellwetherIntegrationGameService
    {
        Task<List<GameFeatureViewModel>> GetGameFeatures(int languageId);
        Task<List<DirectIntegrationGameViewModel>> GetIntegrationGames(int languageId);
    }
    public class WebBellwetherIntegrationGameService: IWebBellwetherIntegrationGameService
    {
        public async Task<List<GameFeatureViewModel>> GetGameFeatures(int languageId)
        {
            var stringContent = await RequestExecutor.CreateRequestGetWithUriParam(await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("GetIntegrationGamesFeatures") + languageId);
            var gameFeatures = JsonConvert.DeserializeObject<ResponseViewModel<List<GameFeatureViewModel>>>(stringContent);
            return gameFeatures.Data;
        }

        public async Task<List<DirectIntegrationGameViewModel>> GetIntegrationGames(int languageId)
        {
            var stringContent =
                await
                    RequestExecutor.CreateRequestGetWithUriParam(
                        await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("GetIntegrationGames" + languageId));
            var integrationGames =
                JsonConvert.DeserializeObject<ResponseViewModel<List<DirectIntegrationGameViewModel>>>(stringContent);
            return integrationGames.Data;
        }
    }
}
