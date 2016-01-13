using System.Collections.Generic;

namespace Bellwether.Models.Models
{
    public class GameFeatureViewModel
    {
        public int Id { get; set; }
        public string GameFeatureName { get; set; }
        public IEnumerable<GameFeatureDetailViewModel> GameFeatureDetailModels { get; set; }
    }
}
