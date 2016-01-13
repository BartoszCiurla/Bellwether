using System;
using Windows.UI.Popups;
using Bellwether.Models.ViewModels;

namespace Bellwether.ExtensionMethods
{
    public static class ResponseExtensionMethods
    {
        public static ResponseViewModel<TModel> Valid<TModel>(this ResponseViewModel<TModel> response, TModel data)
        {
            return new ResponseViewModel<TModel>()
            {
                Data = data,
                IsValid = true
            };
        }

        public static ResponseViewModel<TModel> Invalid<TModel>(this ResponseViewModel<TModel> response, string errorMessage)
        {
            return new ResponseViewModel<TModel>()
            {
                IsValid = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
