using System;
using NGC.DataStore.Abstraction.Stores;
using NGC.Models.DataObjects;

namespace NGC.DataStore.Implementation.Stores
{
    public class CheckinTypeStore : BaseStore<CheckinType>, ICheckinTypeStore
    {
        public override string Identifier => "CheckinType";
    }
}
