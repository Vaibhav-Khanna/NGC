using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NGC.DataStore.Abstraction.Stores;
using NGC.Models.DataObjects;

namespace NGC.DataStore.Implementation.Stores
{
    public class NoteStore : BaseStore<Note>, INoteStore
    {
        public override string Identifier => "Note";

        public async Task<IEnumerable<Note>> GetAllReminders()
        {
            try
            {
                await InitializeStore().ConfigureAwait(false);

                await PullLatestAsync().ConfigureAwait(false);

                return await Table.Where(arg => arg.Kind == "note" && arg.ReminderAt != null).OrderBy( arg => arg.ReminderAt ).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {

            }

            return null;
        }

        public async Task<IEnumerable<Note>> GetRemindersByContactId(string id)
        {
            try
            {
                await InitializeStore().ConfigureAwait(false);

                return await Table.Where(arg => arg.ContactId == id && arg.Kind == "note" && arg.ReminderAt != null ).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {

            }

            return null;
        }
    }
}
