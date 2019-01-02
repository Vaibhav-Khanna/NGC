using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FreshMvvm;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using NGC.DataStore.Abstraction;
using NGC.DataStore.Abstraction.Stores;
using NGC.Models.DataObjects;

namespace NGC.DataStore.Implementation.Stores
{
    public class ContactStore : BaseStore<Contact>, IContactStore
    {
        public override string Identifier => "Contact";

        public async Task<IEnumerable<Contact>> GetItemsByTypeAsync(string ContactType, string Filter = null, bool forceRefresh = false) {

            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh) {  await PullLatestAsync().ConfigureAwait(false); }
            if (!String.IsNullOrEmpty(Filter))
            {
                return await Table.Where(x => (((x.Firstname.ToLower().Contains(Filter)) || (x.Lastname.ToLower().Contains(Filter)) || (x.Email.ToLower().Contains(Filter)) || (x.Phone.ToLower().Contains(Filter))) && (x.Qualification == ContactType))).OrderBy(x => x.Lastname).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            }
            else
            {
                return await Table.Where(x => (x.Qualification == ContactType)).OrderByDescending(x => x.ContactCreatedAt).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Contact>> GetItemsFilterAsync(string Filter = null, bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh) { await PullLatestAsync().ConfigureAwait(false); }
          
            if (!String.IsNullOrEmpty(Filter))
            {
                return await Table.Where(x => ((x.Firstname.ToLower().Contains(Filter)) || (x.Lastname.ToLower().Contains(Filter)) || (x.Email.ToLower().Contains(Filter)) || (x.Phone.ToLower().Contains(Filter)))).OrderBy(x => x.Lastname).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else
            {
                return await Table.OrderByDescending(x => x.Lastname).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Contact>> GetValidContactsWithLocation()
        {
            try
            {
                await InitializeStore().ConfigureAwait(false);

                return await Table.Where(x => x.Latitude != 0 && x.Longitude != 0).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {

            }

            return null;
        }

        public async Task<IEnumerable<Contact>> SearchCompany(string query)
        {
            try
            {
                await InitializeStore().ConfigureAwait(false);

                if (!String.IsNullOrWhiteSpace(query))
                {
                    return await Table.Where(x => x.Firstname.ToLower().Contains(query.ToLower()) || x.Lastname.ToLower().Contains(query.ToLower())).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                }
            }
            catch (Exception)
            {

            }

            return null;
        }



       
    }

    public class JObjectManualQuery
    {
        public IEnumerable<Contact> results { get; set; }
        public double count { get; set; }
    }
}