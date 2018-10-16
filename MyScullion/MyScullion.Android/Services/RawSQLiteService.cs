using System;
using System.Linq;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Mono.Data.Sqlite;
using MyScullion.Models;
using MyScullion.Services.Databases;
using MyScullion.Services.Databases.RawSqlite;

namespace MyScullion.Droid.Services
{
    public class RawSQLiteService : DatabaseBase, IDatabaseService
    {
        private List<Type> tables = new List<Type>();

        public RawSQLiteService()
        {            
        }
        
        public new Task<bool> Delete<T>(T item) where T : BaseModel, new()
        {
            CheckTable<T>();
            
            try
            {                
                base.Delete(item.ToDeleteQuery(), item.GetIdParameter());
                return Task.FromResult(true);

            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return Task.FromResult(false);

        }

        public Task<T> Get<T>(int id) where T : MyScullion.Models.BaseModel, new()
        {
            CheckTable<T>();

            try
            {
                var myT = base.SelectFirst<T>($"SELECT {ExtensionMethodsSQLiteRaw.PrepareFieldsToSelectOrInsert<T>()} FROM {typeof(T).Name} WHERE Id = @Id",
                    new List<object>() { new SqliteParameter("@Id", id) }, ExtensionMethodsSQLiteRaw.Serialize<T>);

                return Task.FromResult(myT);
                                
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return Task.FromResult(default(T));
        }

        public Task<IEnumerable<T>> GetAll<T>() where T : MyScullion.Models.BaseModel, new()
        {
            CheckTable<T>();

            var myCollection = new List<T>();

            try
            {
                myCollection = base.Select<T>($"SELECT {ExtensionMethodsSQLiteRaw.PrepareFieldsToSelectOrInsert<T>()} FROM {typeof(T).Name} WHERE Id = @Id",
                    ExtensionMethodsSQLiteRaw.SerializeCollection<T>);                                
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return Task.FromResult<IEnumerable<T>>(myCollection);
        }

        public IObservable<T> GetAndFetch<T>(Func<Task<T>> restAction) where T : MyScullion.Models.BaseModel, new()
        {
            throw new NotImplementedException();
        }

        public Task Insert<T>(T item) where T : MyScullion.Models.BaseModel, new()
        {
            try
            {
                base.Insert(ExtensionMethodsSQLiteRaw.ToInsertQuery<T>(), item.GetParameters<T>());                                
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return Task.FromResult(Unit.Default);
        }

        public new Task InsertAll<T>(List<T> items) where T : MyScullion.Models.BaseModel, new()
        {
            try
            {
                base.InsertAll(ExtensionMethodsSQLiteRaw.ToInsertQuery<T>(), items.Select(x => x.GetParameters<T>()).ToList());
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return Task.FromResult(Unit.Default);            
        }

        private Task CheckTable<T>() where T : BaseModel, new()
        {
            if(!tables.Contains(typeof(T)))
            {
                tables.Add(typeof(T));
                CreateTable(new T().ToCreateQuery());
            }

            return Task.FromResult(Unit.Default);
        }
    }

    
}