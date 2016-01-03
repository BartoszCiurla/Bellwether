namespace Bellwether.Models.Scenarios
{
    public static class AppScenario
    {
        public static string[] SettingsScenario = { "ApplicationLanguage", "SynchronizeData"};

        public static string[] ApiUrlScenario = {
            "GetVersion", "GetAvailableLanguages", "GetLanguageFile", "GetLanguage",
            "GetJokeCategories", "GetJokes", "GetIntegrationGames",
            "GetIntegrationGamesFeatures"
        };

        public static string[] ApplicationVersion =
        {
            "ApplicationVersion", "LanguageVersion", "IntegrationGameVersion",
            "JokeCategoryVersion", "JokeVersion"
        };


    }
}
