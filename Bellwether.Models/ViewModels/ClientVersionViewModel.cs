namespace Bellwether.Models.ViewModels
{
    public class ClientVersionViewModel
    {
        public Models.BellwetherLanguage Language { get; set; }
        public string ApplicationVersion { get; set; }
        public string LanguageVersion { get; set; }
        public string IntegrationGameVersion { get; set; }
        public string IntegrationGamesFeatureVersion { get; set; }
        public string JokeCategoryVersion { get; set; }
        public string JokeVersion { get; set; }    
    }
}
