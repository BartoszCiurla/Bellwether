using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.VersionService
{
    public interface IVersionValidateService
    {
        Task<bool> ValidateVersion();
    }
    public class VersionValidateService : IVersionValidateService
    {
        public async Task<bool> ValidateVersion()
        {
            await ValidateAvailableLanguages();
            BellwetherLanguage clientLanguage =
                ServiceFactory.LanguageService.GetLocalLanguageByShortName(
                    await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("ApplicationLanguage"));
            ClientVersionViewModel mandatoryVersion = await ServiceFactory.WebBellwetherVersionService.GetVersionForLanguage(clientLanguage.Id);
            if (mandatoryVersion == null)
                return false;
            await ValidateLanguageVersion(mandatoryVersion);
            await ValidateJokeCategoryVersion(mandatoryVersion);
            await ValidateJokeVersion(mandatoryVersion);
            await ValidateGameFeatureVersion(mandatoryVersion);
            await ValidateIntegrationGameVersion(mandatoryVersion);
            DeleteOtherLanguageData(mandatoryVersion);
            return true;
        }

        private void DeleteOtherLanguageData(ClientVersionViewModel mandatoryVersion)
        {
            var jokes = RepositoryFactory.Context.Jokes.Where(x => x.Language.Id != mandatoryVersion.Language.Id);
            RepositoryFactory.Context.Jokes.RemoveRange(jokes);
            var jokeCategories =
                RepositoryFactory.Context.JokeCategories.Where(x => x.Language.Id != mandatoryVersion.Language.Id);
            RepositoryFactory.Context.JokeCategories.RemoveRange(jokeCategories);
            var integrationGames =
                RepositoryFactory.Context.IntegrationGames.Where(x => x.Language.Id != mandatoryVersion.Language.Id).ToList();
            RepositoryFactory.Context.IntegrationGames.RemoveRange(integrationGames);                           
            RepositoryFactory.Context.SaveChanges();
        }
        private async Task<bool> ValidateIntegrationGameVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (ValidateVersion(
                    await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("IntegrationGameVersion"),
                    mandatoryVersion.IntegrationGameVersion))
                return await ServiceFactory.VersionManagementService.UpdateIntegrationGameVersion(mandatoryVersion);
            return false;
        }

        private async Task<bool> ValidateGameFeatureVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (ValidateVersion(
                    await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("GameFeatureVersion"),
                    mandatoryVersion.GameFeatureVersion))
                return await ServiceFactory.VersionManagementService.UpdateGameFeatureVersion(mandatoryVersion);
            return false;
        }

        private async Task<bool> ValidateLanguageVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (ValidateVersion(await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("LanguageVersion"),
                mandatoryVersion.LanguageVersion))
                return await ServiceFactory.VersionManagementService.UpdateLanguageVersion(mandatoryVersion);
            return false;
        }

        private async Task<bool> ValidateJokeVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (ValidateVersion(await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("JokeVersion"),
                mandatoryVersion.JokeVersion))
                return await ServiceFactory.VersionManagementService.UpdateJokeVersion(mandatoryVersion);
            return false;
        }

        private async Task<bool> ValidateJokeCategoryVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (ValidateVersion(await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("JokeCategoryVersion"), mandatoryVersion.JokeCategoryVersion))
                return await ServiceFactory.VersionManagementService.UpdateJokeCategoryVersion(mandatoryVersion);
            return false;
        }

        private async Task<bool> ValidateAvailableLanguages()
        {
            ServiceFactory.LanguageService.ValidateAndFillLanguages(await ServiceFactory.WebBellwetherLanguageService.GetLanguages());
            return true;
        }

        private bool ValidateVersion(object appVersion, object mandatoryVersion)
        {
            return Convert.ToDouble(appVersion) < Convert.ToDouble(mandatoryVersion);
        }

    }
}
