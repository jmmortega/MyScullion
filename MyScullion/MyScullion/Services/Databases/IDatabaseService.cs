using MyScullion.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyScullion.Services.Databases
{
    public interface IDatabaseService
    {
        Task<IEnumerable<T>> GetAll<T>() where T : BaseModel;

        IObservable<T> GetAndFetch<T>(Func<Task<T>> restAction) where T : BaseModel;

        T Get<T>(int id) where T : BaseModel;

        void Insert<T>(T item) where T : BaseModel;

        void InsertAll<T>(List<T> items) where T : BaseModel;

        Task<bool> Delete<T>(T item) where T : BaseModel;
    }
}
