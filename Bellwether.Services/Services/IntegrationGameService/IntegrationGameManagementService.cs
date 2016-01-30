using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bellwether.Models.ViewModels;
using Bellwether.Repositories.Entities;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.IntegrationGameService
{
    public interface IIntegrationGameManagementService
    {
        bool ValidateAndFillIntegrationGames(List<SimpleIntegrationGameViewModel> mandatoryIntegrationGames);
    }
    public class IntegrationGameManagementService : IIntegrationGameManagementService
    {
        public bool ValidateAndFillIntegrationGames(List<SimpleIntegrationGameViewModel> mandatoryIntegrationGames)
        {
            if (mandatoryIntegrationGames == null)
                return false;
            BellwetherLanguageDao localLanguage =
                RepositoryFactory.Context.BellwetherLanguages.FirstOrDefault(
                    x => x.Id == mandatoryIntegrationGames.First().LanguageId);
            if (localLanguage == null)
                return false;
            InsertIntegrationGame(mandatoryIntegrationGames, localLanguage);
            return true;
        }

        private void InsertIntegrationGame(List<SimpleIntegrationGameViewModel> mandatory, BellwetherLanguageDao language)
        {
            List<GameFeatureDao> localGameFeatures = RepositoryFactory.Context.GameFeatures.ToList();
            mandatory.ForEach(x =>
            {
                var integrationGameDao = RepositoryFactory.Context.IntegrationGames.FirstOrDefault(y => y.Id == x.Id);
                if (integrationGameDao == null)
                {
                    RepositoryFactory.Context.IntegrationGames.Add(new IntegrationGameDao { Id = x.Id, Language = language, IntegrationGameDescription = x.GameDescription, IntegrationGameName = x.GameName, GameFeatures = FillFeaturesForGame(x, localGameFeatures, language) });
                }
                else
                {
                    integrationGameDao.IntegrationGameDescription = x.GameDescription;
                    integrationGameDao.IntegrationGameName = x.GameName;
                    integrationGameDao.Language = language;
                    integrationGameDao.GameFeatures.ToList().ForEach(z =>
                    {
                        z.Language = language;
                        z.GameFeature = GetGameFeatureById(z.GameFeature.Id, localGameFeatures);
                        z.GameFeatureDetail = GetGameFeatureDetailById(z.GameFeatureDetail.Id, localGameFeatures);
                    });
                    RepositoryFactory.Context.Update(integrationGameDao);
                }
            });
            RepositoryFactory.Context.SaveChanges();
        }

        private List<IntegrationGameFeatureDao> FillFeaturesForGame(SimpleIntegrationGameViewModel mandatoryGame, List<GameFeatureDao> gameFeatures, BellwetherLanguageDao language)
        {
            var integrationGameFeatures = new List<IntegrationGameFeatureDao>();
            mandatoryGame.GameFeatures.ToList().ForEach(gameFeatureDetailId =>
            {
                integrationGameFeatures.Add(new IntegrationGameFeatureDao
                {
                    Language = language,
                    GameFeature = GetGameFeatureById(gameFeatureDetailId, gameFeatures),
                    GameFeatureDetail = GetGameFeatureDetailById(gameFeatureDetailId, gameFeatures)
                });
            });
            return integrationGameFeatures;
        }

        private GameFeatureDao GetGameFeatureById(int gameFeatureDetailId,
            List<GameFeatureDao> gameFeatures)
        {
            GameFeatureDao gameFeature = null;
            gameFeatures.ForEach(x =>
            {
                x.GameFeatureDetails.ToList().ForEach(z =>
                {
                    if (z.Id == gameFeatureDetailId)
                    {
                        gameFeature = x;
                    }
                });
            });
            return gameFeature;
        }

        private GameFeatureDetailDao GetGameFeatureDetailById(int gameFeatureDetailId,
            List<GameFeatureDao> gameFeatures)
        {
            GameFeatureDetailDao gameFeatureDetail = null;
            gameFeatures.ForEach(x =>
            {
                x.GameFeatureDetails.ToList().ForEach(z =>
                {
                    if (z.Id == gameFeatureDetailId)
                        gameFeatureDetail = z;
                });
            });
            return gameFeatureDetail;
        }
    }
}
