using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.ResourceService
{
    public interface IResourceService
    {
        Task<SettingsViewModel> GetAppSettings();
        Task<Dictionary<string, string>> GetLanguageContentForKeys(string[] languageKeys);
        Task<bool> SaveValueForKey(string key, string value);
    }
    public class ResourceService:IResourceService
    {
        public async Task<SettingsViewModel> GetAppSettings()
        {
            var settings = await RepositoryFactory.ApplicationResourceRepository.GetSelectedKeysValues(new[] { "SynchronizeData", "ApplicationLanguage", "ApplicationVoiceId"});
            return new SettingsViewModel
            {
                ApplicationLanguage = settings["ApplicationLanguage"],
                SynchronizeData = Convert.ToBoolean(settings["SynchronizeData"]),
                ApplicationVoiceId = settings["ApplicationVoiceId"]
            };
        }

        public async Task<Dictionary<string, string>> GetLanguageContentForKeys(string[] languageKeys)
        {
            return await RepositoryFactory.LanguageResourceRepository.GetSelectedKeysValues(languageKeys);
        }

        public async Task<bool> SaveValueForKey(string key, string value)
        {
            return await RepositoryFactory.ApplicationResourceRepository.SaveValueForKey(key, value);
        } 
    }
}
