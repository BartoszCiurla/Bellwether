using System.Collections.Generic;
using System.Threading.Tasks;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services
{
    public interface ILanguageResourceService
    {
        Task<Dictionary<string, string>> GetLanguageContentScenario(IEnumerable<string> requiredKeysForScenario);
    }
    public class LanguageResourceService:ILanguageResourceService
    {
        public async Task<Dictionary<string, string>> GetLanguageContentScenario(IEnumerable<string> requiredKeysForScenario)
        {
            return await RepositoryFactory.LanguageResourceRepository.GetSelectedKeysValues(requiredKeysForScenario);
        }
    }
}
