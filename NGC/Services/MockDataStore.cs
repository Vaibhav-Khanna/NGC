using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGC.Models;

namespace NGC.Services
{
    public class MockDataStore : IDataStore<BaseDataObject>
    {
        List<BaseDataObject> items;

        public MockDataStore()
        {
            items = new List<BaseDataObject>();
            var mockItems = new List<BaseDataObject>
            {
                new BaseDataObject { Id = Guid.NewGuid().ToString(),  },
                new BaseDataObject { Id = Guid.NewGuid().ToString(), },
                new BaseDataObject { Id = Guid.NewGuid().ToString(), },
                 new BaseDataObject { Id = Guid.NewGuid().ToString(), },
                 new BaseDataObject { Id = Guid.NewGuid().ToString(), },
                 new BaseDataObject { Id = Guid.NewGuid().ToString(), }
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(BaseDataObject item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(BaseDataObject item)
        {
            var oldItem = items.Where((BaseDataObject arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((BaseDataObject arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<BaseDataObject> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<BaseDataObject>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}