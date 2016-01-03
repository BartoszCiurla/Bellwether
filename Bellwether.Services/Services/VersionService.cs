using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bellwether.Models.Entities;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Factories;
using Bellwether.WebServices.WebServices;

namespace Bellwether.Services.Services
{
    public interface IVersionService
    {
        Task VerifyVersion();
    }
    public class VersionService : IVersionService
    {
        private readonly ILanguageService _languageService;
        private readonly IResourceService _resourceService;
        private readonly IWebBellwetherVersionService _webBellwetherVersionService;
        private readonly IJokeService _jokeService;
        public VersionService()
        {
            _languageService = ServiceFactory.LanguageService;
            _resourceService = ServiceFactory.ResourceService;
            _webBellwetherVersionService = ServiceFactory.WebBellwetherVersionService;
            _jokeService = ServiceFactory.JokeService;
        }
        private AppSettings _appSettings;
        private AppVersion _appVersion;
        private AppApiUrl _appApiUrl;
        private AppVersion _mandatoryVersion;
        private BellwetherLanguage _currentLanguage;

        public async Task VerifyVersion()
        {
            bool resourcesPrepared = await PrepareResources();
            if (resourcesPrepared)
            {
                await UpdateLanguageVersion();
                await UpdateJokeCategoryVersion();
                await UpdateJokeVersion();
                Dispose();
            }
        }

        private void Dispose()
        {
            _appSettings = null;
            _appVersion = null;
            _appApiUrl = null;
            _mandatoryVersion = null;
            _currentLanguage = null;
        }
        private async Task<bool> PrepareResources()
        {
            _appSettings = await _resourceService.GetApplicationSettings();
            if (!_appSettings.SynchronizeData)
                return false;
            _appVersion = await _resourceService.GetApplicationVersion();
            _appApiUrl = await _resourceService.GetApplicationApiUrl();
            bool availableLanguagesChecked = await VerifyAvailableLanguages(_appApiUrl.GetAvailableLanguages);
            if (!availableLanguagesChecked)
                return false;
            var languageDao = _languageService.GetLocalLanguages()
               .FirstOrDefault(x => x.LanguageShortName == _appSettings.AppLanguage);
            if (languageDao == null)
                return false;            
            _currentLanguage = new BellwetherLanguage { Id = languageDao.Id, LanguageShortName = languageDao.LanguageShortName, LanguageName = languageDao.LanguageName };
            _mandatoryVersion = await _webBellwetherVersionService.GetVersionForLanguage(_appApiUrl.GetVersion + _currentLanguage.Id);
            return true;
        }

        private async Task UpdateJokeVersion()
        {
            if (Convert.ToDouble(_appVersion.JokeVersion) < Convert.ToDouble(_mandatoryVersion.JokeVersion))
            {
                bool checkAndFillJokeResult =
                    await _jokeService.CheckAndFillJokes(_appApiUrl.GetJokes + _currentLanguage.Id);
                if (checkAndFillJokeResult)
                {
                    await _resourceService.ChangeJokeVersion(_mandatoryVersion.JokeVersion);
                }
            }
        }
        private async Task UpdateJokeCategoryVersion()
        {
            if (Convert.ToDouble(_appVersion.JokeCategoryVersion) <
                Convert.ToDouble(_mandatoryVersion.JokeCategoryVersion))
            {
                bool checkAndFillJokeCategoriesResult =
                    await _jokeService.CheckAndFillJokeCategories(_appApiUrl.GetJokeCategories + _currentLanguage.Id);
                if (checkAndFillJokeCategoriesResult)
                {
                    await _resourceService.ChangeJokeCategoryVersion(_mandatoryVersion.JokeCategoryVersion);
                }
            }
        }
        private async Task UpdateLanguageVersion()
        {
            if (Convert.ToDouble(_appVersion.LanguageVersion) < Convert.ToDouble(_mandatoryVersion.LanguageVersion))
            {
                _currentLanguage.LanguageVersion = Convert.ToDouble(_mandatoryVersion.LanguageVersion);
                var vesionLanguageFile =
                    await
                        _languageService.GetLanguageFile(_appApiUrl.GetLanguageFile + _currentLanguage.Id);
                if (vesionLanguageFile != null)
                {
                    await _resourceService.ChangeLanguage(vesionLanguageFile, _currentLanguage);
                }
            }
        }
        private async Task<bool> VerifyAvailableLanguages(string getAvailableLanguagesApiUrl)
        {
            bool operationCompleted = false;
            try
            {
                operationCompleted = await _languageService.CheckAndFillLanguages(getAvailableLanguagesApiUrl);
            }
            catch (Exception)
            {
                operationCompleted = false;
            }
            return operationCompleted;

        }
    }
}
