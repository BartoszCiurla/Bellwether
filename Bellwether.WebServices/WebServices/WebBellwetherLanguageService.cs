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
        Task<IEnumerable<BellwetherLanguage>> GetLanguages();
    }
    public class WebBellwetherLanguageService:IWebBellwetherLanguageService
    {
        public async Task<IEnumerable<BellwetherLanguage>> GetLanguages()
        {
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var requestContent = await hc.GetAsync(WebBellwetherApis.GetAvailableLanguages);
            var stringContent = await requestContent.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<BellwetherLanguage[]>(stringContent);
            List<BellwetherLanguage> languages = responseObj.ToList();
            return languages;
        }
    }
}
