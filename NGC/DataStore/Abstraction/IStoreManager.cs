using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NGC.DataStore.Abstraction
{
    public interface IStoreManager
    {
        Task<object> LoginAsync(string email, string password);

        void LogoutAsync();
      
        Task<object> RegisterAsync(string email, string password,string deviceLanguage);

        Task<bool> RequestNewPassword(string username,string password);
       
        Task<bool> RegenerateToken();
    }
}
