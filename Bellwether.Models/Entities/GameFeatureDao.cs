using System.Collections.Generic;
namespace Bellwether.Models.Entities
{
    public class GameFeatureDao
    {
        public int Id { get; set; }
        public string GameFeatureName { get; set; }
        public virtual ICollection<GameFeatureDetailDao> GameFeatureDetails { get; set; }
    }
}
