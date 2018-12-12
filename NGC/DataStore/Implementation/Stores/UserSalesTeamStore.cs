using System;
using NGC.DataStore.Abstraction.Stores;
using NGC.Models.DataObjects;

namespace NGC.DataStore.Implementation.Stores
{
    public class UserSalesTeamStore : BaseStore<UserSalesTeam>, IUserSalesTeamStore
    {
        public override string Identifier => "UserSalesTeam";
    }
}
