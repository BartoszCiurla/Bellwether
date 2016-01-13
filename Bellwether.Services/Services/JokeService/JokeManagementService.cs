using System.Collections.Generic;
using System.Linq;
using Bellwether.Models.Models;
using Bellwether.Repositories.Entities;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.JokeService
{
    public interface IJokeManagementService
    {
        bool ValidateAndFillJokes(Joke[] mandatoryJokes);
    }
    public class JokeManagementService : IJokeManagementService
    {
        public bool ValidateAndFillJokes(Joke[] mandatoryJokes)
        {
            if (mandatoryJokes == null)
                return false;
            var localJokes = RepositoryFactory.Context.Jokes.ToList();
            BellwetherLanguageDao localLanguage =
               RepositoryFactory.Context.BellwetherLanguages.FirstOrDefault(
                   x => x.Id == mandatoryJokes.First().LanguageId);
            if (localLanguage == null)
                return false;
            if (localJokes == null) return InsertJokesAndSave(mandatoryJokes, localLanguage);
            InsertJokeIfNotExistsOnLocalList(mandatoryJokes, localJokes, localLanguage);
            DeleteJokeIfNotExistsOnMandatoryList(mandatoryJokes, localJokes);
            RepositoryFactory.Context.SaveChanges();
            return true;
        }

        private bool InsertJokesAndSave(Joke[] mandatoryJokes, BellwetherLanguageDao language)
        {
            var jokeCategories = RepositoryFactory.Context.JokeCategories.ToList();
            mandatoryJokes.ToList().ForEach(x =>
            {
                var jokeCategory =
                    jokeCategories.FirstOrDefault(z => z.Language.Id == language.Id && z.Id == x.JokeCategoryId);
                if (jokeCategory != null)
                    RepositoryFactory.Context.Jokes.Add(new JokeDao
                    {
                        Id = x.Id,
                        JokeContent = x.JokeContent,
                        JokeCategory = jokeCategory,
                        Language = language
                    });
            });
            RepositoryFactory.Context.SaveChanges();
            return true;
        }

        private void InsertJokeIfNotExistsOnLocalList(Joke[] mandatoryJokes, List<JokeDao> localJokes, BellwetherLanguageDao language)
        {
            var jokeCategories = RepositoryFactory.Context.JokeCategories.ToList();
            mandatoryJokes.ToList().ForEach(x =>
            {
                if (localJokes.FirstOrDefault(z => z.Id == x.Id && z.Language.Id == language.Id) == null)
                {
                    var jokeCategory =
                    jokeCategories.FirstOrDefault(z => z.Language.Id == language.Id && z.Id == x.JokeCategoryId);
                    if (jokeCategory != null)
                        RepositoryFactory.Context.Jokes.Add(new JokeDao { Id = x.Id, Language = language, JokeContent = x.JokeContent,JokeCategory = jokeCategory});
                }

            });
        }

        private void DeleteJokeIfNotExistsOnMandatoryList(Joke[] mandatoryJokes, List<JokeDao> localJokes)
        {
            localJokes.ForEach(x =>
            {
                if (mandatoryJokes.ToList().FirstOrDefault(z => z.Id == x.Id) == null)
                    RepositoryFactory.Context.Jokes.Remove(x);
            });
        }
    }
}
