using NGC.DataStore.Abstraction.Stores;
using NGC.Models.DataObjects;

namespace NGC.DataStore.Implementation.Stores
{
    public class ContactCustomFieldSourceStore : BaseStore<ContactCustomFieldSource>, IContactCustomFieldSourceStore
    {
        public override string Identifier => "ContactCustomFieldSource";

    }
}
