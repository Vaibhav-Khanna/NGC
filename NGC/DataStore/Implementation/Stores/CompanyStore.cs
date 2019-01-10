using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NGC.DataStore.Abstraction.Stores;
using NGC.Models.DataObjects;
using System.Net.Http;
using Newtonsoft.Json;

namespace NGC.DataStore.Implementation.Stores
{
    public class CompanyStore : BaseStore<Company>, ICompanyStore
    {
        public override string Identifier => "Company";

        HttpClient openData_client;
        const string RestURLOpenData = "https://datastore.opendatasoft.com/public/api/records/1.0/search?dataset=sirene@public&q=";

        public CompanyStore()
        {
            openData_client = new HttpClient();
        }

        public async Task<IEnumerable<OpenDataResponse.Record>> SearchCompanyFromOpenData(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return null;

            try
            {
                var result = await openData_client.GetAsync(RestURLOpenData + searchText);

                if (result.IsSuccessStatusCode)
                {
                   var content = await result.Content.ReadAsStringAsync();

                   var objects = JsonConvert.DeserializeObject<OpenDataResponse.Example>(content);

                   return objects.records;
                }

            }
            catch (Exception)
            {

            }

            return null;
        }

        public async Task<IEnumerable<Company>> SearchCompany(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return null;

            try
            {
                var result = await Table.Where(arg => arg.Name.ToLower().Contains(searchText.ToLower())).ToEnumerableAsync().ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
