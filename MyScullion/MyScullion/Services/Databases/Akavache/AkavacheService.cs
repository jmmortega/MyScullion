using Akavache;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyScullion.Models;

namespace MyScullion.Services.Databases
{
    public class AkavacheService : IDatabaseService
    {
        private readonly IBlobCache localBlobCache;

        public AkavacheService()
        {
            Akavache.BlobCache.ApplicationName = typeof(App).Namespace;
            localBlobCache = BlobCache.LocalMachine;
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : BaseModel
        {
            return await localBlobCache.GetAllObjects<T>();
        }

        public T Get<T>(int id) where T : BaseModel
        {
            return localBlobCache.GetObject<T>(typeof(T).Name).FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetAndFetch<T>(Func<Task<T>> restAction) where T : BaseModel
        {
            return new List<T>() { await localBlobCache.GetAndFetchLatest(typeof(T).Name, restAction) };                       
        }

        public void Insert<T>(T item) where T : BaseModel
        {
            localBlobCache.InsertObject<T>(typeof(T).Name, item);
        }

        public void InsertAll<T>(List<T> items) where T : BaseModel
        {
            foreach(var item in items)
            {
                Insert(item);
            }
        }

        public async Task<bool> Delete<T>(T item) where T : BaseModel
        {
            var collection = (await GetAll<T>()).ToList();

            var element = collection.FirstOrDefault(x => x.Id == item.Id);

            if(element != null)
            {
                collection.Remove(element);
                await localBlobCache.Invalidate(typeof(T).Name);
                InsertAll(collection);
            }
            
            return false;
        }


    }
}
