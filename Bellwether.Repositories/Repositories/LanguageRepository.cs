using System;
using System.Collections.Generic;
using System.Linq;
using Bellwether.Models.Entities;

namespace Bellwether.Repositories.Repositories
{
    public interface ILanguageRepository
    {
        IEnumerable<BellwetherLanguageDao> GetLanguages();
        bool CheckAndFillLanguages(IEnumerable<BellwetherLanguageDao> mandatoryLanguages);
    }
    public class LanguageRepository : ILanguageRepository
    {
        private readonly IGenericRepository<BellwetherLanguageDao> _repository;
        private IEnumerable<BellwetherLanguageDao> _localLanguages;

        public LanguageRepository(IGenericRepository<BellwetherLanguageDao> repository)
        {
            _repository = repository;
        }

        public IEnumerable<BellwetherLanguageDao> GetLanguages()
        {
            return _repository.GetAll();
        }

        public bool CheckAndFillLanguages(IEnumerable<BellwetherLanguageDao> mandatoryLanguages)
        {
            if (mandatoryLanguages == null)
                return false;
            try
            {
                var bellwetherLanguages = mandatoryLanguages as BellwetherLanguageDao[] ?? mandatoryLanguages.ToArray();
                _localLanguages = GetLanguages();
                //do usuwania gówna 
                //_localLanguages.ToList().ForEach(x =>
                //{
                //    _repository.Delete(x);
                //});
                //_repository.Save();

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

        private void InsertLanguageIfNotExistsOnLocalList(IEnumerable<BellwetherLanguageDao> mandatoryLanguages)
        {
            mandatoryLanguages.ToList().ForEach(mandatoryLanguage =>
            {
                if (_localLanguages.ToList().FirstOrDefault(localLanguage => localLanguage.Id == mandatoryLanguage.Id) == null)
                    InsertLanguage(mandatoryLanguage);
            });
        }

        private void DeleteLanguageIfNotExistsOnMandatoryList(IEnumerable<BellwetherLanguageDao> mandatoryLanguages)
        {
            _localLanguages.ToList().ForEach(localLanguage =>
            {
                if (mandatoryLanguages.ToList().FirstOrDefault(mandatoryLanguage => mandatoryLanguage.Id == localLanguage.Id) == null)
                    DeleteLanguage(localLanguage);
            });
        }

        private bool InsertLanguagesAndSave(IEnumerable<BellwetherLanguageDao> mandatoryLanguages)
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

        private void InsertLanguage(BellwetherLanguageDao mandatoryLanguage)
        {
            _repository.Insert(mandatoryLanguage);
        }

        private void DeleteLanguage(BellwetherLanguageDao localLanguage)
        {
            _repository.Delete(localLanguage);
        }
    }
}
