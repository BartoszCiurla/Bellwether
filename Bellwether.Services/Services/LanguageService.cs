using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellwether.Models.Entities;
using Bellwether.Repositories.Context;
using Bellwether.Repositories.Factories;
using Bellwether.Repositories.Repositories;
using Bellwether.Services.Factories;
using Bellwether.WebServices.WebServices;

namespace Bellwether.Services.Services
{
    public interface ILanguageService
    {
        IEnumerable<BellwetherLanguageDao> GetLocalLanguages();
        Task<Dictionary<string, string>> GetLanguageFile(string urlWithParams);
        Task<BellwetherLanguageDao> GetLanguage(string urlWithParams);
        Task<bool> CheckAndFillLanguages(string urlWithParams);
    }
    public class LanguageService : ILanguageService
    {
        private readonly IWebBellwetherLanguageService _service;
        private readonly ILanguageRepository _repository;
        public LanguageService()
        {
            _service = ServiceFactory.WebBellwetherLanguageService;
            _repository = RepositoryFactory.LanguageRepository;
        }

        public IEnumerable<BellwetherLanguageDao> GetLocalLanguages()
        {
            return _repository.GetLanguages();
        }

        public async Task<Dictionary<string, string>> GetLanguageFile(string urlWithParams)
        {
            return await _service.GetLanguageFile(urlWithParams);
        }

        public async Task<BellwetherLanguageDao> GetLanguage(string urlWithParams)
        {
            return await _service.GetLanguage(urlWithParams);
        }
        public async Task<bool> CheckAndFillLanguages(string urlWithParams)
        {
            var mandatoryLanguages = await _service.GetLanguages(urlWithParams);
            return  _repository.CheckAndFillLanguages(mandatoryLanguages);
        }
    }
}
