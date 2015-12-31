using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;

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

        public VersionService(ILanguageService languageService, IResourceService resourceService)
        {
            _languageService = languageService;
            _resourceService = resourceService;
        }

        public async Task VerifyVersion()
        {
            await UpdateLanguageVersion();
        }

        private async Task UpdateLanguageVersion()
        {
            AppLanguageSettingsViewModel languageSettings = await _resourceService.GetApplicationLanguageSettings();
            bool availableLanguagesChecked = await VerifyAvailableLanguages(languageSettings.GetAvailableLanguagesApiUrl);
            if (availableLanguagesChecked)
            {
                BellwetherLanguage newLanguageVersion = await VerifyLanguageVersion(languageSettings);
                if (newLanguageVersion != null)
                {
                    var vesionLanguageFile =
                        await
                            _languageService.GetLanguageFile(languageSettings.GetLanguageFileApiUrl + newLanguageVersion.Id);
                    if (vesionLanguageFile != null)
                    {
                        await _resourceService.ChangeLanguage(vesionLanguageFile, newLanguageVersion);
                    }
                }
            }
        }

        private async Task<bool> VerifyAvailableLanguages(string getAvailableLanguagesApiUrl)
        {
            bool operationCompleted = false;
            try
            {
                var availableLanguages = await _languageService.GetLanguages(getAvailableLanguagesApiUrl);
                _languageService.CheckAndFillLanguages(availableLanguages);
                operationCompleted = true;
            }
            catch (Exception)
            {
                operationCompleted = false;
            }
            return operationCompleted;

        }

        private async Task<BellwetherLanguage> VerifyLanguageVersion(AppLanguageSettingsViewModel languageSettings)
        {
            try
            {
                string currentLangShortName = languageSettings.ApplicationLanguage;
                string currentLanguageVersion = languageSettings.LanguageResourceVersion;
                int languageId =
                    _languageService.GetLocalLanguages()
                        .ToList()
                        .FirstOrDefault(x => x.LanguageShortName == currentLangShortName)
                        .Id;
                var newLanguageVersion = await _languageService.GetLanguage(languageSettings.GetLanguageApiUrl + languageId);
                if (Convert.ToDouble(currentLanguageVersion) < newLanguageVersion.LanguageVersion)
                    return newLanguageVersion;
                return null;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Wyjebałem się w VersionService " + exception);
                return null;
            }
        }
    }
}
