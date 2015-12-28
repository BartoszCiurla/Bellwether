using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace Bellwether.Repositories.Repositories
{
    public interface ILocalResourceRepository
    {
        Task<string> GetValueForKey(string key);
        Task SaveValueForKey(string key, string value);
        Task<Dictionary<string, string>> GetScenario(IEnumerable<string> keys);
        Task SaveValuesAndKays(Dictionary<string, string> resources);
        void SpecifyTargetFile(string fileName, string localResourceFolderName);
        Task Init();
    }
    public class LocalResourceRepository : ILocalResourceRepository
    {
        private string _fileName;
        private string _localResourceFolderName;
        private readonly StorageFolder _localFolder;
        private StorageFile _localFile;

        public void SpecifyTargetFile(string fileName, string localResourceFolderName)
        {
            _fileName = fileName;
            _localResourceFolderName = localResourceFolderName;
        }
        public LocalResourceRepository()
        {         
            _localFolder = ApplicationData.Current.LocalFolder;
        }

        public async Task Init()
        {
            var dataFolder = await _localFolder.TryGetItemAsync(_localResourceFolderName) as StorageFolder;
            if (dataFolder != null) _localFile = await dataFolder.TryGetItemAsync(_fileName) as StorageFile;
        }

        public async Task<string> GetValueForKey(string key)
        {
            string content = await FileIO.ReadTextAsync(_localFile);
            dynamic jsonObj = JsonConvert.DeserializeObject(content);
            return jsonObj[key];
        }

        public async Task SaveValueForKey(string key, string value)
        {
            string content = await
                FileIO.ReadTextAsync(_localFile);
            dynamic jsonObj = JsonConvert.DeserializeObject(content);
            jsonObj[key] = value;
            FileIO.WriteTextAsync(_localFile, JsonConvert.SerializeObject(jsonObj));
        }

        public async Task<Dictionary<string, string>> GetScenario(IEnumerable<string> keys)
        {
            string content = await
               FileIO.ReadTextAsync(_localFile);
            Dictionary<string,string> localResource = JsonConvert.DeserializeObject<Dictionary<string,string>>(content);
            Dictionary<string, string> scenario = new Dictionary<string, string>();
            keys.ToList().ForEach(key =>
            {
                var searchItem = localResource.FirstOrDefault(z => z.Key == key);
                if(searchItem.Key != null)
                scenario.Add(searchItem.Key, searchItem.Value);
            });
            return scenario;
        }

        public async Task SaveValuesAndKays(Dictionary<string, string> resources)
        {
            await FileIO.WriteTextAsync(_localFile, JsonConvert.SerializeObject(resources));
        }
    }
}
