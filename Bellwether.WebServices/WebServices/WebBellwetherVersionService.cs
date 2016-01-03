using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.WebServices.Utility;
using Newtonsoft.Json;

namespace Bellwether.WebServices.WebServices
{
    public interface IWebBellwetherVersionService
    {
        Task<AppVersion> GetVersionForLanguage(string urlWithParams);
    }
    public class WebBellwetherVersionService:IWebBellwetherVersionService
    {
        public async Task<AppVersion> GetVersionForLanguage(string urlWithParams)
        {
            var stringContent = await RequestExecutor.CreateRequestGetWithUriParam(urlWithParams);
            var webBellwetherVersion = JsonConvert.DeserializeObject<AppVersion>(stringContent);
            return webBellwetherVersion;
        } 
    }
}
