using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Bellwether.Models.Entities;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.WebServices.Utility;
using Newtonsoft.Json;

namespace Bellwether.WebServices.WebServices
{
    public interface IWebBellwetherLanguageService
    {
        Task<IEnumerable<BellwetherLanguageDao>> GetLanguages(string urlWithParams);
        Task<Dictionary<string, string>> GetLanguageFile(string urlWithParams);
        Task<BellwetherLanguageDao> GetLanguage(string urlWithParams);
    }
    public class WebBellwetherLanguageService : IWebBellwetherLanguageService
    {
        public async Task<IEnumerable<BellwetherLanguageDao>> GetLanguages(string urlWithParams)
        {
            var stringContent = await RequestExecutor.CreateRequestGetWithUriParam(urlWithParams);
            var languages = JsonConvert.DeserializeObject<BellwetherLanguageDao[]>(stringContent);
            return languages.ToList();            
        }

        public async Task<Dictionary<string,string>> GetLanguageFile(string urlWithParams)
        {
            var stringContent = await RequestExecutor.CreateRequestGetWithUriParam(urlWithParams);
            var responseObj = JsonConvert.DeserializeObject<IEnumerable<LanguageFilePosition>>(stringContent);            
            var languageFile = new Dictionary<string,string>();
            if (responseObj == null)
                return null;
            responseObj.ToList().ForEach(x =>
            {
                languageFile.Add(x.Key, x.Value);
            });
            return languageFile;
        }

        public async Task<BellwetherLanguageDao> GetLanguage(string urlWithParams)
        {
            string stringContent = await RequestExecutor.CreateRequestGetWithUriParam(urlWithParams);
            var language = JsonConvert.DeserializeObject<BellwetherLanguageDao>(stringContent);
            return language;
        }
    }
}
