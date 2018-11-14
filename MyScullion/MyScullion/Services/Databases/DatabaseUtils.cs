using MyScullion.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace MyScullion.Services.Databases
{
    public static class DatabaseUtils
    {
        public static IObservable<IEnumerable<T>> PrepareGetAndFetch<T>(Func<Task<IEnumerable<T>>> getDatabase, Func<Task<IEnumerable<T>>> fetchFunction) where T : BaseModel, new ()
        {
            var getCache = Task.Run(getDatabase)
                .ToObservable();

            var fetch = Task.Run(fetchFunction)
                .ToObservable();

            return getCache
                .Concat(fetch)
                .Multicast(new ReplaySubject<IEnumerable<T>>())
                .RefCount();
        }
    }
}
