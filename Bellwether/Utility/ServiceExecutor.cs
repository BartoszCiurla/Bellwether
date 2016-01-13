using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Bellwether.ExtensionMethods;
using Bellwether.Models.ViewModels;

namespace Bellwether.Utility
{
    public static class ServiceExecutor
    {
        public static async Task<ResponseViewModel<TResponse>> ExecuteAsync<TResponse>(Func<Task<TResponse>> serviceExecution)
        {
            try
            {
                var response = await serviceExecution();
                return new ResponseViewModel<TResponse>().Valid(response);
            }
            catch (Exception e)
            {
                await new MessageDialog(e.ToString()).ShowAsync();
                return new ResponseViewModel<TResponse>().Invalid(e.Message);
            }
        }

        public static async Task<ResponseViewModel<TResponse>> ExecuteAsyncIfCan<TResponse>(
            Func<Task<TResponse>> serviceExecution)
        {
            try
            {
                var response = await serviceExecution();
                return new ResponseViewModel<TResponse>().Valid(response);
            }
            catch (Exception e)
            {
                return new ResponseViewModel<TResponse>().Invalid(e.Message);
            }
        }
    }
}
