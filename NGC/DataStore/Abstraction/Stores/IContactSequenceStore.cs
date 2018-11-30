using NGC.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NGC.DataStore.Abstraction.Stores
{
    public interface IContactSequenceStore : IBaseStore<ContactSequence>
    {
        Task<bool> IsContactSequence(string contactId);
        Task<ContactSequence> SequenceInProgress(string contactId);
    }
}
