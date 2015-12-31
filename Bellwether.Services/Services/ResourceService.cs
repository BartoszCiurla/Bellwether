using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Windows.Storage;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.Repositories.Repositories;

namespace Bellwether.Services.Services
{
    public interface IResourceService
    {
        Task<bool> ChangeLanguage(Dictionary<string, string> languageFile, BellwetherLanguage language);
        Task<string> GetApplicationVersion();
        Task<AppLanguageSettingsViewModel> GetApplicationLanguageSettings();
        Task<Dictionary<string, string>> GetLanguageContentScenario(IEnumerable<string> requiredKeysForScenario);
    }
    public class ResourceService : IResourceService
    {
        //WEDŁUG MOJEGO PLANU Z TEGO MIEJSCA MAJA BYC POBIERANE JESZCZE SCENARIUSZE DLA WIDOKÓW ORAZ SYNCHRONIZACJA + JEJ USTAWIENIE + LEKTOR + JEGO USTAWIENEI
        private readonly IResourceRepository _appResourceRepository;
        private readonly IResourceRepository _langResourceRepository;

        public ResourceService()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            _appResourceRepository = new ResourceRepository(ResourcesFiles.LocalApplicationResourcesFile, ResourcesFiles.LocalResourcesFolderName, localFolder);
            _langResourceRepository = new ResourceRepository(ResourcesFiles.LocalLanguageResourcesFile, ResourcesFiles.LocalResourcesFolderName, localFolder);
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

        public async Task<Dictionary<string, string>> GetLanguageContentScenario(IEnumerable<string> requiredKeysForScenario)
        {
            return await _langResourceRepository.GetSelectedKeysValues(requiredKeysForScenario);
        }

        public async Task<AppLanguageSettingsViewModel> GetApplicationLanguageSettings()
        {
            AppLanguageSettingsViewModel settings = new AppLanguageSettingsViewModel();
            var requiredKeys = new List<string>
            {
                "ApplicationLanguage",
                "LanguageResourceVersion",
                "GetAvailableLanguagesApiUrl",
                "GetLanguageFileApiUrl",
                "GetLanguageApiUrl"
            };
           // await _appResourceRepository.Init();
            var localLanguageSettings = await _appResourceRepository.GetSelectedKeysValues(requiredKeys);
            settings.ApplicationLanguage = localLanguageSettings["ApplicationLanguage"];
            settings.LanguageResourceVersion = localLanguageSettings["LanguageResourceVersion"];
            settings.GetAvailableLanguagesApiUrl = localLanguageSettings["GetAvailableLanguagesApiUrl"];
            settings.GetLanguageFileApiUrl = localLanguageSettings["GetLanguageFileApiUrl"];
            settings.GetLanguageApiUrl = localLanguageSettings["GetLanguageApiUrl"];
            return settings;
        }


        public async Task<string> GetApplicationVersion()
        {
            return await TakeKeyValueFromLocalAppResources("ApplicationVersion");
        }

        private async Task<string> TakeKeyValueFromLocalAppResources(string key)
        {
            return await _appResourceRepository.GetValueForKey(key);
        }
        private async Task<bool> SaveLanguageInAppResources(BellwetherLanguage language)
        {
            bool operationCompleted = false;
            Dictionary<string, string> resources = new Dictionary<string, string>();
            try
            {
                resources.Add("ApplicationLanguage", language.LanguageShortName);
                resources.Add("LanguageResourceVersion", language.LanguageVersion.ToString(CultureInfo.InvariantCulture));
                await _appResourceRepository.SaveSelectedValues(resources);
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
