using System.Collections.Generic;
using MyScullion.Features.Main;
using MyScullion.Features.Test;
using MyScullion.Services;
using MyScullion.Services.Databases;
using MyScullion.Services.Databases.MonkeyCache;

[assembly: Xamarin.Forms.Dependency(typeof(MenuService))]
namespace MyScullion.Services
{    
    public class MenuService : IMenuService
    {
        public List<MasterDetailPageMenuItem> GetMenuItems()
        {
            return new List<MasterDetailPageMenuItem>()
            {
                new MasterDetailPageMenuItem(typeof(AkavacheService))
                {
                    Id = 10,
                    Title = "Akavache"
                },
                new MasterDetailPageMenuItem(typeof(MonkeyCacheService))
                {
                    Id = 20,
                    Title = "MonkeyCache"
                },
                new SeparatorMasterDetail(),

                new MasterDetailPageMenuItem(typeof(TestView))
                {
                    Id = 30,
                    Title = "Test database view"
                }
            };
        }
    }
}
