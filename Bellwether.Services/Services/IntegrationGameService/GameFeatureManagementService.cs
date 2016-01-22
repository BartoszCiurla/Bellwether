using System.Collections.Generic;
using System.Linq;
using Bellwether.Models.ViewModels;
using Bellwether.Repositories.Entities;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.IntegrationGameService
{
    public interface IGameFeatureManagementService
    {
        bool ValidateAndFillGameFeatures(List<GameFeatureViewModel> mandatoryGameFeatures);
    }
    public class GameFeatureManagementService : IGameFeatureManagementService
    {
        public bool ValidateAndFillGameFeatures(List<GameFeatureViewModel> mandatoryGameFeatures)
        {
            if (mandatoryGameFeatures == null)
                return false;
            BellwetherLanguageDao localLanguage =
                RepositoryFactory.Context.BellwetherLanguages.FirstOrDefault(
                    x => x.Id == mandatoryGameFeatures.First().LanguageId);
            if (localLanguage == null)
                return false;
            InsertGameFeatureDetails(mandatoryGameFeatures, localLanguage);
            InsertGameFeatures(mandatoryGameFeatures, localLanguage);
            return true;
        }
        private void InsertGameFeatureDetails(List<GameFeatureViewModel> mandatoryGameFeatures, BellwetherLanguageDao language)
        {
            var localGameFeatureDetails = RepositoryFactory.Context.GameFeatureDetails.ToList();
            mandatoryGameFeatures.ForEach(x =>
            {
                x.GameFeatureDetailModels.ToList().ForEach(z =>
                {
                    var gameFeature = localGameFeatureDetails.FirstOrDefault(k => k.Id == z.GameFeatureDetailId);
                    if (gameFeature == null)
                    {
                        RepositoryFactory.Context.GameFeatureDetails.Add(new GameFeatureDetailDao
                        {
                            Id = z.GameFeatureDetailId,
                            Language = language,
                            GameFeatureDetailName = z.GameFeatureDetailName
                        });
                    }
                    else
                    {
                        gameFeature.GameFeatureDetailName = z.GameFeatureDetailName;
                        gameFeature.Language = language;
                        RepositoryFactory.Context.GameFeatureDetails.Update(gameFeature);
                    }
                });
            });
            RepositoryFactory.Context.SaveChanges();
        }

        private void InsertGameFeatures(List<GameFeatureViewModel> mandatoryGameFeatures, BellwetherLanguageDao language)
        {
            var localGameFeatures = RepositoryFactory.Context.GameFeatures.ToList();
            var localGameFeaturesDetail = RepositoryFactory.Context.GameFeatureDetails.ToList();
            mandatoryGameFeatures.ForEach(x =>
            {
                var gameFeature =
                    localGameFeatures.FirstOrDefault(
                        z => z.Id == x.Id);
                if (gameFeature == null)
                    RepositoryFactory.Context.GameFeatures.Add(new GameFeatureDao
                    {
                        Id = x.Id,
                        GameFeatureName = x.GameFeatureName,
                        Language = language,
                        GameFeatureDetails =
                            new List<GameFeatureDetailDao>(
                                x.GameFeatureDetailModels.ToList().Select(y => localGameFeaturesDetail.FirstOrDefault(k => k.Id == y.GameFeatureDetailId)))
                    });
                else
                {
                    gameFeature.GameFeatureName = x.GameFeatureName;
                    gameFeature.Language = language;
                    x.GameFeatureDetailModels.ToList().ForEach(k =>
                    {
                        var gameFeatureDetail = gameFeature.GameFeatureDetails.FirstOrDefault(u => u.Id == k.GameFeatureDetailId);
                        gameFeatureDetail.GameFeatureDetailName = k.GameFeatureDetailName;
                        gameFeatureDetail.Language = language;
                    });      
                    RepositoryFactory.Context.GameFeatures.Update(gameFeature);
                }
            });
            RepositoryFactory.Context.SaveChanges();
        }
    }
}
