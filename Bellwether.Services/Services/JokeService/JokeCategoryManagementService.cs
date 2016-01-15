using System.Collections.Generic;
using System.Linq;
using Bellwether.Models.Models;
using Bellwether.Repositories.Entities;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.JokeService
{
    public interface IJokeCategoryManagementService
    {
        bool ValidateAndFillJokeCategories(JokeCategory[] mandatoryJokeCategories);
    }
    public class JokeCategoryManagementService: IJokeCategoryManagementService
    {
        public bool ValidateAndFillJokeCategories(JokeCategory[] mandatoryJokeCategories)
        {
            if (mandatoryJokeCategories == null)
                return false;
            List<JokeCategoryDao> localJokeCategories = RepositoryFactory.Context.JokeCategories.ToList();
            BellwetherLanguageDao localLanguage =
                RepositoryFactory.Context.BellwetherLanguages.FirstOrDefault(
                    x => x.Id == mandatoryJokeCategories.First().LanguageId);
            if (localLanguage == null)
                return false;
            if (!localJokeCategories.Any())
                return InsertJokeCategoriesAndSave(mandatoryJokeCategories,localLanguage);
            InsertJokeCategoryIfNotExistsOnLocalList(mandatoryJokeCategories,localJokeCategories,localLanguage);
            RemoveJokeCategoryIfNotExistsOnMandatoryList(mandatoryJokeCategories,localJokeCategories);
            RepositoryFactory.Context.SaveChanges();
            return true;
        }

        private bool InsertJokeCategoriesAndSave(JokeCategory[] mandatoryJokeCategories,BellwetherLanguageDao language)
        {
            List<JokeCategoryDao> jokeCategories = new List<JokeCategoryDao>();
            mandatoryJokeCategories.ToList().ForEach(x =>
            {
                jokeCategories.Add(new JokeCategoryDao
                {
                    Id = x.Id,
                    Language = language,
                    JokeCategoryName = x.JokeCategoryName
                });
            });
            RepositoryFactory.Context.JokeCategories.AddRange(jokeCategories);
            RepositoryFactory.Context.SaveChanges();
            return true;
        }

        private void InsertJokeCategoryIfNotExistsOnLocalList(JokeCategory[] mandatoryJokeCategories,List<JokeCategoryDao> localJokeCategories, BellwetherLanguageDao language)
        {
            mandatoryJokeCategories.ToList().ForEach(x =>
            {
                if (localJokeCategories.FirstOrDefault(local => local.Id == x.Id && local.Language.Id == language.Id) == null)
                    RepositoryFactory.Context.JokeCategories.Add(new JokeCategoryDao { Id= x.Id,JokeCategoryName = x.JokeCategoryName,Language = language});
            });
        }

        private void RemoveJokeCategoryIfNotExistsOnMandatoryList(JokeCategory[] mandatoryJokeCategories, List<JokeCategoryDao> localJokeCategories)
        {
            localJokeCategories.ForEach(localJokeCategory =>
            {
                if (
                    mandatoryJokeCategories.ToList()
                        .FirstOrDefault(mandatoryJokeCategory => mandatoryJokeCategory.Id == localJokeCategory.Id && mandatoryJokeCategory.LanguageId == localJokeCategory.Language.Id) ==
                    null)
                    RepositoryFactory.Context.JokeCategories.Remove(localJokeCategory);
            });
        }
    }
}


