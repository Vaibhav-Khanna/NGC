using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NGC.Helpers.PopUps;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace NGC.Helpers
{
    public class ToastService : IToastService
    {

       


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


    }
}
