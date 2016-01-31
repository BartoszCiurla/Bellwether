using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            InsertGameFeatures(mandatoryGameFeatures, localLanguage);
            return true;
        }        

        private void InsertGameFeatures(List<GameFeatureViewModel> mandatoryGameFeatures, BellwetherLanguageDao language)
        {
            var localGameFeatures = RepositoryFactory.Context.GameFeatures;
            mandatoryGameFeatures.ForEach(x =>
            {
                var gameFeature =
                    localGameFeatures.FirstOrDefault(
                        z => z.Id == x.Id);
                if (gameFeature == null)
                {                    
                    List<GameFeatureDetailDao> gameFeatureDetail = new List<GameFeatureDetailDao>();
                    x.GameFeatureDetailModels.ToList().ForEach(z =>
                    {
                        gameFeatureDetail.Add(new GameFeatureDetailDao
                        {
                            Id = z.GameFeatureDetailId,
                            Language = language,
                            GameFeatureDetailName = z.GameFeatureDetailName
                        });
                    });
                    RepositoryFactory.Context.GameFeatures.Add(new GameFeatureDao
                    {
                        Id = x.Id,
                        GameFeatureName = x.GameFeatureName,
                        Language = language,
                        GameFeatureDetails = gameFeatureDetail                          
                    });
                }                   
                else
                {
                    gameFeature.GameFeatureName = x.GameFeatureName;
                    gameFeature.Language = language;
                    x.GameFeatureDetailModels.ToList().ForEach(k =>
                    {
                        var gameFeatureDetail =
                            RepositoryFactory.Context.GameFeatureDetails.FirstOrDefault(
                                z => z.Id == k.GameFeatureDetailId);
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
