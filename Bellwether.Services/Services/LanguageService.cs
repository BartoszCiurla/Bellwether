using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellwether.Models.Models;
using Bellwether.Repositories.Context;
using Bellwether.Repositories.Repositories;
using Bellwether.WebServices.WebServices;

namespace Bellwether.Services.Services
{
    public interface ILanguageService
    {
        IEnumerable<BellwetherLanguage> GetLanguages();
        Task<Dictionary<string, string>> GetLanguageFile(BellwetherLanguage language, string apiUrl);
    }
    public class LanguageService:ILanguageService
    {
        private readonly IWebBellwetherLanguageService _service;
        private readonly ILanguageRepository _repository;
        public LanguageService()
        {
            _service = new WebBellwetherLanguageService();
            _repository = new LanguageRepository(new GenericRepository<BellwetherLanguage>(new DataContext()));          
        }

        public IEnumerable<BellwetherLanguage> GetLanguages()
        {
            return _repository.GetLanguages();
        }

        public async Task<Dictionary<string,string>> GetLanguageFile(BellwetherLanguage language, string apiUrl)
        {
            return await _service.GetLanguageFile(language, apiUrl);
        }
        //public async void TestService()
        //{
        //    //mam dzialajace podstawowe zachowanai teraz tylko musze zrobić resourcy i to jakoś połączyć 
        //    var languages = await _service.GetLanguages();
        //    if (languages == null)
        //        Debug.WriteLine("dupa zbita");
        //    bool resultInsert = _repository.CheckAndFillLanguages(languages);

        //    var locallanguages = _repository.GetLanguages();
        //    var bellwetherLanguages = locallanguages as BellwetherLanguage[] ?? locallanguages.ToArray();
        //    if (bellwetherLanguages.ToList().Any())
        //    {
        //        Debug.WriteLine(bellwetherLanguages.Count());
        //        Debug.WriteLine("dziala");
        //    }

        //}

    }
}
