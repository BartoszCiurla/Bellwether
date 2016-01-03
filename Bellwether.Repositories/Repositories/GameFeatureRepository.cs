using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bellwether.Models.Entities;
using Bellwether.Models.Models;

namespace Bellwether.Repositories.Repositories
{
    public interface IGameFeatureRepository
    {
        bool CheckAndFillGameFeatures(IEnumerable<GameFeature> mandatoryGameFeatures);
        IEnumerable<GameFeatureDao> GetGameFeatures();
    }
    public class GameFeatureRepository:IGameFeatureRepository
    {
        private IGenericRepository<GameFeatureDao> _gameFeatureRepository;
        private IGenericRepository<GameFeatureDetailDao> _gameFeatureDetailRepository;
        private IEnumerable<GameFeatureDao> _localGameFeatures;
        private IEnumerable<GameFeatureDetailDao> _localGameFeatureDetailDaos; 

        public GameFeatureRepository(IGenericRepository<GameFeatureDao> gameFeatureRepository,
            IGenericRepository<GameFeatureDetailDao> gameFeatureDetailRepository)
        {
            _gameFeatureRepository = gameFeatureRepository;
            _gameFeatureDetailRepository = gameFeatureDetailRepository;
        }

        public IEnumerable<GameFeatureDao> GetGameFeatures()
        {
            return _gameFeatureRepository.GetAll();
        }

        private IEnumerable<GameFeatureDetailDao> GetGameFeatureDetailDaos()
        {
            return _gameFeatureDetailRepository.GetAll();
        } 

        public bool CheckAndFillGameFeatures(IEnumerable<GameFeature> mandatoryGameFeatures)
        {
            if (mandatoryGameFeatures == null)
                return false;
            try
            {
                var gameFeatures = mandatoryGameFeatures as GameFeature[] ?? mandatoryGameFeatures.ToArray();
                _localGameFeatures = GetGameFeatures();//ta linia jest bardzo zla trzeba to inaczej zrobic
                _localGameFeatureDetailDaos = GetGameFeatureDetailDaos();
                if (!_localGameFeatures.Any() && !_localGameFeatureDetailDaos.Any()) return InsertGameFeaturesAndSave(gameFeatures);
                return false;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return false;
            }
        }

        private bool InsertGameFeaturesAndSave(IEnumerable<GameFeature> mandatoryGameFeatures)
        {
            try
            {
                var mandatory = mandatoryGameFeatures.ToList();
                mandatory.ForEach(x =>
                {
                    var feature = new GameFeatureDao {Id = x.Id, GameFeatureName = x.GameFeatureName,GameFeatureDetails = new List<GameFeatureDetailDao>()};
                    x.GameFeatureDetailModels.ToList().ForEach(z =>
                    {
                        feature.GameFeatureDetails.Add(new GameFeatureDetailDao
                        {
                            Id = z.Id,
                            GameFeatureDetailName = z.GameFeatureDetailName
                        });
                    });
                    _gameFeatureRepository.Insert(feature);
                });
                _gameFeatureRepository.Save();
                _gameFeatureDetailRepository.Save();
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return false;
            }
        }
    }
}
