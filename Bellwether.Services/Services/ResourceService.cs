using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Bellwether.Models.Models;
using Bellwether.Models.Scenarios;
using Bellwether.Repositories.Factories;
using Bellwether.Repositories.Repositories;

namespace Bellwether.Services.Services
{
    public interface IResourceService
    {
        Task<bool> ChangeLanguage(Dictionary<string, string> languageFile, BellwetherLanguage language);
        Task ChangeJokeCategoryVersion(string jokeCategoryVersion);
        Task ChangeJokeVersion(string jokeVersion);
        Task<Dictionary<string, string>> GetLanguageContentScenario(IEnumerable<string> requiredKeysForScenario);
        Task<AppSettings> GetApplicationSettings();
        Task<AppVersion> GetApplicationVersion();
        Task<AppApiUrl> GetApplicationApiUrl();
    }
    public class ResourceService : IResourceService
    {
        //WEDŁUG MOJEGO PLANU Z TEGO MIEJSCA MAJA BYC POBIERANE JESZCZE SCENARIUSZE DLA WIDOKÓW ORAZ SYNCHRONIZACJA + JEJ USTAWIENIE + LEKTOR + JEGO USTAWIENEI
        private readonly IResourceRepository _appResourceRepository;
        private readonly IResourceRepository _langResourceRepository;

        public ResourceService()
        {
            _appResourceRepository = RepositoryFactory.AppResourceRepository;
            _langResourceRepository = RepositoryFactory.LangResourceRepository;
        }

        public async Task<bool> ChangeLanguage(Dictionary<string, string> languageFile, BellwetherLanguage language)
        {
            bool operationCompleted = false;
            try
            {
                operationCompleted = await SaveLanguageInAppResources(language);
                if (operationCompleted)
                    operationCompleted = await SaveLanguageFile(languageFile);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            return operationCompleted;
        }

        public async Task ChangeJokeCategoryVersion(string jokeCategoryVersion)
        {
            try
            {
                await _appResourceRepository.SaveSelectedValues(resources: new Dictionary<string,string> { { "JokeCategoryVersion", jokeCategoryVersion} });
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        public async Task ChangeJokeVersion(string jokeVersion)
        {
            try
            {
                await
                    _appResourceRepository.SaveSelectedValues(resources:
                        new Dictionary<string, string> {{ "JokeVersion", jokeVersion}});
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }
        public async Task<Dictionary<string, string>> GetLanguageContentScenario(IEnumerable<string> requiredKeysForScenario)
        {
            return await _langResourceRepository.GetSelectedKeysValues(requiredKeysForScenario);
        }
        public async Task<AppSettings> GetApplicationSettings()
        {
            var resources = await _appResourceRepository.GetSelectedKeysValues(AppScenario.SettingsScenario);
            return new AppSettings {AppLanguage = resources["ApplicationLanguage"],SynchronizeData = Convert.ToBoolean(resources["SynchronizeData"]) };
        }

        public async Task<AppVersion> GetApplicationVersion()
        {
            var resource = await _appResourceRepository.GetSelectedKeysValues(AppScenario.ApplicationVersion);
            return new AppVersion
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
            var resource = await _appResourceRepository.GetSelectedKeysValues(AppScenario.ApiUrlScenario);
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

        private async Task<bool> SaveLanguageInAppResources(BellwetherLanguage language)
        {
            bool operationCompleted = false;
            try
            {
                await _appResourceRepository.SaveSelectedValues(resources : new Dictionary<string, string>
                {
                    {"ApplicationLanguage", language.LanguageShortName },
                    {"LanguageVersion", language.LanguageVersion.ToString(CultureInfo.InvariantCulture) }
                });
                operationCompleted = true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            return operationCompleted;
        }
        private async Task<bool> SaveLanguageFile(Dictionary<string, string> languageFile)
        {
            bool operationCompleted = false;
            try
            {
                await _langResourceRepository.SaveValuesAndKays(languageFile);
                operationCompleted = true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            return operationCompleted;
        }
    }
}
