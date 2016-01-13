using System.Threading.Tasks;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;
using Newtonsoft.Json;

namespace Bellwether.Services.WebServices
{
    public interface IWebBellwetherJokeService
    {
        Task<JokeCategory[]> GetJokeCategories(int languageId);
        Task<Joke[]> GetJokes(int languageId);
    }
    public class WebBellwetherJokeService:IWebBellwetherJokeService
    {
        public async Task<JokeCategory[]> GetJokeCategories(int languageId)
        {
            var stringContent = await RequestExecutor.CreateRequestGetWithUriParam(await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("GetJokeCategories") + languageId);
            var jokeCategories = JsonConvert.DeserializeObject<ResponseViewModel<JokeCategory[]>>(stringContent);
            return jokeCategories.IsValid ? jokeCategories.Data : null;
        }
        public async Task<Joke[]> GetJokes(int languageId)
        {
            var stringContent = await RequestExecutor.CreateRequestGetWithUriParam(await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("GetJokes") + languageId);
            var jokes = JsonConvert.DeserializeObject<ResponseViewModel<Joke[]>>(stringContent);
            return jokes.IsValid ? jokes.Data : null;
        }

    }
}
