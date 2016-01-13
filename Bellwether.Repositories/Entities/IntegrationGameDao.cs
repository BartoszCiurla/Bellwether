using System.Collections.Generic;

namespace Bellwether.Repositories.Entities
{
    public class IntegrationGameDao
    {
        public int Id { get; set; }
        public string IntegrationGameName { get; set; }
        public string IntegrationGameDescription { get; set; }
        public virtual ICollection<IntegrationGameFeatureDao> GameFeatures { get; set; }
        public virtual BellwetherLanguageDao Language { get; set; }
    }
}
