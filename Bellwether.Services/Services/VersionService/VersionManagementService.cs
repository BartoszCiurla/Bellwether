using System.Threading.Tasks;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.VersionService
{
    public interface IVersionManagementService
    {
        Task<bool> UpdateIntegrationGameVersion(ClientVersionViewModel mandatoryVersion);
        Task<bool> UpdateGameFeatureVersion(ClientVersionViewModel mandatoryVersion);
        Task<bool> UpdateLanguageVersion(ClientVersionViewModel mandatoryVersion);
        Task<bool> UpdateJokeVersion(ClientVersionViewModel mandatoryVersion);
        Task<bool> UpdateJokeCategoryVersion(ClientVersionViewModel mandatoryVersion);
    }
    public class VersionManagementService:IVersionManagementService
    {
        public async Task<bool> UpdateIntegrationGameVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (ServiceFactory.IntegrationGameManagementService.ValidateAndFillIntegrationGames(
                await
                    ServiceFactory.WebBellwetherIntegrationGameService.GetIntegrationGames(
                        mandatoryVersion.Language.Id)))
                return await ChangeIntegrationGameVersion(mandatoryVersion.IntegrationGameVersion);
            return true;
        }

        public async Task<bool> UpdateGameFeatureVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (ServiceFactory.GameFeatureManagementService.ValidateAndFillGameFeatures(
                await
                    ServiceFactory.WebBellwetherIntegrationGameService.GetGameFeatures(mandatoryVersion.Language.Id)))
                return await ChangeGameFeatureVersion(mandatoryVersion.GameFeatureVersion);
            return true;
        }

        public async Task<bool> UpdateLanguageVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (await FillLanguageFile(mandatoryVersion))
                return await ChangeLanguageVersion(mandatoryVersion.LanguageVersion);
            return false;
        }

        public async Task<bool> UpdateJokeVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (ServiceFactory.JokeManagementService.ValidateAndFillJokes(
                await ServiceFactory.WebBellwetherJokeService.GetJokes(mandatoryVersion.Language.Id)))
                return await ChangeJokeVersion(mandatoryVersion.JokeVersion);
            return true;
        }

       public async Task<bool> UpdateJokeCategoryVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (
                ServiceFactory.JokeCategoryManagementService.ValidateAndFillJokeCategories(
                    await ServiceFactory.WebBellwetherJokeService.GetJokeCategories(mandatoryVersion.Language.Id)))
                return await ChangeJokeCategoryVersion(mandatoryVersion.JokeCategoryVersion);
            return true;
        }

        private async Task<bool> FillLanguageFile(ClientVersionViewModel mandatoryVersion)
        {
            var languageFile =
               await ServiceFactory.WebBellwetherLanguageService.GetLanguageFile(mandatoryVersion.Language.Id);
            if (languageFile == null)
                return false;
            return await RepositoryFactory.LanguageResourceRepository.SaveValuesAndKays(languageFile);
        }

        private async Task<bool> ChangeGameFeatureVersion(string gameFeatureMandatoryVersion)
        {
            return
                await
                    RepositoryFactory.ApplicationResourceRepository.SaveValueForKey("GameFeatureVersion", gameFeatureMandatoryVersion);
        }

        private async Task<bool> ChangeIntegrationGameVersion(string integrationGameMandatoryVersion)
        {
            return
                await
                    RepositoryFactory.ApplicationResourceRepository.SaveValueForKey("IntegrationGameVersion", integrationGameMandatoryVersion);
        }

        private async Task<bool> ChangeJokeVersion(string jokeMandatoryVersion)
        {
            return await RepositoryFactory.ApplicationResourceRepository.SaveValueForKey("JokeVersion", jokeMandatoryVersion);
        }

        private async Task<bool> ChangeJokeCategoryVersion(string jokeCategoryMandatoryVersion)
        {
            return await RepositoryFactory.ApplicationResourceRepository.SaveValueForKey("JokeCategoryVersion", jokeCategoryMandatoryVersion);
        }

        private async Task<bool> ChangeLanguageVersion(string languageMandatoryVersion)
        {
            return await RepositoryFactory.ApplicationResourceRepository.SaveValueForKey("LanguageVersion", languageMandatoryVersion);
        }
    }
}
