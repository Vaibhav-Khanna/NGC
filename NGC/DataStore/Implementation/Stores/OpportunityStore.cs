using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NGC.DataStore.Abstraction.Stores;
using NGC.Models.DataObjects;

namespace NGC.DataStore.Implementation.Stores
{
    public class OpportunityStore : BaseStore<Opportunity>, IOpportunityStore
    {
        public override string Identifier => "Opportunity";


        public async Task<IEnumerable<Opportunity>> GetOpportunitiesByContactId(string id)
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
