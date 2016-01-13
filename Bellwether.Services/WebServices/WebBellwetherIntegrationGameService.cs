using System.Threading.Tasks;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;
using Newtonsoft.Json;

namespace Bellwether.Services.WebServices
{
    public interface IWebBellwetherIntegrationGameService
    {
        Task<GameFeatureViewModel[]> GetGameFeatures(int languageId);
    }
    public class WebBellwetherIntegrationGameService: IWebBellwetherIntegrationGameService
    {
        public async Task<GameFeatureViewModel[]> GetGameFeatures(int languageId)
        {
            var stringContent = await RequestExecutor.CreateRequestGetWithUriParam(await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("GetIntegrationGamesFeatures") + languageId);
            var gameFeatures = JsonConvert.DeserializeObject<ResponseViewModel<GameFeatureViewModel[]>>(stringContent);
            return gameFeatures.Data;

        }
    }
}
