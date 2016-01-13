//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Threading.Tasks;
//using Bellwether.Models.Models;
//using Bellwether.Models.Scenarios;
//using Bellwether.Repositories.Repositories;
//using IResourceRepository = Bellwether.Repositories.Repositories.IResourceRepository;

//namespace Bellwether.Services.Services
//{
//    public interface IResourceService
//    {
//        Task<bool> ChangeLanguage(Dictionary<string, string> languageFile, BellwetherLanguage language);
//        Task ChangeJokeCategoryVersion(string jokeCategoryVersion);
//        Task ChangeJokeVersion(string jokeVersion);
//    }
//    public class ResourceService : IResourceService
//    {
//        //WEDŁUG MOJEGO PLANU Z TEGO MIEJSCA MAJA BYC POBIERANE JESZCZE SCENARIUSZE DLA WIDOKÓW ORAZ SYNCHRONIZACJA + JEJ USTAWIENIE + LEKTOR + JEGO USTAWIENEI
//        private readonly IResourceRepository _appResourceRepository;
//        private readonly IResourceRepository _langResourceRepository;

//        public ResourceService(IResourceRepository appResourceRepository, IResourceRepository langResourceRepository)
//        {
//            _appResourceRepository = appResourceRepository;
//            _langResourceRepository = langResourceRepository;
//        }

//        public async Task<bool> ChangeLanguage(Dictionary<string, string> languageFile, BellwetherLanguage language)
//        {
//            bool operationCompleted = await SaveLanguageInAppResources(language);
//            if (operationCompleted)
//                operationCompleted = await SaveLanguageFile(languageFile);
//            return operationCompleted;
//        }

//        public async Task ChangeJokeCategoryVersion(string jokeCategoryVersion)
//        {
//            await _appResourceRepository.SaveSelectedValues(resources: new Dictionary<string, string> { { "JokeCategoryVersion", jokeCategoryVersion } });
//        }

//        public async Task ChangeJokeVersion(string jokeVersion)
//        {
//            await
//                _appResourceRepository.SaveSelectedValues(resources:
//                    new Dictionary<string, string> { { "JokeVersion", jokeVersion } });
//        }
       


//        private async Task<bool> SaveLanguageInAppResources(BellwetherLanguage language)
//        {
//            await _appResourceRepository.SaveSelectedValues(resources: new Dictionary<string, string>
//                {
//                    {"ApplicationLanguage", language.LanguageShortName },
//                    {"LanguageVersion", language.LanguageVersion.ToString(CultureInfo.InvariantCulture) }
//                });
//            return true;
//        }
//        private async Task<bool> SaveLanguageFile(Dictionary<string, string> languageFile)
//        {
//            await _langResourceRepository.SaveValuesAndKays(languageFile);
//            return true;
//        }
//    }
//}
