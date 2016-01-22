using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Bellwether.Models.ViewModels;
using Bellwether.Services.ExtensionMethods;

namespace Bellwether.Services.Utility
{
    public static class ServiceExecutor
    {
        public static ResponseViewModel<TResponse> Execute<TResponse>(Func<TResponse> serviceExecution)
        {
            try
            {
                var response = serviceExecution();

                return new ResponseViewModel<TResponse>().Valid(response);
            }
            catch (Exception e)
            {
                return new ResponseViewModel<TResponse>().Invalid(e.Message);
            }
        }

        public static async Task<ResponseViewModel<TResponse>> ExecuteAsync<TResponse>(Func<Task<TResponse>> serviceExecution)
        {
            try
            {
                var response = await serviceExecution();
                return new ResponseViewModel<TResponse>().Valid(response);
            }
            catch (Exception e)
            {
                await new MessageDialog(e.Message).ShowAsync();
                return new ResponseViewModel<TResponse>().Invalid(e.Message);
            }
        }

        public static async Task<ResponseViewModel<TResponse>> ExecuteAsyncIfSyncData<TResponse>(
            Func<Task<TResponse>> serviceExecution)
        {
            try
            {
                bool isSyncData = Convert.ToBoolean(
                    await RepositoryFactory.ApplicationResourceRepository.GetValueForKey("SynchronizeData"));
                if (!isSyncData)
                    throw new Exception(await RepositoryFactory.LanguageResourceRepository.GetValueForKey("DataSynchronizationSecurityDescription"));
                var response = await serviceExecution();
                return new ResponseViewModel<TResponse>().Valid(response);
            }
            catch (Exception e)
            {
                await new MessageDialog(e.Message).ShowAsync();
                return new ResponseViewModel<TResponse>().Invalid(e.Message);
            }
        }
    }
}
