using System.Reflection.Metadata;

namespace Bellwether.Repositories.Entities
{
    public class GameFeatureDetailDao
    {
        public int Id { get; set; }
        public string GameFeatureDetailName { get; set; }
        public int GameFeatureId { get; set; }
        public GameFeatureDao GameFeature { get; set; }
        public int LanguageId { get; set; }
        public BellwetherLanguageDao Language { get; set; }
    }
}
