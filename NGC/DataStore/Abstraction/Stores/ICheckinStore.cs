using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NGC.Models.DataObjects;

namespace NGC.DataStore.Abstraction.Stores
{
    public interface ICheckinStore : IBaseStore<Checkin>
    {
        Task<IEnumerable<Checkin>> GetCheckinsByContactId(string id);
    }
}
