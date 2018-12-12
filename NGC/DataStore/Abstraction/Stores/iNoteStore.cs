using System;
using NGC.Models.DataObjects;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NGC.DataStore.Abstraction.Stores
{
    public interface INoteStore : IBaseStore<Note>
    {
        Task<IEnumerable<Note>> GetRemindersByContactId(string id);
    }
}
