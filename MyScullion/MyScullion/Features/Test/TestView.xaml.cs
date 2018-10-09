using MyScullion.Services;
using MyScullion.Services.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyScullion.Features.Test
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestView : ContentPage
	{

        private readonly IDatabaseService databaseService;
        private readonly IRandomService randomService;
        private readonly IFileEmbeddedService fileEmbeddedService;

		public TestView ()
		{
			InitializeComponent ();

            databaseService = CustomDependencyService.Get<IDatabaseService>();
            randomService = CustomDependencyService.Get<IRandomService>();
            fileEmbeddedService = CustomDependencyService.Get<IFileEmbeddedService>();
		}

        private void InsertIngredientsClicked(object sender, ItemTappedEventArgs args)
        {
            var data = fileEmbeddedService.GetFile("ingredients.csv");
            
        }

        private void InsertMeasuresClicked(object sender, ItemTappedEventArgs args)
        {

        }

        private void InsertRandomTest(object sender, ItemTappedEventArgs args)
        {
            
        }

    }
}