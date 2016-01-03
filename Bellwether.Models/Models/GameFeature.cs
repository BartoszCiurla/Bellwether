using System.Collections.Generic;

namespace Bellwether.Models.Models
{
    public class GameFeature
    {
        public int Id { get; set; }
        public string GameFeatureName { get; set; }
        public IEnumerable<GameFeatureDetail> GameFeatureDetailModels { get; set; }
    }
}
