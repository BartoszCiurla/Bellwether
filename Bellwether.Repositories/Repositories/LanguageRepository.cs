using System;
using System.Collections.Generic;
using System.Linq;
using Bellwether.Models.Models;

namespace Bellwether.Repositories.Repositories
{
    public interface ILanguageRepository
    {
        IEnumerable<BellwetherLanguage> GetLanguages();
        bool CheckAndFillLanguages(IEnumerable<BellwetherLanguage> mandatoryLanguages);
    }
    public class LanguageRepository : ILanguageRepository
    {
        private readonly IGenericRepository<BellwetherLanguage> _repository;
        private IEnumerable<BellwetherLanguage> _localLanguages;

        public LanguageRepository(IGenericRepository<BellwetherLanguage> repository)
        {
            _repository = repository;
        }

        public IEnumerable<BellwetherLanguage> GetLanguages()
        {
            return _repository.GetAll();
        }

        public bool CheckAndFillLanguages(IEnumerable<BellwetherLanguage> mandatoryLanguages)
        {
            try
            {
                var bellwetherLanguages = mandatoryLanguages as BellwetherLanguage[] ?? mandatoryLanguages.ToArray();
                _localLanguages = GetLanguages();
                if (_localLanguages == null) return InsertLanguagesAndSave(bellwetherLanguages);

                InsertLanguageIfNotExistsOnLocalList(bellwetherLanguages);
                DeleteLanguageIfNotExistsOnMandatoryList(bellwetherLanguages);

                _repository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void InsertLanguageIfNotExistsOnLocalList(IEnumerable<BellwetherLanguage> mandatoryLanguages)
        {
            mandatoryLanguages.ToList().ForEach(mandatoryLanguage =>
            {
                if (_localLanguages.ToList().FirstOrDefault(localLanguage => localLanguage.Id == mandatoryLanguage.Id) == null)
                    InsertLanguage(mandatoryLanguage);
            });
        }

        private void DeleteLanguageIfNotExistsOnMandatoryList(IEnumerable<BellwetherLanguage> mandatoryLanguages)
        {
            _localLanguages.ToList().ForEach(localLanguage =>
            {
                if (mandatoryLanguages.ToList().FirstOrDefault(mandatoryLanguage => mandatoryLanguage.Id == localLanguage.Id) == null)
                    DeleteLanguage(localLanguage);
            });
        }

        private bool InsertLanguagesAndSave(IEnumerable<BellwetherLanguage> mandatoryLanguages)
        {
            try
            {
                _repository.InsertRange(mandatoryLanguages);
                _repository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void InsertLanguage(BellwetherLanguage mandatoryLanguage)
        {
            _repository.Insert(mandatoryLanguage);
        }

        private void DeleteLanguage(BellwetherLanguage localLanguage)
        {
            _repository.Delete(localLanguage);
        }
    }
}
