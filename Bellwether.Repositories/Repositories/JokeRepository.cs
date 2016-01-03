using System;
using System.Collections.Generic;
using System.Linq;
using Bellwether.Models.Entities;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.Repositories.Factories;

namespace Bellwether.Repositories.Repositories
{
    public interface IJokeRepository
    {
        IEnumerable<JokeViewModel> GetJokes();
        bool CheckAndFillJokes(IEnumerable<Joke> mandatoryJokes);
        void RemoveJokes();
    }
    public class JokeRepository:IJokeRepository
    {
        private readonly IGenericRepository<JokeDao> _repository;
        private readonly IJokeCategoryRepository _jokeCategoryRepository;
        private JokeDao[] _localJokes;
        private JokeCategoryDao[] _localJokesCategories; 

        public JokeRepository(IGenericRepository<JokeDao> repository)
        {
            _repository = repository;
            _jokeCategoryRepository = RepositoryFactory.JokeCategoryRepository;
        }

        public IEnumerable<JokeViewModel> GetJokes()
        {
            var query = _repository.GetAll();
            var result = new List<JokeViewModel>();
            query.ToList().ForEach(x =>
            {
                result.Add(new JokeViewModel {Id = x.Id,JokeCategoryId = x.JokeCategory.Id,JokeContent = x.JokeContent,JokeCategoryName = x.JokeCategory.JokeCategoryName});
            });
            return result;
        }
        //to tak nie bedzie
        public void RemoveJokes()
        {
            _repository.Delete(x=>x.Id!= 0);
            _repository.Save();
        }
        public bool CheckAndFillJokes(IEnumerable<Joke> mandatoryJokes)
        {
            if (mandatoryJokes == null)
                return false;
            var jokes = mandatoryJokes as Joke[] ?? mandatoryJokes.ToArray();
            _localJokes = _repository.GetAll().ToArray();
            _localJokesCategories = _jokeCategoryRepository.GetJokeCategories().ToArray();
            if (_localJokes == null) return InsertJokesAndSave(jokes);
            InsertJokeIfNotExistsOnLocalList(jokes);
            DeleteJokeIfNotExistsOnMandatoryList(jokes);
            _repository.Save();
            Dispose();
            return true;
        }

        private void Dispose()
        {
            _localJokesCategories = null;
            _localJokes = null;
        }

        private JokeCategoryDao GetJokeCategoryById(int jokeCategoryId)
        {
            return _localJokesCategories.FirstOrDefault(x => x.Id == jokeCategoryId);
        }

        private bool InsertJokesAndSave(IEnumerable<Joke> mandatoryJokes)
        {
            try
            {
                var jokesDao = new List<JokeDao>();
                mandatoryJokes.ToList().ForEach(x =>
                {
                    jokesDao.Add(new JokeDao
                    {
                        Id = x.Id,
                        JokeContent = x.JokeContent,
                        JokeCategory = GetJokeCategoryById(x.JokeCategoryId)
                    });
                });
                _repository.InsertRange(jokesDao);
                _repository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void InsertJokeIfNotExistsOnLocalList(IEnumerable<Joke> mandatoryJokes)
        {
            mandatoryJokes.ToList().ForEach(x =>
            {
                if (_localJokes.ToList().FirstOrDefault(z => z.Id == x.Id) == null)
                    InsertJoke(x);
            });
        }

        private void DeleteJokeIfNotExistsOnMandatoryList(IEnumerable<Joke> mandatoryJokes)
        {
            _localJokes.ToList().ForEach(x =>
            {
                if (mandatoryJokes.ToList().FirstOrDefault(z => z.Id == x.Id) == null)
                    DeleteJoke(x);
            });
        }

        private void DeleteJoke(JokeDao localJoke)
        {
            _repository.Delete(x => x.Id == localJoke.Id);
        }
        private void InsertJoke(Joke mandatoryJoke)
        {
            _repository.Insert(new JokeDao {Id = mandatoryJoke.Id,JokeContent = mandatoryJoke.JokeContent,JokeCategory = GetJokeCategoryById(mandatoryJoke.JokeCategoryId)});
        }
    }
}
