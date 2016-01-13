using System;
using System.Threading.Tasks;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.Version
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
            //await ValidateGameFeatureVersion(mandatoryVersion);
            return true;
        }

        private async Task<bool> ValidateGameFeatureVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (
                ValidateVersion(
                    await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("GetIntegrationGamesFeatures"),
                    mandatoryVersion.IntegrationGamesFeatureVersion))
                return await UpdateGameFeatureVersion(mandatoryVersion);
            return false;
        } 

        private async Task<bool> ValidateLanguageVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (ValidateVersion(await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("LanguageVersion"),
                mandatoryVersion.LanguageVersion))
                return await UpdateLanguageVersion(mandatoryVersion);
            return false;
        } 

        private async Task<bool> ValidateJokeVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (ValidateVersion(await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("JokeVersion"),
                mandatoryVersion.JokeVersion))
                return await UpdateJokeVersion(mandatoryVersion);
            return false;
        }

        private async Task<bool> ValidateAvailableLanguages()
        {
            ServiceFactory.LanguageService.ValidateAndFillLanguages(await ServiceFactory.WebBellwetherLanguageService.GetLanguages());
            return true;
        }

        private async Task<bool> ValidateJokeCategoryVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (ValidateVersion(await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("JokeCategoryVersion"), mandatoryVersion.JokeCategoryVersion))
                return await UpdateJokeCategoryVersion(mandatoryVersion);
            return false;
        }

        private async Task<bool> UpdateGameFeatureVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (ServiceFactory.GameFeatureManagementService.ValidateAndFillGameFeatures(
                        await
                            ServiceFactory.WebBellwetherIntegrationGameService.GetGameFeatures(mandatoryVersion.Language.Id)))
                return false;
            return true;
        }

        private async Task<bool> UpdateLanguageVersion(ClientVersionViewModel mandatoryVersion)
        {
            if(await FillLanguageFile(mandatoryVersion))             
                return await ChangeLanguageVersion(mandatoryVersion.LanguageVersion);
            return false;
        }

        private async Task<bool> FillLanguageFile(ClientVersionViewModel mandatoryVersion)
        {
            var languageFile =
               await ServiceFactory.WebBellwetherLanguageService.GetLanguageFile(mandatoryVersion.Language.Id);
            if (languageFile == null)
                return false;
            return await RepositoryFactory.LanguageResourceRepository.SaveValuesAndKays(languageFile);
        } 

        private async Task<bool> UpdateJokeVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (ServiceFactory.JokeManagementService.ValidateAndFillJokes(
                await ServiceFactory.WebBellwetherJokeService.GetJokes(mandatoryVersion.Language.Id)))
                return await ChangeJokeVersion(mandatoryVersion.JokeVersion);
            return true;
        }

        private async Task<bool> UpdateJokeCategoryVersion(ClientVersionViewModel mandatoryVersion)
        {
            if (
                ServiceFactory.JokeCategoryManagementService.ValidateAndFillJokeCategories(
                    await ServiceFactory.WebBellwetherJokeService.GetJokeCategories(mandatoryVersion.Language.Id)))
                return await ChangeJokeCategoryVersion(mandatoryVersion.JokeCategoryVersion);
            return true;
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

        private bool ValidateVersion(object appVersion, object mandatoryVersion)
        {
            return Convert.ToDouble(appVersion) <= Convert.ToDouble(mandatoryVersion);
        }
    }
}
