using System;
using System.Collections.Generic;

namespace MyScullion.Services.Databases
{
    public class AkavacheService : IDatabaseService
    {
        public List<T> GetAll<T>()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAndFetch<T>(Func<T> restAction)
        {
            throw new NotImplementedException();
        }

        public void Insert<T>(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T item)
        {
            throw new NotImplementedException();
        }        
    }
}
