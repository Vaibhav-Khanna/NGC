using NGC.DataModels;
using NGC.DataStore.Implementation.Stores;
using NGC.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NGC.DataStore.Abstraction.Stores
{
    public interface IContactStore : IBaseStore<Contact>
    {

        Task<IEnumerable<Contact>> SearchCompany(string query);

        Task<IEnumerable<Contact>> GetValidContactsWithLocation();

       
    }
}
