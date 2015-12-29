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
        IEnumerable<BellwetherLanguage> GetLocalLanguages();
        Task<Dictionary<string, string>> GetLanguageFile(string urlWithParams);
        Task<BellwetherLanguage> GetLanguage(string urlWithParams);
        Task<IEnumerable<BellwetherLanguage>> GetLanguages(string urlWithParams);
        bool CheckAndFillLanguages(IEnumerable<BellwetherLanguage> mandatoryLanguages);
    }
    public class LanguageService : ILanguageService
    {
        private readonly IWebBellwetherLanguageService _service;
        private readonly ILanguageRepository _repository;
        public LanguageService()
        {
            _service = new WebBellwetherLanguageService();
            _repository = new LanguageRepository(new GenericRepository<BellwetherLanguage>(new DataContext()));
        }

        public IEnumerable<BellwetherLanguage> GetLocalLanguages()
        {
            return _repository.GetLanguages();
        }

        public async Task<Dictionary<string, string>> GetLanguageFile(string urlWithParams)
        {
            return await _service.GetLanguageFile(urlWithParams);
        }

        public async Task<BellwetherLanguage> GetLanguage(string urlWithParams)
        {
            return await _service.GetLanguage(urlWithParams);
        }

        public async Task<IEnumerable<BellwetherLanguage>> GetLanguages(string urlWithParams)
        {
            return await _service.GetLanguages(urlWithParams);
        }

        public bool CheckAndFillLanguages(IEnumerable<BellwetherLanguage> mandatoryLanguages)
        {
            return  _repository.CheckAndFillLanguages(mandatoryLanguages);
        }
    }
}
