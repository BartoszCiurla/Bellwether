using System.Linq;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.Repositories.Entities;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.JokeService
{
    public interface IJokeManagementService
    {
        bool ValidateAndFillJokes(JokeViewModel[] mandatoryJokes);
    }
    public class JokeManagementService : IJokeManagementService
    {
        public bool ValidateAndFillJokes(JokeViewModel[] mandatoryJokes)
        {
            if (mandatoryJokes == null)
                return false;

            BellwetherLanguageDao localLanguage =
               RepositoryFactory.Context.BellwetherLanguages.FirstOrDefault(
                   x => x.Id == mandatoryJokes.First().LanguageId);
            if (localLanguage == null)
                return false;
            InsertJokeIfNotExistsOnLocalList(mandatoryJokes, localLanguage);

            return true;
        }
        private void InsertJokeIfNotExistsOnLocalList(JokeViewModel[] mandatoryJokes, BellwetherLanguageDao language)
        {
            var jokeCategories = RepositoryFactory.Context.JokeCategories.ToList();
            var localJokes = RepositoryFactory.Context.Jokes.ToList();
            mandatoryJokes.ToList().ForEach(x =>
            {
                var jokeCategory =
                  jokeCategories.FirstOrDefault(z => z.Language.Id == language.Id && z.Id == x.JokeCategoryId);
                if (jokeCategory != null)
                {
                    var localJoke = localJokes.FirstOrDefault(z => z.Id == x.Id);
                    if (localJoke == null)
                    {

                        RepositoryFactory.Context.Jokes.Add(new JokeDao { Id = x.Id, Language = language, JokeContent = x.JokeContent, JokeCategory = jokeCategory });
                    }
                    else
                    {
                        localJoke.JokeContent = x.JokeContent;
                        localJoke.Language = language;
                        localJoke.JokeCategory = jokeCategory;
                    }
                }
            });
            RepositoryFactory.Context.SaveChanges();
        }
    }
}
