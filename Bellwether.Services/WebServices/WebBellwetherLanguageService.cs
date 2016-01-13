using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.Repositories.Entities;
using Bellwether.Services.Utility;
using Newtonsoft.Json;

namespace Bellwether.Services.WebServices
{
    public interface IWebBellwetherLanguageService
    {
        Task<BellwetherLanguageDao[]> GetLanguages();
        Task<Dictionary<string, string>> GetLanguageFile(int languageId);
        Task<BellwetherLanguageDao> GetLanguage(int languageId);
    }
    public class WebBellwetherLanguageService : IWebBellwetherLanguageService
    {
        public async Task<BellwetherLanguageDao[]> GetLanguages()
        {
            var stringContent = await RequestExecutor.CreateRequestGetWithUriParam(await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("GetAvailableLanguages"));
            var languages = JsonConvert.DeserializeObject<ResponseViewModel<BellwetherLanguageDao[]>>(stringContent);
            return languages.IsValid ? languages.Data : null;           
        }

        public async Task<Dictionary<string, string>> GetLanguageFile(int languageId)
        {
            var stringContent =
                await
                    RequestExecutor.CreateRequestGetWithUriParam(
                        await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("GetLanguageFile") +
                        languageId);
            var responseObj = JsonConvert.DeserializeObject<ResponseViewModel<IEnumerable<LanguageFilePosition>>>(stringContent);
            var languageFile = new Dictionary<string, string>();
            if (!responseObj.IsValid)
                return null;
            responseObj.Data.ToList().ForEach(x =>
            {
                languageFile.Add(x.Key, x.Value);
            });
            return languageFile;
        }

        public async Task<BellwetherLanguageDao> GetLanguage(int languageId)
        {
            string stringContent =
                await
                    RequestExecutor.CreateRequestGetWithUriParam(
                        await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("GetLanguage") + languageId);
            var language = JsonConvert.DeserializeObject<ResponseViewModel<BellwetherLanguageDao>>(stringContent);
            return language.IsValid ? language.Data : null;
        }
    }
}
