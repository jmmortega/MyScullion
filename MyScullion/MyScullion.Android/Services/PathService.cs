using System.IO;
using MyScullion.Services;

namespace MyScullion.Droid.Services
{
    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            var databasePath = Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), 
                    $"{typeof(App).Namespace}LiteDB");

            if(!File.Exists(databasePath))
            {
                File.Create(databasePath).Dispose();
            }

            return databasePath;
        }
        
    }
}