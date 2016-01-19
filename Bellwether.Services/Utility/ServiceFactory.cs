using Bellwether.Services.Services.IntegrationGameService;
using Bellwether.Services.Services.JokeService;
using Bellwether.Services.Services.LanguageService;
using Bellwether.Services.Services.ResourceService;
using Bellwether.Services.Services.SpeechSynthesizer;
using Bellwether.Services.Services.VersionService;
using Bellwether.Services.WebServices;

namespace Bellwether.Services.Utility
{
    public static class ServiceFactory
    {
        private static IInitResourceService _initResourceService;
        public static IInitResourceService InitResourceService
            => _initResourceService ?? (_initResourceService = new InitiateResourceService());

        private static IResourceService _resourceService;
        public static IResourceService ResourceService => _resourceService ?? (_resourceService = new ResourceService());

        private static IVersionValidateService _versionValidateService;
        public static IVersionValidateService VersionValidateService
            => _versionValidateService ?? (_versionValidateService = new VersionValidateService());

        private static IVersionManagementService _versionManagementService;
        public static IVersionManagementService VersionManagementService
            => _versionManagementService ?? (_versionManagementService = new VersionManagementService());

        private static ILanguageService _languageService;
        public static ILanguageService LanguageService => _languageService ?? (_languageService = new LanguageService());

        private static ILanguageManagementService _languageManagementService;
        public static ILanguageManagementService LanguageManagementService
            => _languageManagementService ?? (_languageManagementService = new LanguageManagementService());

        private static IWebBellwetherLanguageService _webBellwetherLanguageService;
        public static IWebBellwetherLanguageService WebBellwetherLanguageService
            => _webBellwetherLanguageService ?? (_webBellwetherLanguageService = new WebBellwetherLanguageService());

        private static IWebBellwetherVersionService _webBellwetherVersionService;
        public static IWebBellwetherVersionService WebBellwetherVersionService
            => _webBellwetherVersionService ?? (_webBellwetherVersionService = new WebBellwetherVersionService());

        private static IWebBellwetherJokeService _webBellwetherJokeService;
        public static IWebBellwetherJokeService WebBellwetherJokeService
            => _webBellwetherJokeService ?? (_webBellwetherJokeService = new WebBellwetherJokeService());

        private static IJokeCategoryManagementService _jokeCategoryManagementService;
        public static IJokeCategoryManagementService JokeCategoryManagementService
            => _jokeCategoryManagementService ?? (_jokeCategoryManagementService = new JokeCategoryManagementService());

        private static IJokeManagementService _jokeManagementService;
        public static IJokeManagementService JokeManagementService
            => _jokeManagementService ?? (_jokeManagementService = new JokeManagementService());

        private static IJokeService _jokeService;
        public static IJokeService JokeService => _jokeService ?? (_jokeService = new JokeService());

        private static IGameFeatureManagementService _gameFeatureManagementService;
        public static IGameFeatureManagementService GameFeatureManagementService
            => _gameFeatureManagementService ?? (_gameFeatureManagementService = new GameFeatureManagementService());

        private static IWebBellwetherIntegrationGameService _webBellwetherIntegrationGameService;
        public static IWebBellwetherIntegrationGameService WebBellwetherIntegrationGameService
            =>
                _webBellwetherIntegrationGameService ??
                (_webBellwetherIntegrationGameService = new WebBellwetherIntegrationGameService());

        private static IIntegrationGameManagementService _integrationGameManagementService;
        public static IIntegrationGameManagementService IntegrationGameManagementService
            =>
                _integrationGameManagementService ??
                (_integrationGameManagementService = new IntegrationGameManagementService());

        private static IIntegrationGameService _integrationGameService;
        public static IIntegrationGameService IntegrationGameService
            => _integrationGameService ?? (_integrationGameService = new IntegrationGameService());

        private static ISpeechSyntesizerService _speechSyntesizerService;
        public static ISpeechSyntesizerService SpeechSyntesizerService
            => _speechSyntesizerService ?? (_speechSyntesizerService = new SpeechSyntesizerService());
    }
}
