using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using MyScullion.Models;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Reactive;

namespace MyScullion.Services.Databases.Realm
{
    public class RealmService : IDatabaseService
    {
        private Realms.Realm database;

        public RealmService()
        {
            database = Realms.Realm.GetInstance(CustomDependencyService.Get<IPathService>().GetDatabasePath("Realm"));            
        }

        public Task<bool> Delete<T>(T item) where T : BaseModel
        {
            var element = database.Find<WrapRealm<T>>(item.Id);

            if(element != null)
            {
                database.Remove(element);

                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public T Get<T>(int id) where T : BaseModel
        {
            return database.Find<WrapRealm<T>>(id).Model;
        }

        public Task<IEnumerable<T>> GetAll<T>() where T : BaseModel
        {            
            return Task.FromResult(database.All<WrapRealm<T>>().ToList().Select(x => x.Model));
        }

        public IObservable<T> GetAndFetch<T>(Func<Task<T>> restAction) where T : BaseModel
        {
            var fetch = Observable.Defer(() => GetAll<T>().ToObservable())
                .SelectMany(_ =>
                {
                    var fetchObs = restAction().ToObservable().Catch<T, Exception>(ex =>
                    {
                        return Observable.Return(Unit.Default).SelectMany(x => Observable.Throw<T>(ex));
                    });
                    return fetchObs;
                });

            return fetch;
        }

        public void Insert<T>(T item) where T : BaseModel
        {
            database.Add(new WrapRealm<T>(item));
        }

        public void InsertAll<T>(List<T> items) where T : BaseModel
        {
            foreach(var element in items)
            {
                Insert(element);
            }
        }
    }
}
