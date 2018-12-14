using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NGC.Models.DataObjects;

namespace NGC.Helpers.PopUps
{
    public interface IToastService
    {
        Task<string> ShowActionSheet(string Title, IEnumerable<Tuple<string, string>> Buttons, string Cancel);

        void ShowLoading(string text = null);

        void HideLoading();

        Task<object> ShowSearchCompany();

        Task<Contact> ShowSearchContact();
    }
}
