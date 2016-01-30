using System.Collections.Generic;
using System.Threading.Tasks;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.LanguageService
{
    public interface ILanguageManagementService
    {
        Task<bool> ChangeApplicationLanguage(string languageShortName);
    }
    public class LanguageManagementService: ILanguageManagementService
    {
        public async Task<bool> ChangeApplicationLanguage(string languageShortName)
        {
            var resetSelectedKeysInResources = new Dictionary<string, string>
            {
                {"ApplicationLanguage", languageShortName},
                {"LanguageVersion", "-1"},
                {"IntegrationGameVersion", "-1"},
                {"GameFeatureVersion", "-1"},
                {"JokeCategoryVersion", "-1"},
                {"JokeVersion", "-1"}
            };
            if (!await RepositoryFactory.ApplicationResourceRepository.SaveSelectedValues(resetSelectedKeysInResources))
                return false;
            var validateResult = await 
                ServiceExecutor.ExecuteAsyncIfSyncData(() => ServiceFactory.VersionValidateService.ValidateVersion());            
            return validateResult.IsValid;
        }
    }
}
