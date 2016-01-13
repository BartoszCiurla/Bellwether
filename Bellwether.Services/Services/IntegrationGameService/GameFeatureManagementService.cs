using System.Linq;
using Bellwether.Models.Models;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.IntegrationGameService
{
    public interface IGameFeatureManagementService
    {
        bool ValidateAndFillGameFeatures(GameFeatureViewModel[] mandatoryGameFeatures);
    }
    public class GameFeatureManagementService: IGameFeatureManagementService
    {
        public bool ValidateAndFillGameFeatures(GameFeatureViewModel[] mandatoryGameFeatures)
        {
            if (mandatoryGameFeatures == null)
                return false;
            var localGameFeatures = RepositoryFactory.Context.GameFeatures.ToList();
            if(!localGameFeatures.Any())
            RepositoryFactory.Context.SaveChanges();
            return true;
        }
    }
}
