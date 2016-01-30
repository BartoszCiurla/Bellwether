using System;
using System.Threading.Tasks;
using Windows.Storage;
using Bellwether.Services.Utility;
using Microsoft.Data.Entity;

namespace Bellwether.Services.Services.ResourceService
{
    public interface IInitResourceService
    {
        Task<bool> Initiate();
    }
    public class InitiateResourceService : IInitResourceService
    {
        private readonly string _staticApplicationResourcesFile;
        private readonly string _staticLanguageResourcesFile;
        private readonly string _localApplicationResourcesFile;
        private readonly string _localLanguageResourcesFile;
        private readonly string _localResourcesFolder;


        public InitiateResourceService()
        {
            _staticApplicationResourcesFile = ResourcesFiles.StaticApplicationResourcesFile;
            _staticLanguageResourcesFile = ResourcesFiles.StaticLanguageResourcesFile;
            _localApplicationResourcesFile = ResourcesFiles.LocalApplicationResourcesFile;
            _localLanguageResourcesFile = ResourcesFiles.LocalLanguageResourcesFile;
            _localResourcesFolder = ResourcesFiles.LocalResourcesFolderName;
        }
        public async Task<bool> Initiate()
        {
            RepositoryFactory.Context.Database.Migrate();
            await InitApplicationResources(_localResourcesFolder, _localApplicationResourcesFile, _staticApplicationResourcesFile);
            await
                InitLanguageResources(_localResourcesFolder, _localLanguageResourcesFile, _staticLanguageResourcesFile);
            return true;
        }

        private async Task InitLanguageResources(string localFolderName, string langResourcesFile,
            string staticLangResourcesFile)
        {
            await GetLocalFile(localFolderName, langResourcesFile, staticLangResourcesFile);
        }
        private async Task InitApplicationResources(string localFolderName, string appResourcesFile, string staticAppResourcesFile)
        {
                await GetLocalFile(localFolderName, appResourcesFile, staticAppResourcesFile);
        }
        private async Task GetLocalFile(string folderName, string fileName, string staticFileLocation)
        {
                //to zdecydowanie nie jest ostateczna wersja , na czas produkcyjny styknie
                var dataFolder = await ApplicationData.Current.LocalFolder.TryGetItemAsync(folderName) as StorageFolder ??
                            await ApplicationData.Current.LocalFolder.CreateFolderAsync(folderName);
                var localFile = await dataFolder.TryGetItemAsync(fileName) as StorageFile;
            if (localFile == null)
            {
                // te ify dopiero w wersji produkcyjnej 
                StorageFile storageStaticFile = GetDataFromStaticFile(staticFileLocation);
                var file = await dataFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                string content = await FileIO.ReadTextAsync(storageStaticFile);
                await FileIO.WriteTextAsync(file, content);
            }
        }
        private StorageFile GetDataFromStaticFile(string staticFileLocation)
        {
            StorageFile staticFile = null;
            Uri appUri = new Uri(staticFileLocation);
            staticFile = StorageFile.GetFileFromApplicationUriAsync(appUri).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
            return staticFile;
        }
    }
}
