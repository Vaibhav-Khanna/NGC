using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NGC.DataStore.Abstraction;
using Newtonsoft.Json;

using System.Linq;

namespace NGC.DataStore.Implementation
{
	public class BaseStore<T> : IBaseStore<T> where T : BaseDataObject
    {
		protected readonly HttpClient client;
		protected readonly string Type;
		protected string Auth => string.Concat("token=", "Settings.Token");
     
        public BaseStore()
        {
			client = new HttpClient();
			Type = typeof(T).Name.ToLower() + "s";
        }

		public Task<T> GetItemAsync(string id)
		{
			try
			{
               
			}
			catch (Exception)
			{

			}

			return null;
		}

		public Task<IEnumerable<T>> GetItemsAsync()
		{
			try
			{
               

			}
			catch (Exception)
			{

			}

			return null;
		}

		public Task<T> InsertAsync(T item)
		{
			
                
            try
            {
               					
            }
			catch (Exception)
            { 
				
            }

			return null;
		}

		public Task<bool> RemoveAsync(T item)
		{
	
            try
            {
				
            }
            catch (Exception)
            {      
                
            }

            return null;
		}

		public  Task<T> UpdateAsync(T item)
		{
			
            try
            {
               
            }
            catch (Exception)
            {
				
            }

            return null;
		}
       
	}
}
