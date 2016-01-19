using System.Collections.Generic;
using System.Linq;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.Repositories.Entities;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.JokeService
{
    public interface IJokeCategoryManagementService
    {
        bool ValidateAndFillJokeCategories(JokeCategoryViewModel[] mandatoryJokeCategories);
    }
    public class JokeCategoryManagementService: IJokeCategoryManagementService
    {
        public bool ValidateAndFillJokeCategories(JokeCategoryViewModel[] mandatoryJokeCategories)
        {
            if (mandatoryJokeCategories == null)
                return false;
            BellwetherLanguageDao localLanguage =
                RepositoryFactory.Context.BellwetherLanguages.FirstOrDefault(
                    x => x.Id == mandatoryJokeCategories.First().LanguageId);
            if (localLanguage == null)
                return false;
            InsertJokeCategories(mandatoryJokeCategories,localLanguage);        
            return true;
        }     

        private void InsertJokeCategories(JokeCategoryViewModel[] mandatoryJokeCategories,BellwetherLanguageDao language)
        {
            List<JokeCategoryDao> localJokeCategories = RepositoryFactory.Context.JokeCategories.ToList();
            mandatoryJokeCategories.ToList().ForEach(x =>
            {
                var localJokeCategory = localJokeCategories.FirstOrDefault(local => local.Id == x.Id);
                if (localJokeCategory == null)
                    RepositoryFactory.Context.JokeCategories.Add(new JokeCategoryDao { Id= x.Id,JokeCategoryName = x.JokeCategoryName,Language = language});
                else
                {
                    localJokeCategory.JokeCategoryName = x.JokeCategoryName;
                    localJokeCategory.Language = language;
                    RepositoryFactory.Context.JokeCategories.Update(localJokeCategory);
                }
            });
            RepositoryFactory.Context.SaveChanges();
        }       
    }
}


