using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;

namespace Bellwether.Services.Services
{
    public interface IInitResourceService
    {
        Task Init();
    }
    public class InitResourceService : IInitResourceService
    {
        private readonly string _staticApplicationResourcesFile;
        private readonly string _staticLanguageResourcesFile;
        private readonly string _localApplicationResourcesFile;
        private readonly string _localLanguageResourcesFile;
        private readonly string _localResourcesFolder;

        private readonly StorageFolder _localFolder;

        //private Dictionary<string, string> _applicationResources;
        //private Dictionary<string, string> _languageResources;

        public InitResourceService()
        {
            _localFolder = ApplicationData.Current.LocalFolder;
            _staticApplicationResourcesFile = ResourcesFiles.StaticApplicationResourcesFile;
            _staticLanguageResourcesFile = ResourcesFiles.StaticLanguageResourcesFile;
            _localApplicationResourcesFile = ResourcesFiles.LocalApplicationResourcesFile;
            _localLanguageResourcesFile = ResourcesFiles.LocalLanguageResourcesFile;
            _localResourcesFolder = ResourcesFiles.LocalResourcesFolderName;
        }
        public async Task Init()
        {
            await InitApplicationResources(_localResourcesFolder, _localApplicationResourcesFile, _staticApplicationResourcesFile);
            await
                InitLanguageResources(_localResourcesFolder, _localLanguageResourcesFile, _staticLanguageResourcesFile);
        }

        private async Task InitLanguageResources(string localFolderName, string langResourcesFile,
            string staticLangResourcesFile)
        {
            try
            {
                //IStorageFile file =
                await GetLocalFile(localFolderName, langResourcesFile, staticLangResourcesFile);
                //string text = FileIO.ReadTextAsync(file).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
                //_languageResources = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
            }
            catch (Exception)
            {
                //tu bedzie logowanie
            }
        }
        private async Task InitApplicationResources(string localFolderName, string appResourcesFile, string staticAppResourcesFile)
        {
            try
            {
                await GetLocalFile(localFolderName, appResourcesFile, staticAppResourcesFile);
                //string text = FileIO.ReadTextAsync(file).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
                //_applicationResources = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
            }
            catch (Exception)
            {
                //tu bedzie logowanie
            }

        }
        private async Task GetLocalFile(string folderName, string fileName, string staticFileLocation)
        {
            try
            {
                var dataFolder = await _localFolder.TryGetItemAsync(folderName) as StorageFolder ??
                            await _localFolder.CreateFolderAsync(folderName);
                var localFile = await dataFolder.TryGetItemAsync(fileName) as StorageFile;
                //if (localFile == null)
                //{ // te ify dopiero w wersji produkcyjnej 
                    StorageFile storageStaticFile = GetDataFromStaticFile(staticFileLocation);
                    await storageStaticFile.CopyAsync(dataFolder);
                
                //}
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //tutaj bedzie logowanie
            }
        }
        private StorageFile GetDataFromStaticFile(string staticFileLocation)
        {
            StorageFile staticFile = null;
            try
            {
                Uri appUri = new Uri(staticFileLocation);
                staticFile = StorageFile.GetFileFromApplicationUriAsync(appUri).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                //tu bedzie logowanei
            }
            return staticFile;
        }
    }
}
