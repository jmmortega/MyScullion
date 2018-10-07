using System;
using System.Collections.Generic;
using System.Text;

namespace MyScullion.Services.Databases
{
    public interface IDatabaseService
    {
        List<T> GetAll<T>();

        List<T> GetAndFetch<T>(Func<T> restAction);

        T Get<T>(int id);

        void Insert<T>(T item);

        void Delete<T>(T item);
    }
}
