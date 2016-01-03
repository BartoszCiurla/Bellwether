using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bellwether.Models.Entities;

namespace Bellwether.Repositories.Repositories
{
    public interface IJokeCategoryRepository
    {
        IEnumerable<JokeCategoryDao> GetJokeCategories();
        bool CheckAndFillJokeCategories(IEnumerable<JokeCategoryDao> mandatoryJokeCategories);
        void RemoveJokeCategories();
    }
    public class JokeCategoryRepository:IJokeCategoryRepository
    {
        private readonly IGenericRepository<JokeCategoryDao> _repository;
        private IEnumerable<JokeCategoryDao> _localJokeCategories; 

        public JokeCategoryRepository(IGenericRepository<JokeCategoryDao> repository)
        {
            _repository = repository;
        }

        public void RemoveJokeCategories()
        {
            _repository.Delete(x=>x.Id != 0);
            _repository.Save();
        }
        public IEnumerable<JokeCategoryDao> GetJokeCategories()
        {
            return _repository.GetAll();
        } 
        public bool CheckAndFillJokeCategories(IEnumerable<JokeCategoryDao> mandatoryJokeCategories)
        {
            if (mandatoryJokeCategories == null)
                return false;
            try
            {
                var jokeCategories = mandatoryJokeCategories as JokeCategoryDao[] ?? mandatoryJokeCategories.ToArray();
                _localJokeCategories = GetJokeCategories();
                if (_localJokeCategories == null) return InsertJokeCategoriesAndSave(jokeCategories);
                InsertJokeCategoryIfNotExistsOnLocalList(jokeCategories);
                DeleteJokeCategoryIfNotExistsOnMandatoryList(jokeCategories);
                _repository.Save();
                _localJokeCategories = null;
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return false;
            }
        }

        private void InsertJokeCategoryIfNotExistsOnLocalList(IEnumerable<JokeCategoryDao> mandatoryJokeCategories)
        {
            mandatoryJokeCategories.ToList().ForEach(mandatoryJokeCategory =>
            {
                if (_localJokeCategories.ToList().FirstOrDefault(local => local.Id == mandatoryJokeCategory.Id) == null)
                    InsertJokeCategory(mandatoryJokeCategory);
            });
        }

        private void DeleteJokeCategoryIfNotExistsOnMandatoryList(IEnumerable<JokeCategoryDao> mandatoryJokeCategories)
        {
            _localJokeCategories.ToList().ForEach(localJokeCategory =>
            {
                if (mandatoryJokeCategories.ToList().FirstOrDefault(mandatoryJokeCategory => mandatoryJokeCategory.Id == localJokeCategory.Id) == null)
                    DeleteJokeCategory(localJokeCategory);
            });
        }

        private bool InsertJokeCategoriesAndSave(IEnumerable<JokeCategoryDao> mandatoryJokeCategories)
        {
            try
            {
                _repository.InsertRange(mandatoryJokeCategories);
                _repository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void InsertJokeCategory(JokeCategoryDao mandatoryLanguage)
        {
            _repository.Insert(mandatoryLanguage);
        }

        private void DeleteJokeCategory(JokeCategoryDao localJokeCategory)
        {
            _repository.Delete(localJokeCategory);
        }
    }
}
