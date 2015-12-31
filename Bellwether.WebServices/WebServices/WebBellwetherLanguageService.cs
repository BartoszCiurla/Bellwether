using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Newtonsoft.Json;

namespace Bellwether.WebServices.WebServices
{
    public interface IWebBellwetherLanguageService
    {
        Task<IEnumerable<BellwetherLanguage>> GetLanguages(string urlWithParams);
        Task<Dictionary<string, string>> GetLanguageFile(string urlWithParams);
        Task<BellwetherLanguage> GetLanguage(string urlWithParams);
    }
    public class WebBellwetherLanguageService : IWebBellwetherLanguageService
    {
        public async Task<IEnumerable<BellwetherLanguage>> GetLanguages(string urlWithParams)
        {
            var stringContent = await CreateRequestWithUriParam(urlWithParams);
            var responseObj = JsonConvert.DeserializeObject<BellwetherLanguage[]>(stringContent);
            return responseObj.ToList();            
        }

        public async Task<Dictionary<string,string>> GetLanguageFile(string urlWithParams)
        {
            var stringContent = await CreateRequestWithUriParam(urlWithParams);
            var responseObj = JsonConvert.DeserializeObject<IEnumerable<LanguageFilePosition>>(stringContent);            
            var result = new Dictionary<string,string>();
            if (responseObj == null)
                return null;
            responseObj.ToList().ForEach(x =>
            {
                result.Add(x.Key, x.Value);
            });
            return result;
        }

        public async Task<BellwetherLanguage> GetLanguage(string urlWithParams)
        {
            string stringContent = await CreateRequestWithUriParam(urlWithParams);
            var responseObj = JsonConvert.DeserializeObject<BellwetherLanguage>(stringContent);
            return responseObj;
        }

        private async Task<string> CreateRequestWithUriParam(string urlWithParams)
        {
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await hc.GetAsync(urlWithParams);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
