using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace Bellwether.Repositories.Repositories
{
    public interface IResourceRepository
    {
        Task<string> GetValueForKey(string key);
        Task<bool> SaveValueForKey(string key, string value);
        Task<Dictionary<string, string>> GetSelectedKeysValues(IEnumerable<string> keys);
        Task<bool> SaveValuesAndKays(Dictionary<string, string> resources);
        Task<bool> SaveSelectedValues(Dictionary<string, string> resources);
        Task<Dictionary<string, string>> GetAll();
    }
    public class ResourceRepository : IResourceRepository
    {
        private readonly string _fileName;
        private readonly string _localResourceFolderName;
        private readonly SemaphoreSlim _mutex = new SemaphoreSlim(1);

        private async Task<StorageFile> GetLocalFile()
        {
            var dataFolder = await ApplicationData.Current.LocalFolder.TryGetItemAsync(_localResourceFolderName) as StorageFolder;
            if (dataFolder != null)
                return await dataFolder.TryGetItemAsync(_fileName) as StorageFile;
            throw new Exception();
        }      

        public ResourceRepository(string fileName, string resourcesFolderName)
        {
            _fileName = fileName;
            _localResourceFolderName = resourcesFolderName;
        }
        public async Task<string> GetValueForKey(string key)
        {
            string content = await FileIO.ReadTextAsync(await GetLocalFile());
            dynamic jsonObj = JsonConvert.DeserializeObject(content);            
            return jsonObj[key];
        }

        public async Task<bool> SaveValueForKey(string key, string value)
        {
            StorageFile localFile = await GetLocalFile();
            string content = await
                FileIO.ReadTextAsync(localFile);
            dynamic jsonObj = JsonConvert.DeserializeObject(content);            
            jsonObj[key] = value;
            await SaveAsync(localFile, JsonConvert.SerializeObject(jsonObj));            
            return true;
        }
        private async Task SaveAsync(StorageFile localFile,string content)
        {
            await _mutex.WaitAsync();
            try
            {
                await FileIO.WriteTextAsync(localFile, content);
            }
            finally
            {
                _mutex.Release();
            }
        }
        public async Task<Dictionary<string, string>> GetSelectedKeysValues(IEnumerable<string> keys)
        {
            string content = await
               FileIO.ReadTextAsync(await GetLocalFile());
            Dictionary<string, string> localResource = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            Dictionary<string, string> scenario = new Dictionary<string, string>();
            keys.ToList().ForEach(key =>
            {
                var searchItem = localResource.FirstOrDefault(z => z.Key == key);
                if (searchItem.Key != null)
                    scenario.Add(searchItem.Key, searchItem.Value);
            });            
            return scenario;
        }
        public async Task<bool> SaveValuesAndKays(Dictionary<string, string> resources)
        {            
            await SaveAsync(await GetLocalFile(), JsonConvert.SerializeObject(resources));
            return true;
        }

        public async Task<bool> SaveSelectedValues(Dictionary<string, string> resources)
        {
            var localFile = await GetLocalFile();
            string content = await
            FileIO.ReadTextAsync(localFile);
            var localResource = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            resources.ToList().ForEach(x =>
            {
                localResource[x.Key] = x.Value;
            });
            await SaveAsync(localFile, JsonConvert.SerializeObject(localResource));
            return true;
        }

        public async Task<Dictionary<string, string>> GetAll()
        {
            string content = await
            FileIO.ReadTextAsync(await GetLocalFile());
            var localResource = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            return localResource;
        }      
    }
}
