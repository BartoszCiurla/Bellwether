using System.Threading.Tasks;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;
using Newtonsoft.Json;

namespace Bellwether.Services.WebServices
{
    public interface IWebBellwetherVersionService
    {
        Task<ClientVersionViewModel> GetVersionForLanguage(int languageid);
    }
    public class WebBellwetherVersionService:IWebBellwetherVersionService
    {
        public async Task<ClientVersionViewModel> GetVersionForLanguage(int languageId)
        {
            var stringContent =
                await
                    RequestExecutor.CreateRequestGetWithUriParam(
                        await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("GetVersion") + languageId);
            var webBellwetherVersion = JsonConvert.DeserializeObject<ResponseViewModel<ClientVersionViewModel>>(stringContent);
            return webBellwetherVersion.IsValid ? webBellwetherVersion.Data : null;
        } 
    }
}
