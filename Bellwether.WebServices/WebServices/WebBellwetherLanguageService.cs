using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bellwether.Models.Models;
using Newtonsoft.Json;

namespace Bellwether.WebServices.WebServices
{
    public interface IWebBellwetherLanguageService
    {
        Task<IEnumerable<BellwetherLanguage>> GetLanguages(string apiUrl);
        Task<Dictionary<string, string>> GetLanguageFile(BellwetherLanguage language, string apiUrl);
    }
    public class WebBellwetherLanguageService:IWebBellwetherLanguageService
    {
        public async Task<IEnumerable<BellwetherLanguage>> GetLanguages(string apiUrl)
        {
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var requestContent = await hc.GetAsync(apiUrl);
            var stringContent = await requestContent.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<BellwetherLanguage[]>(stringContent);
            List<BellwetherLanguage> languages = responseObj.ToList();
            return languages;
        }

        public async Task<Dictionary<string, string>> GetLanguageFile(BellwetherLanguage language,string apiUrl)
        {
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var requestContent = await hc.GetAsync("http://localhost:62645/api/Language/GetLanguageFile/?languageId=" + language.Id);
            var stringContent = await requestContent.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<Dictionary<string,string>>(stringContent);
            return responseObj;
        } 
    }
}
