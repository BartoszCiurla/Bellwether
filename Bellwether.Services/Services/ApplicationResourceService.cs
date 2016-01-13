using System;
using System.Threading.Tasks;
using Bellwether.Models.Models;
using Bellwether.Models.Scenarios;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services
{
    public interface IApplicationResourceService
    {
        Task<AppSettings> GetApplicationSettings();
        Task<ClientVersionViewModel> GetApplicationVersion();
        Task<AppApiUrl> GetApplicationApiUrl();
    }
    public class ApplicationResourceService: IApplicationResourceService
    {

        public async Task<ClientVersionViewModel> GetApplicationVersion()
        {
            var resource = await RepositoryFactory.ApplicationResourceRepository.GetSelectedKeysValues(AppScenario.ApplicationVersion);
            return new ClientVersionViewModel
            {
                ApplicationVersion = resource["ApplicationVersion"],
                LanguageVersion = resource["LanguageVersion"],
                IntegrationGameVersion = resource["IntegrationGameVersion"],
                JokeCategoryVersion = resource["JokeCategoryVersion"],
                JokeVersion = resource["JokeVersion"]
            };
        }

        public async Task<AppApiUrl> GetApplicationApiUrl()
        {
            var resource = await RepositoryFactory.ApplicationResourceRepository.GetSelectedKeysValues(AppScenario.ApiUrlScenario);
            return new AppApiUrl
            {
                GetVersion = resource["GetVersion"],
                GetAvailableLanguages = resource["GetAvailableLanguages"],
                GetLanguageFile = resource["GetLanguageFile"],
                GetLanguage = resource["GetLanguage"],
                GetJokeCategories = resource["GetJokeCategories"],
                GetJokes = resource["GetJokes"],
                GetIntegrationGames = resource["GetIntegrationGames"],
                GetIntegrationGamesFeatures = resource["GetIntegrationGamesFeatures"]
            };
        }
        public async Task<AppSettings> GetApplicationSettings()
        {
            var resources = await RepositoryFactory.ApplicationResourceRepository.GetSelectedKeysValues(AppScenario.SettingsScenario);
            return new AppSettings { AppLanguage = resources["ApplicationLanguage"], SynchronizeData = Convert.ToBoolean(resources["SynchronizeData"]) };
        }
    }
}
