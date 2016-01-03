using System.Collections.Generic;
using System.Threading.Tasks;
using Bellwether.Models.Entities;
using Bellwether.Models.Models;
using Bellwether.WebServices.Utility;
using Newtonsoft.Json;

namespace Bellwether.WebServices.WebServices
{
    public interface IWebBellwetherJokeService
    {
        Task<IEnumerable<JokeCategoryDao>> GetJokeCategories(string urlWithParams);
        Task<IEnumerable<Joke>> GetJokes(string urlWithParams);
    }
    public class WebBellwetherJokeService:IWebBellwetherJokeService
    {
        public async Task<IEnumerable<JokeCategoryDao>> GetJokeCategories(string urlWithParams)
        {
            var stringContent = await RequestExecutor.CreateRequestGetWithUriParam(urlWithParams);
            var jokeCategories = JsonConvert.DeserializeObject<JokeCategoryDao[]>(stringContent);
            return jokeCategories;
        }
        public async Task<IEnumerable<Joke>> GetJokes(string urlWithParams)
        {
            var stringContent = await RequestExecutor.CreateRequestGetWithUriParam(urlWithParams);
            var jokes = JsonConvert.DeserializeObject<Joke[]>(stringContent);
            return jokes;
        }

    }
}
