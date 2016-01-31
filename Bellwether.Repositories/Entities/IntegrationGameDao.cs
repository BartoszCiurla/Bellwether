using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bellwether.Repositories.Entities
{
    public class IntegrationGameDao
    {
        public int Id { get; set; }
        public string IntegrationGameName { get; set; }
        public string IntegrationGameDescription { get; set; }
        public List<IntegrationGameFeatureDao> GameFeatures { get; set; }
        public int LanguageId { get; set; }
        public  BellwetherLanguageDao Language { get; set; }
    }
}
