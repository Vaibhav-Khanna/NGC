using System;
using NGC.DataStore.Abstraction.Stores;
using NGC.Models.DataObjects;

namespace NGC.DataStore.Implementation.Stores
{
    public class SalesTeamStore : BaseStore<SalesTeam>, ISalesTeamStore
    {
        public override string Identifier => "SalesTeam";
    }
}
