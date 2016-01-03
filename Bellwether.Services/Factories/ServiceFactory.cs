using Bellwether.Services.Services;
using Bellwether.WebServices.WebServices;

namespace Bellwether.Services.Factories
{
    public static class ServiceFactory
    {
        private static IInitResourceService _initResourceService;
        public static IInitResourceService InitResourceService
            => _initResourceService ?? (_initResourceService = new InitResourceService());

        private static ILanguageService _languageService;
        public static ILanguageService LanguageService => _languageService ?? (_languageService = new LanguageService());

        private static IWebBellwetherLanguageService _webBellwetherLanguageService;
        public static IWebBellwetherLanguageService WebBellwetherLanguageService
            => _webBellwetherLanguageService ?? (_webBellwetherLanguageService = new WebBellwetherLanguageService());

        private static IResourceService _resourceService;
        public static IResourceService ResourceService => _resourceService ?? (_resourceService = new ResourceService());

        private static IVersionService _versionService;
        public static IVersionService VersionService => _versionService ?? (_versionService = new VersionService());

        private static IWebBellwetherJokeService _webBellwetherJokeService;
        public static IWebBellwetherJokeService WebBellwetherJokeService
            => _webBellwetherJokeService ?? (_webBellwetherJokeService = new WebBellwetherJokeService());

        private static IJokeService _jokeService;
        public static IJokeService JokeService => _jokeService ?? (_jokeService = new JokeService());

        private static IWebBellwetherVersionService _webBellwetherVersionService;

        public static IWebBellwetherVersionService WebBellwetherVersionService
            => _webBellwetherVersionService ?? (_webBellwetherVersionService = new WebBellwetherVersionService());

        private static IWebBellwetherGameFeatureService _webBellwetherGameFeatureService;
        public static IWebBellwetherGameFeatureService WebBellwetherGameFeatureService
            =>
                _webBellwetherGameFeatureService ??
                (_webBellwetherGameFeatureService = new WebBellwetherGameFeatureService());


    }
}
