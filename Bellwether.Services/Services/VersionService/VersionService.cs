//using System;
//using System.Threading.Tasks;
//using Bellwether.Models.Models;
//using Bellwether.Models.ViewModels;
//using BellwetherLanguage = Bellwether.Models.Models.BellwetherLanguage;

//namespace Bellwether.Services.Services.Version
//{
//    public interface IVersionService
//    {
//        Task VerifyVersion();
//    }
//    public class VersionService : IVersionService
//    {
//        private AppSettings _appSettings;
//        private ClientVersionViewModel _appVersion;
//        private AppApiUrl _appApiUrl;
//        private ClientVersionViewModel _mandatoryVersion;
//        private BellwetherLanguage _currentLanguage;

//        public async Task VerifyVersion()
//        {
//            bool resourcesPrepared = await PrepareResources();
//            if (resourcesPrepared)
//            {
//                await UpdateLanguageVersion();
//                await UpdateJokeCategoryVersion();
//                await UpdateJokeVersion();
//                Dispose();
//            }
//        }

//        private void Dispose()
//        {
//            _appSettings = null;
//            _appVersion = null;
//            _appApiUrl = null;
//            _mandatoryVersion = null;
//            _currentLanguage = null;
//        }
//        private async Task<bool> PrepareResources()
//        {
//            _appSettings = await _resourceService.GetApplicationSettings();
//            if (!_appSettings.SynchronizeData)
//                return false;
//            _appVersion = await _resourceService.GetApplicationVersion();
//            _appApiUrl = await _resourceService.GetApplicationApiUrl();
//            bool availableLanguagesChecked = await VerifyAvailableLanguages(_appApiUrl.GetAvailableLanguages);
//            if (!availableLanguagesChecked)
//                return false;
//            var languageDao = _languageService.GetLocalLanguages()
//               .FirstOrDefault(x => x.LanguageShortName == _appSettings.AppLanguage);
//            if (languageDao == null)
//                return false;
//            //tutaj testuje nowości 
//            //var result = await ServiceFactory.WebBellwetherGameFeatureService.GetGameFeatures(_appApiUrl.GetIntegrationGamesFeatures + languageDao.Id);
//            //bool result1 = RepositoryFactory.GameFeatureRepository.CheckAndFillGameFeatures(result);
//            //var result3 = RepositoryFactory.GameFeatureRepository.GetGameFeatures();
//            //ZAPIS CECH GIER DZIALA ...

//            //tutaj testuje nowości 

//            _currentLanguage = new BellwetherLanguage { Id = languageDao.Id, LanguageShortName = languageDao.LanguageShortName, LanguageName = languageDao.LanguageName };
//            _mandatoryVersion = await _webBellwetherVersionService.GetVersionForLanguage(_appApiUrl.GetVersion + _currentLanguage.Id);
//            return true;
//        }

//        private async Task UpdateJokeVersion()
//        {
//            if (Convert.ToDouble(_appVersion.JokeVersion) < Convert.ToDouble(_mandatoryVersion.JokeVersion))
//            {
//                bool checkAndFillJokeResult =
//                    await _jokeService.CheckAndFillJokes(_appApiUrl.GetJokes + _currentLanguage.Id);
//                if (checkAndFillJokeResult)
//                {
//                    await _resourceService.ChangeJokeVersion(_mandatoryVersion.JokeVersion);
//                }
//            }
//        }
//        private async Task UpdateJokeCategoryVersion()
//        {
//            if (Convert.ToDouble(_appVersion.JokeCategoryVersion) <
//                Convert.ToDouble(_mandatoryVersion.JokeCategoryVersion))
//            {
//                bool checkAndFillJokeCategoriesResult =
//                    await _jokeService.CheckAndFillJokeCategories(_appApiUrl.GetJokeCategories + _currentLanguage.Id);
//                if (checkAndFillJokeCategoriesResult)
//                {
//                    await _resourceService.ChangeJoke{CategoryVersion(_mandatoryVersion.JokeCategoryVersion);
//                }
//            }
//        }
//        private async Task UpdateLanguageVersion()
//        {
//            if (Convert.ToDouble(_appVersion.LanguageVersion) < Convert.ToDouble(_mandatoryVersion.LanguageVersion))
//            {
//                _currentLanguage.LanguageVersion = Convert.ToDouble(_mandatoryVersion.LanguageVersion);
//                var vesionLanguageFile =
//                    await
//                        _languageService.GetLanguageFile(_appApiUrl.GetLanguageFile + _currentLanguage.Id);
//                if (vesionLanguageFile != null)
//                {
//                    await _resourceService.ChangeLanguage(vesionLanguageFile, _currentLanguage);
//                }
//            }
//        }
//        private async Task<bool> VerifyAvailableLanguages(string getAvailableLanguagesApiUrl)
//        {
//            bool operationCompleted = false;
//            try
//            {
//                operationCompleted = await _languageService.CheckAndFillLanguages(getAvailableLanguagesApiUrl);
//            }
//            catch (Exception)
//            {
//                operationCompleted = false;
//            }
//            return operationCompleted;

//        }
//    }
//}
