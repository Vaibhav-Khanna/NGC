﻿using NGC.Models.DataObjects;
using System.Threading.Tasks;

namespace NGC.DataStore.Abstraction.Stores
{
    public interface IUserStore : IBaseStore<User>
    {
        Task<User> GetProfileAsync(string token);
    }
}
