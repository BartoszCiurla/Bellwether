using Windows.Storage;
using Bellwether.Repositories.Context;
using Bellwether.Repositories.Repositories;
using Microsoft.Data.Entity;

namespace Bellwether.Services.Utility
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

        //private static StorageFolder _localFolder;
        //public static StorageFolder LocalFolder => _localFolder ?? (_localFolder = ApplicationData.Current.LocalFolder);

        private static IResourceRepository _applicationResourceRepository;
        public static IResourceRepository ApplicationResourceRepository
            =>
                _applicationResourceRepository ??
                (_applicationResourceRepository = new ResourceRepository(ResourcesFiles.LocalApplicationResourcesFile,
                    ResourcesFiles.LocalResourcesFolderName));

        private static IResourceRepository _languageResourceRepository;
        public static IResourceRepository LanguageResourceRepository
            =>
                _languageResourceRepository ??
                (_languageResourceRepository = new ResourceRepository(ResourcesFiles.LocalLanguageResourcesFile,
                    ResourcesFiles.LocalResourcesFolderName));
    }
}
