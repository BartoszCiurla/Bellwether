using System.Collections.Generic;
using System.Linq;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.JokeService
{
    public interface IJokeService
    {
        IEnumerable<JokeViewModel> GetJokes();
    }
    public class JokeService:IJokeService
    {
        public IEnumerable<JokeViewModel> GetJokes()
        {
            return RepositoryFactory.Context.Jokes.ToList().Select(x => new JokeViewModel
            {
                Id = x.Id,
                JokeContent = x.JokeContent,
                JokeCategoryId = x.JokeCategory.Id,
                JokeCategoryName = x.JokeCategory.JokeCategoryName
            });

        }
    }
}
