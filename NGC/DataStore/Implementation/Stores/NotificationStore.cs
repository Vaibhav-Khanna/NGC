using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NGC.DataStore.Abstraction.Stores;
using NGC.Models.DataObjects;

namespace NGC.DataStore.Implementation.Stores
{
    public class NotificationStore : BaseStore<Notification>, INotificationStore
    {
        public override string Identifier => "Notification";

        public async Task<IEnumerable<Notification>> GetNotificationsByUserId(string id)
        {
            try
            {
                await InitializeStore().ConfigureAwait(false);

                await PullLatestAsync().ConfigureAwait(false);

                return await Table.Where(arg => arg.UserId == id).OrderByDescending(arg => arg.DatabaseInsertAt).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {

            }

            return null;

        }
    }
}
