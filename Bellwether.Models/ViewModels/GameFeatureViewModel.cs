using System.Collections.Generic;

namespace Bellwether.Models.ViewModels
{
    public class GameFeatureViewModel
    {
        public int Id { get; set; }
        public string GameFeatureName { get; set; }
        public int LanguageId { get; set; }
        public IEnumerable<GameFeatureDetailViewModel> GameFeatureDetailModels { get; set; }
    }
}
