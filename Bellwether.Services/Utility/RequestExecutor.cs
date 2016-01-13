using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Bellwether.Services.Utility
{
    public static class RequestExecutor
    {
        public static async Task<string> CreateRequestGetWithUriParam(string urlWithParams)
        {
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await hc.GetAsync(urlWithParams);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
