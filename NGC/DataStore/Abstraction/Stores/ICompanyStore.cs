using System;
using NGC.Models.DataObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NGC.DataStore.Abstraction.Stores
{
    public interface ICompanyStore : IBaseStore<Company>
    {
        Task<IEnumerable<OpenDataResponse.Record>> SearchCompanyFromOpenData(string searchText);

        Task<IEnumerable<Company>> SearchCompany(string searchText);
    }
}
