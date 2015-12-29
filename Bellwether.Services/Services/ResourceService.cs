using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.Data.Json;
using Windows.Globalization;
using Windows.Storage;
using Bellwether.Models.Models;
using Bellwether.Repositories.Repositories;
using Newtonsoft.Json;

namespace Bellwether.Services.Services
{
    public static class ResourcesFiles
    {
        public const string StaticApplicationResourcesFile = "ms-appx:///Bellwether.Resources/StaticResources/ApplicationResources.json";
        public const string StaticLanguageResourcesFile = "ms-appx:///Bellwether.Resources/StaticResources/LanguageResources.json";
        public const string LocalApplicationResourcesFile = "ApplicationResources.json";
        public const string LocalLanguageResourcesFile = "LanguageResources.json";
        public const string LocalResourcesFolderName = "Resources";
    }

    public interface IResourceService
    {
        Task<bool> ChangeLanguage(Dictionary<string, string> languageFile, BellwetherLanguage language);
        Task<string> GetApplicationVersion();
        Task<string> GetApplicationLanguage();
        Task<string> GetLanguageResourceVersion();
        Task<string> GetAvailableLanguagesApiUrl();
        Task<string> GetLanguageFileApiUrl();
        Task<string> GetLanguageApiUrl();
    }
    public class ResourceService : IResourceService
    {
        //WEDŁUG MOJEGO PLANU Z TEGO MIEJSCA MAJA BYC POBIERANE JESZCZE SCENARIUSZE DLA WIDOKÓW ORAZ SYNCHRONIZACJA + JEJ USTAWIENIE + LEKTOR + JEGO USTAWIENEI
        private readonly ILocalResourceRepository _repository;
        private readonly string _appLocalResources;
        private readonly string _langLocalResources;
        private readonly string _localResourcesFile;
        public ResourceService()
        {
            _repository = new LocalResourceRepository();
            _appLocalResources = ResourcesFiles.LocalApplicationResourcesFile;
            _langLocalResources = ResourcesFiles.LocalLanguageResourcesFile;
            _localResourcesFile = ResourcesFiles.LocalResourcesFolderName;
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
            catch (Exception)
            {
                //tu bedzie logowanie
            }
            return operationCompleted;
        }

        public async Task<string> GetApplicationVersion()
        {
            return await TakeKeyValueFromLocalAppResources("ApplicationVersion");
        }

        public async Task<string> GetApplicationLanguage()
        {
            return await TakeKeyValueFromLocalAppResources("ApplicationLanguage");
        }

        public async Task<string> GetLanguageResourceVersion()
        {
            return await TakeKeyValueFromLocalAppResources("LanguageResourceVersion");
        }

        public async Task<string> GetAvailableLanguagesApiUrl()
        {
            return await TakeKeyValueFromLocalAppResources("GetAvailableLanguagesApiUrl");
        }

        public async Task<string> GetLanguageFileApiUrl()
        {
            return await TakeKeyValueFromLocalAppResources("GetLanguageFileApiUrl");
        }

        public async Task<string> GetLanguageApiUrl()
        {
            return await TakeKeyValueFromLocalAppResources("GetLanguageApiUrl");
        }

        private async Task<string> TakeKeyValueFromLocalAppResources(string key)
        {
            await InitRepositoryForAppResources();
            return await _repository.GetValueForKey(key);
        }
        private async Task<string> TakeKeyValueFromLocalLangResources(string key)
        {
            await InitRepositoryForLangResources();
            return await _repository.GetValueForKey(key);
        }
        private async Task<bool> SaveLanguageInAppResources(BellwetherLanguage language)
        {
            bool operationCompleted = false;
            try
            {
                await InitRepositoryForAppResources();
                await _repository.SaveValueForKey("ApplicationLanguage", language.LanguageShortName);
                await _repository.SaveValueForKey("LanguageResourceVersion", language.LanguageVersion.ToString(CultureInfo.InvariantCulture));
                operationCompleted = true;
            }
            catch (Exception)
            {
                //tu bedzie logowanie
            }
            return operationCompleted;
        }
        private async Task<bool> SaveLanguageFile(Dictionary<string, string> languageFile)
        {
            bool operationCompleted = false;
            try
            {
                await InitRepositoryForLangResources();
                await _repository.SaveValuesAndKays(languageFile);
                operationCompleted = true;
            }
            catch (Exception)
            {
                //tu bedzie logowanie
            }
            return operationCompleted;
        }
        private async Task InitRepositoryForAppResources()
        {
            _repository.SpecifyTargetFile(_appLocalResources, _localResourcesFile);
            await _repository.Init();
        }
        private async Task InitRepositoryForLangResources()
        {
            _repository.SpecifyTargetFile(_langLocalResources, _localResourcesFile);
            await _repository.Init();
        }
    }
}
