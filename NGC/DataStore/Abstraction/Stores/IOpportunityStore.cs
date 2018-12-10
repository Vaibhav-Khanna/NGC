using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NGC.Models.DataObjects;

namespace NGC.DataStore.Abstraction.Stores
{
    public interface IOpportunityStore : IBaseStore<Opportunity>
    {
        Task<IEnumerable<Opportunity>> GetOpportunitiesByContactId(string id);
    }
}
