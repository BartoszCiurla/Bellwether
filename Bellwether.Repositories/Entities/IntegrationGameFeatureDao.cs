namespace Bellwether.Repositories.Entities
{
    public class IntegrationGameFeatureDao
    {
        public int Id { get; set; }
        public int IntegrationGameId { get; set; }
        public IntegrationGameDao IntegrationGame { get; set; }
        public int GameFeatureId { get; set; }
        public GameFeatureDao GameFeature { get; set; }
        public int GameFeatureDetailId { get; set; }
        public GameFeatureDetailDao GameFeatureDetail { get; set; }
        public int LanguageId { get; set; }
        public BellwetherLanguageDao Language { get; set; }
    }
}
