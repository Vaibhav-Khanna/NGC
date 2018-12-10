using System;
using NGC.DataStore.Abstraction.Stores;
using NGC.Models.DataObjects;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NGC.DataStore.Implementation.Stores
{
    public class CheckinStore : BaseStore<Checkin>, ICheckinStore
    {
        public override string Identifier => "Checkin";


        public async Task<IEnumerable<Checkin>> GetCheckinsByContactId(string id)
        {
            try
            {
                await InitializeStore().ConfigureAwait(false);

                return await Table.Where(arg => arg.ContactId == id).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {

            }

            return null;
        }
    }
}
