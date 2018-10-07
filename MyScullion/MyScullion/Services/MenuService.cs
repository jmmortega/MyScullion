using System.Collections.Generic;
using MyScullion.Features.Main;
using MyScullion.Services;
using MyScullion.Services.Databases;

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
                    Id = 1,
                    Title = "Akavache"
                }
            };
        }
    }
}
