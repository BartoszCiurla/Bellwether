using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.Data.Json;
using Windows.Globalization;
using Windows.Storage;
using Bellwether.Repositories.Repositories;
using Newtonsoft.Json;

namespace Bellwether.Services.Services
{
    public static class ResourcesFiles
    {
        public const string StaticApplicationResourcesFile = "ms-appx:///Bellwether.Resources/StaticResources/ApplicationResources.json";
        public const string StaticLanguageResourcesFile = "ms-appx:///Bellwether.Resources/StaticResources/LanguageResources.json";
        public const string LocalApplicationResourcesFile = "ApplicationResources.json";
        public const string LocalLanguageResourcesFile = "LanguageResources.json";
        public const string LocalResourcesFolderName = "Resources";
    }

    public interface IResourceService
    {
        Task<string> TakeKeyValueFromLocalAppResources(string key);
    }
    public class ResourceService : IResourceService
    {
        private readonly ILocalResourceRepository _repository;
        private readonly string _appLocalResources;
        private readonly string _langLocalResources;
        private readonly string _localResourcesFile;
        public ResourceService()
        {
            _repository = new LocalResourceRepository();
            _appLocalResources = ResourcesFiles.LocalApplicationResourcesFile;
            _langLocalResources = ResourcesFiles.LocalLanguageResourcesFile;
            _localResourcesFile = ResourcesFiles.LocalResourcesFolderName;
        }

        public async Task<string> TakeKeyValueFromLocalAppResources(string key)
        {
            _repository.SpecifyTargetFile(_appLocalResources,_localResourcesFile);
            await _repository.Init();
            return await _repository.GetValueForKey(key);
        }
        public async Task<string> TakeKeyValueFromLocalLangResources(string key)
        {
            _repository.SpecifyTargetFile(_langLocalResources,_localResourcesFile);
            await _repository.Init();
            return await _repository.GetValueForKey(key);
        }

        public async Task<bool> ChangeLanguage(Dictionary<string, string> languageFile)
        {
            bool operationCompleted = await SaveLanguageFile(languageFile);
            if (operationCompleted)
            {
                
            }
            return operationCompleted;
        }

        private async Task<bool> SaveLanguageFile(Dictionary<string, string> languageFile)
        {
            bool operationCompleted = false;
            try
            {
                if (languageFile == null)
                    return false;
                await _repository.Init();
                _repository.SpecifyTargetFile(_langLocalResources,_localResourcesFile);
                await _repository.SaveValuesAndKays(languageFile);
                operationCompleted = true;
            }
            catch (Exception)
            {
                //tu bedzie logowanie
            }
            return operationCompleted;

        }
    }








    //to jest zeby było i tyle ... 
    public class SettingInfo
    {
        public string keyName { get; set; }
        public string keyValue { get; set; }

        public SettingInfo()
        {

        }
        public void Test2()
        {
            //string myResult = CustomJsonReader.GetKeyValue(ApplicationResourcesFile, "ApplicationLanguage");
            //Debug.WriteLine(myResult);
            //CustomJsonReader.SaveKeyValue(ApplicationResourcesFile, "ApplicationLanguage", "en");

        }
        public static async void GetApplicationResources()
        {

            bool fileExists;
            try
            {
                string fileName = "Resources/ApplicationResources.json";
                Uri appUri = new Uri("ms-appdata:///local/" + fileName);
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(appUri);
                fileExists = file != null;
            }
            catch (Exception e)
            {
                fileExists = false;
            }


            List<SettingInfo> lstContent = new List<SettingInfo>();
            try
            {

                /*  
                ApplicationData.Current.LocalFolder for storing local application data

                    Need to make sure using ConfigureAwait, GetAwaiter to avoid any file access errors
                    Once the file is opened can use ReadTextAsync again with GetAwaiver as snow below 
                    Conver the text to JsonArray and then deserialize into the object of our own format using DataContractJsonSerializer as below
                    For more details refer https://www.suchan.cz/2014/07/file-io-best-practices-in-windows-and-phone-apps-part-1-available-apis-and-file-exists-checking/
                */
                Uri appUri = new Uri(ResourcesFiles.StaticApplicationResourcesFile);//File name should be prefixed with 'ms-appx:///Assets/*
                StorageFile anjFile = StorageFile.GetFileFromApplicationUriAsync(appUri).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
                //var pasta = Windows.ApplicationModel.Package.Current.InstalledLocation;
                //StorageFile anjFile = await pasta.GetFileAsync(ApplicationResourcesFile);
                string jsonText = FileIO.ReadTextAsync(anjFile).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
                var jsonSerializer = new DataContractJsonSerializer(typeof(SettingInfo));
                JsonArray anjarray = JsonArray.Parse(jsonText);
                foreach (var jsonValue in anjarray)
                {
                    var oJsonVal = (JsonValue)jsonValue;
                    JsonObject oJsonObj = oJsonVal.GetObject();
                    using (MemoryStream jsonStream = new MemoryStream(Encoding.Unicode.GetBytes(oJsonObj.ToString())))
                    {
                        SettingInfo oContent = (SettingInfo)jsonSerializer.ReadObject(jsonStream);
                        lstContent.Add(oContent);
                    }
                }
            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp);
            }
        }
    }
}
