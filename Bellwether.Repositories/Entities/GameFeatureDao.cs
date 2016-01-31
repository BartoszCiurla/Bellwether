using System.Collections.Generic;

namespace Bellwether.Repositories.Entities
{
    public class GameFeatureDao
    {
        public int Id { get; set; }
        public string GameFeatureName { get; set; }
        public List<GameFeatureDetailDao> GameFeatureDetails { get; set; }
        public int LanguageId { get; set; }
        public BellwetherLanguageDao Language { get; set; }
    }
}
