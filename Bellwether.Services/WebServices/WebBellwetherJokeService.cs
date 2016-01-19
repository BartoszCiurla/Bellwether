using System.Threading.Tasks;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;
using Newtonsoft.Json;

namespace Bellwether.Services.WebServices
{
    public interface IWebBellwetherJokeService
    {
        Task<JokeCategoryViewModel[]> GetJokeCategories(int languageId);
        Task<JokeViewModel[]> GetJokes(int languageId);
    }
    public class WebBellwetherJokeService:IWebBellwetherJokeService
    {
        public async Task<JokeCategoryViewModel[]> GetJokeCategories(int languageId)
        {
            var stringContent = await RequestExecutor.CreateRequestGetWithUriParam(await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("GetJokeCategories") + languageId);
            var jokeCategories = JsonConvert.DeserializeObject<ResponseViewModel<JokeCategoryViewModel[]>>(stringContent);
            return jokeCategories.IsValid ? jokeCategories.Data : null;
        }
        public async Task<JokeViewModel[]> GetJokes(int languageId)
        {
            var stringContent = await RequestExecutor.CreateRequestGetWithUriParam(await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("GetJokes") + languageId);
            var jokes = JsonConvert.DeserializeObject<ResponseViewModel<JokeViewModel[]>>(stringContent);
            return jokes.IsValid ? jokes.Data : null;
        }

    }
}
