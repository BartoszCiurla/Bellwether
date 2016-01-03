using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellwether.Models.Entities;
using Bellwether.Models.Models;
using Bellwether.WebServices.Utility;
using Newtonsoft.Json;

namespace Bellwether.WebServices.WebServices
{
    public interface IWebBellwetherGameFeatureService
    {
        Task<IEnumerable<GameFeature>> GetGameFeatures(string urlWithParams);
    }
    public class WebBellwetherGameFeatureService:IWebBellwetherGameFeatureService
    {
        public async Task<IEnumerable<GameFeature>> GetGameFeatures(string urlWithParams)
        {
            var stringContent = await RequestExecutor.CreateRequestGetWithUriParam(urlWithParams);
            var gameFeatures = JsonConvert.DeserializeObject<GameFeature[]>(stringContent);
            return gameFeatures;
        } 
    }
}
