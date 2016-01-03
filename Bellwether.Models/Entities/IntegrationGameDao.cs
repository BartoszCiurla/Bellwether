using System.Collections.Generic;
namespace Bellwether.Models.Entities
{
    public class IntegrationGameDao
    {
        public int Id { get; set; }
        public string IntegrationGameName { get; set; }
        public string IntegrationGameDescription { get; set; }
        public virtual ICollection<IntegrationGameFeatureDao> GameFeatures { get; set; }
    }
}
