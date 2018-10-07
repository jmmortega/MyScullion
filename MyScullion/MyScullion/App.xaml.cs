using MyScullion.Features.Main;
using MyScullion.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MyScullion
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            CustomDependencyService.Register<MenuService>();
            
            MainPage = new MasterDetailPage() { Master = new MasterDetailMenu(), Detail = new NavigationPage(new MainView()) };
		}

        public static void ChangePresented()
        {
            var masterDetail = (MasterDetailPage)App.Current.MainPage;
            masterDetail.IsPresented = !masterDetail.IsPresented;
        }

        public static void ChangeDetail(Page page)
        {
            var masterDetail = (MasterDetailPage)App.Current.MainPage;
            masterDetail.Detail = page;
            ChangePresented();
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}        
    }
}
