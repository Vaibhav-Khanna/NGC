using System;
using NGC.DataStore.Abstraction.Stores;
using NGC.Models.DataObjects;

namespace NGC.DataStore.Implementation.Stores
{
    public class CollectSourceStore : BaseStore<CollectSource>, ICollectSourceStore
    {
        public override string Identifier => "CollectSource";
    }
}
