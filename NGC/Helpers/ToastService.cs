using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGC.Helpers.PopUps;
using NGC.Models.DataObjects;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace NGC.Helpers
{
    public class ToastService : IToastService
    {

        public async void ShowLoading(string text = null)
        {
            var popupPage = new DialogLayout(text);

            if (PopupNavigation.Instance.PopupStack.Any())
            {
                await PopupNavigation.Instance.PopAllAsync();
            }

            await PopupNavigation.Instance.PushAsync(popupPage, false);
        }


        public async void HideLoading()
        {
            if (PopupNavigation.Instance.PopupStack.Any())
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }


        public async Task<string> ShowActionSheet(string Title, IEnumerable<Tuple<string, string>> Buttons, string Cancel)
        {
            var popupPage = new ActionSheetView(Title,Buttons,Cancel);

            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }

            await PopupNavigation.Instance.PushAsync(popupPage);

            await Task.Run(async() =>
            {
                while (popupPage.Result == null)
                {
                    await Task.Delay(500);
                }
            });

            return popupPage.Result;
        }

        public async Task<object> ShowSearchCompany()
        {

            var popupPage = new SearchCompanyPage() { BindingContext = new SearchCompanyPageModel() }; 

            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }


            await PopupNavigation.Instance.PushAsync(popupPage);


            await Task.Run(async () =>
            {
                while (popupPage.Result == null)
                {
                    await Task.Delay(500);
                }
            });

            return popupPage.Result;
        }

        public async Task<Contact> ShowSearchContact()
        {
            var popupPage = new SearchContactPage() { BindingContext = new SearchContactPageModel() };

            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }

            await PopupNavigation.Instance.PushAsync(popupPage);


            await Task.Run(async () =>
            {
                while (popupPage.Result == null)
                {
                    await Task.Delay(500);
                }
            });

            return popupPage.Result;
        }
    }
}
