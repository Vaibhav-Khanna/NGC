using System;
using NGC.Models.DataObjects;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NGC.DataStore.Abstraction.Stores
{
    public interface INotificationStore : IBaseStore<Notification>
    {

        Task<IEnumerable<Notification>> GetNotificationsByUserId(string id);
    }
}
