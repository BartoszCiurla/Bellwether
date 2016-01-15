using System.Collections.Generic;
using System.Linq;
using Bellwether.Models.ViewModels;
using Bellwether.Repositories.Entities;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.IntegrationGameService
{
    public interface IIntegrationGameManagementService
    {
        bool ValidateAndFillIntegrationGames(List<DirectIntegrationGameViewModel> mandatoryIntegrationGames);
    }
    public class IntegrationGameManagementService : IIntegrationGameManagementService
    {
        public bool ValidateAndFillIntegrationGames(List<DirectIntegrationGameViewModel> mandatoryIntegrationGames)
        {
            if (mandatoryIntegrationGames == null)
                return false;
            List<IntegrationGameDao> localIntegrationGames = RepositoryFactory.Context.IntegrationGames.ToList();
            BellwetherLanguageDao localLanguage =
                RepositoryFactory.Context.BellwetherLanguages.FirstOrDefault(
                    x => x.Id == mandatoryIntegrationGames.First().LanguageId);
            if (localLanguage == null)
                return false;
            InsertIntegrationGameIfNotExistsOnLocalList(mandatoryIntegrationGames, localIntegrationGames, localLanguage);
            RemoveIntegrationGameIfNotExistsOnMandatoryList(mandatoryIntegrationGames, localIntegrationGames);
            return true;
        }

        private void InsertIntegrationGameIfNotExistsOnLocalList(List<DirectIntegrationGameViewModel> mandatory,
            List<IntegrationGameDao> local, BellwetherLanguageDao language)
        {
            List<GameFeatureDao> gameFeatures = RepositoryFactory.Context.GameFeatures.ToList();
            mandatory.ForEach(x =>
            {
                if (local.FirstOrDefault(y => y.Id == x.Id && y.Language.Id == x.LanguageId) == null)
                    RepositoryFactory.Context.IntegrationGames.Add(new IntegrationGameDao {Id=x.Id,Language = language,IntegrationGameDescription = x.GameDescription,IntegrationGameName = x.GameName,GameFeatures = FillFeaturesForGame(x,gameFeatures,language)});
            });
            RepositoryFactory.Context.SaveChanges();
        }

        private void RemoveIntegrationGameIfNotExistsOnMandatoryList(List<DirectIntegrationGameViewModel> mandatory,
            List<IntegrationGameDao> local)
        {
            local.ForEach(x =>
            {
                if (mandatory.FirstOrDefault(z => z.Id == x.Id && z.LanguageId == x.Language.Id) == null)
                {
                    RepositoryFactory.Context.IntegrationGames.Remove(x);
                    RepositoryFactory.Context.IntegrationGameFeatures.RemoveRange(x.GameFeatures);
                }
            });
            RepositoryFactory.Context.SaveChanges();
        }
        private List<IntegrationGameFeatureDao> FillFeaturesForGame(DirectIntegrationGameViewModel mandatoryGame, List<GameFeatureDao> gameFeatures, BellwetherLanguageDao language)
        {
            return new List<IntegrationGameFeatureDao>
            {
                new IntegrationGameFeatureDao
                {
                    Language = language,
                    GameFeature = GetGameFeatureByName(mandatoryGame.GameName, gameFeatures),
                    GameFeatureDetail = GetGameFeatureDetailByName(mandatoryGame.GameName, gameFeatures)
                },
                new IntegrationGameFeatureDao
                {
                    Language = language,
                    GameFeature = GetGameFeatureByName(mandatoryGame.CategoryGame, gameFeatures),
                    GameFeatureDetail = GetGameFeatureDetailByName(mandatoryGame.CategoryGame, gameFeatures)
                },
                new IntegrationGameFeatureDao
                {
                    Language = language,
                    GameFeature = GetGameFeatureByName(mandatoryGame.NumberOfPlayer, gameFeatures),
                    GameFeatureDetail = GetGameFeatureDetailByName(mandatoryGame.NumberOfPlayer, gameFeatures)
                },
                new IntegrationGameFeatureDao
                {
                    Language = language,
                    GameFeature = GetGameFeatureByName(mandatoryGame.PreparationFun, gameFeatures),
                    GameFeatureDetail = GetGameFeatureDetailByName(mandatoryGame.PreparationFun, gameFeatures)
                }
            };
        }

        private GameFeatureDao GetGameFeatureByName(string gameFeatureDetailName,
            List<GameFeatureDao> gameFeatures)
        {
            GameFeatureDao gameFeature = null;
            gameFeatures.ForEach(x =>
            {
                x.GameFeatureDetails.ToList().ForEach(z =>
                {
                    if (z.GameFeatureDetailName == gameFeatureDetailName)
                    {
                        gameFeature = x;
                    }
                });
            });
            return gameFeature;
        }

        private GameFeatureDetailDao GetGameFeatureDetailByName(string gameFeatureDetailName,
            List<GameFeatureDao> gameFeatures)
        {
            GameFeatureDetailDao gameFeatureDetail = null;
            gameFeatures.ForEach(x =>
            {
                x.GameFeatureDetails.ToList().ForEach(z =>
                {
                    if (z.GameFeatureDetailName == gameFeatureDetailName)
                        gameFeatureDetail = z;
                });
            });
            return gameFeatureDetail;
        }
    }
}
