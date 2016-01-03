using Windows.Storage;
using Bellwether.Models.Entities;
using Bellwether.Repositories.Context;
using Bellwether.Repositories.Repositories;
using Microsoft.Data.Entity;

namespace Bellwether.Repositories.Factories
{
    public static class RepositoryFactory
    {
        private static BellwetherDbContext _context;
        public static BellwetherDbContext Context => _context ?? (_context = Initialize());
        private static BellwetherDbContext Initialize()
        {
            var context = new BellwetherDbContext();
            context.Database.OpenConnection();
            return context;
        }

        private static StorageFolder _localFolder;
        private static StorageFolder LocalFolder => _localFolder ?? (_localFolder = ApplicationData.Current.LocalFolder);         

        private static ILanguageRepository _languageRepository;
        public static ILanguageRepository LanguageRepository
            =>
                _languageRepository ??
                (_languageRepository = new LanguageRepository(new GenericRepository<BellwetherLanguageDao>()));

        private static IJokeCategoryRepository _jokeCategoryRepository;
        public static IJokeCategoryRepository JokeCategoryRepository
            =>
                _jokeCategoryRepository ??
                (_jokeCategoryRepository = new JokeCategoryRepository(new GenericRepository<JokeCategoryDao>()));

        private static IJokeRepository _jokeRepository;
        public static IJokeRepository JokeRepository
            => _jokeRepository ?? (_jokeRepository = new JokeRepository(new GenericRepository<JokeDao>()));

        private static IResourceRepository _appResourceRepository;
        public static IResourceRepository AppResourceRepository
            =>
                _appResourceRepository ??
                (_appResourceRepository =
                    new ResourceRepository(ResourcesFiles.LocalApplicationResourcesFile,
                        ResourcesFiles.LocalResourcesFolderName, LocalFolder));

        private static IResourceRepository _langResourceRepository;
        public static IResourceRepository LangResourceRepository
            =>
                _langResourceRepository ??
                (_langResourceRepository =
                    new ResourceRepository(ResourcesFiles.LocalLanguageResourcesFile,
                        ResourcesFiles.LocalResourcesFolderName, LocalFolder));
    }
}
