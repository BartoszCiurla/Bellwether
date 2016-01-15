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
    public class GameFeatureManagementService: IGameFeatureManagementService
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
            FillGameFeatures(mandatoryGameFeatures, localLanguage);
            FillGameFeatureDetail(mandatoryGameFeatures, localLanguage);
            return true;
        }

        private void FillGameFeatureDetail(List<GameFeatureViewModel> mandatoryGameFeatures, BellwetherLanguageDao language)
        {
            var localGameFeatureDetails = RepositoryFactory.Context.GameFeatureDetails.ToList();
            RemoveGameFeatureDetailsIfNotExistsOnLocalList(mandatoryGameFeatures, localGameFeatureDetails);
            InsertGameFeatureDetailsIfNotExistsOnLocalList(mandatoryGameFeatures, localGameFeatureDetails, language);
        }

        private void FillGameFeatures(List<GameFeatureViewModel> mandatoryGameFeatures, BellwetherLanguageDao language)
        {
            var localGameFeatures = RepositoryFactory.Context.GameFeatures.ToList();
            RemoveGameFeaturesIfNotExistsOnMandatoryList(mandatoryGameFeatures, localGameFeatures);
            InsertGameFeaturesIfNotExistsOnLocalList(mandatoryGameFeatures, localGameFeatures, language);
        }

        private void RemoveGameFeatureDetailsIfNotExistsOnLocalList(List<GameFeatureViewModel> mandatoryGameFeatures ,List<GameFeatureDetailDao> localGameFeatureDetails )
        {
                localGameFeatureDetails.ForEach(x =>
                {
                    mandatoryGameFeatures.ForEach(z =>
                    {
                        z.GameFeatureDetailModels.ToList().ForEach(y =>
                        {
                            if (
                                mandatoryGameFeatures.FirstOrDefault(
                                    k =>
                                        k.Id == x.Id && k.GameFeatureName == x.GameFeatureDetailName &&
                                        k.LanguageId == x.Language.Id) == null)
                                RepositoryFactory.Context.GameFeatureDetails.Remove(x);
                        });
                    });
                });
            RepositoryFactory.Context.SaveChanges();
        }

        private void InsertGameFeatureDetailsIfNotExistsOnLocalList(List<GameFeatureViewModel> mandatoryGameFeatures,
      List<GameFeatureDetailDao> localGameFeatureDetails, BellwetherLanguageDao language)
        {
            List<GameFeatureDao> localGameFeatures = RepositoryFactory.Context.GameFeatures.ToList();
            mandatoryGameFeatures.ForEach(x =>
            {
                x.GameFeatureDetailModels.ToList().ForEach(z =>
                {
                    if (localGameFeatureDetails.FirstOrDefault(y => y.Id == z.Id && y.Language.Id == language.Id && y.GameFeatureDetailName == z.GameFeatureDetailName) ==
                        null)
                    {
                        var entityToUpdate =
                            localGameFeatures.FirstOrDefault(k => k.Id == x.Id && k.Language.Id == language.Id);
                        entityToUpdate.GameFeatureDetails.Add(new GameFeatureDetailDao
                        {
                            Id = z.Id,
                            GameFeatureDetailName = z.GameFeatureDetailName,
                            Language = language
                        });
                        RepositoryFactory.Context.Update(entityToUpdate);
                    }
                });
            });        
         
        }

        private void InsertGameFeaturesIfNotExistsOnLocalList(List<GameFeatureViewModel> mandatoryGameFeatures,
            List<GameFeatureDao> localGameFeatures,BellwetherLanguageDao language)
        {
            List<GameFeatureDao> gameFeatures = new List<GameFeatureDao>();
            mandatoryGameFeatures.ForEach(x =>
            {
                if (localGameFeatures.FirstOrDefault(z => z.Id == x.Id && z.Language.Id == language.Id && z.GameFeatureName == x.GameFeatureName) == null)
                    gameFeatures.Add(new GameFeatureDao
                    {
                        Id = x.Id,
                        GameFeatureName = x.GameFeatureName,
                        Language = language,
                        GameFeatureDetails =
                            new List<GameFeatureDetailDao>(
                                x.GameFeatureDetailModels.ToList().Select(y => new GameFeatureDetailDao
                                {
                                    Id = y.Id,
                                    Language = language,
                                    GameFeatureDetailName = y.GameFeatureDetailName
                                }))
                    });
            });
            RepositoryFactory.Context.GameFeatures.AddRange(gameFeatures);
            RepositoryFactory.Context.SaveChanges();
        }

        private void RemoveGameFeaturesIfNotExistsOnMandatoryList(List<GameFeatureViewModel> mandatoryGameFeatures,
            List<GameFeatureDao> localGameFeatures)
        {
            localGameFeatures.ForEach(x =>
            {
                if (mandatoryGameFeatures.FirstOrDefault(z => z.Id == x.Id && z.LanguageId == x.Language.Id && z.GameFeatureName == x.GameFeatureName) ==
                    null)
                    RepositoryFactory.Context.GameFeatures.Remove(x);
            });
            RepositoryFactory.Context.SaveChanges();
        }
    }
}
