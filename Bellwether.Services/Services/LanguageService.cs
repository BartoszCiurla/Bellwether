using System;
using System.Collections.Generic;
using System.Linq;
using Bellwether.Models.Models;
using Bellwether.Repositories.Entities;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services
{
    public interface ILanguageService
    {
        BellwetherLanguage[] GetLocalLanguages();
        bool ValidateAndFillLanguages(BellwetherLanguageDao[] mandatoryLanguages);
        BellwetherLanguage GetLocalLanguageByShortName(string languageShortName);
    }
    public class LanguageService : ILanguageService
    {
        public BellwetherLanguage[] GetLocalLanguages()
        {
            return
                ModelMapper.Map<BellwetherLanguage[], BellwetherLanguageDao[]>(
                    RepositoryFactory.Context.BellwetherLanguages.ToArray());
        }
        public bool ValidateAndFillLanguages(BellwetherLanguageDao[] mandatoryLanguages)
        {
            if (mandatoryLanguages == null)
                return false;
            List<BellwetherLanguageDao> localLanguages = RepositoryFactory.Context.BellwetherLanguages.ToList();
            if (!localLanguages.Any())
                return InsertLanguagesAndSave(mandatoryLanguages);            
            InsertLanguageIfNotExistsOnLocalList(mandatoryLanguages, localLanguages);
            DeleteLanguageIfNotExistsOnMandatoryList(mandatoryLanguages, localLanguages);
            RepositoryFactory.Context.SaveChanges();
            return true;
        }

        public BellwetherLanguage GetLocalLanguageByShortName(string languageShortName)
        {
            var localLanguage = ModelMapper.Map<BellwetherLanguage, BellwetherLanguageDao>(RepositoryFactory.Context.BellwetherLanguages.FirstOrDefault(
                    x => x.LanguageShortName == languageShortName));
            if(localLanguage == null)
                throw new Exception();
            return localLanguage;
        }
        private bool InsertLanguagesAndSave(BellwetherLanguageDao[] mandatoryLanguages)
        {
            RepositoryFactory.Context.BellwetherLanguages.AddRange(mandatoryLanguages);
            RepositoryFactory.Context.SaveChanges();
            return true;
        }
        private void InsertLanguageIfNotExistsOnLocalList(BellwetherLanguageDao[] mandatoryLanguages, List<BellwetherLanguageDao> localLanguages)
        {
            mandatoryLanguages.ToList().ForEach(mandatoryLanguage =>
            {
                if (localLanguages.FirstOrDefault(localLanguage => localLanguage.Id == mandatoryLanguage.Id) == null)
                    RepositoryFactory.Context.BellwetherLanguages.Add(mandatoryLanguage);
            });
        }

        private void DeleteLanguageIfNotExistsOnMandatoryList(BellwetherLanguageDao[] mandatoryLanguages, List<BellwetherLanguageDao> localLanguages)
        {
            localLanguages.ForEach(localLanguage =>
            {
                if (
                    mandatoryLanguages.ToList()
                        .FirstOrDefault(mandatoryLanguage => mandatoryLanguage.Id == localLanguage.Id) == null)
                    RepositoryFactory.Context.BellwetherLanguages.Remove(localLanguage);
            });
        }

    }
}
