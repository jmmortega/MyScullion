using MyScullion.Models;
using MyScullion.Services;
using MyScullion.Services.Databases;
using MyScullion.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var data = fileEmbeddedService.GetFile("measures.csv");
        }

        private async void InsertRandomTest(object sender, ItemTappedEventArgs args)
        {
            var rows = 0;
            int.TryParse(EntryRows.Text, out rows);

            var randomData = randomService.CreateRandomData(rows);
            
            Log.Start($"RandomData{databaseService.GetType().Name}");
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            await databaseService.InsertAll<RandomModel>(randomData);

            stopWatch.Stop();
            
            LabelTimeWorking.Text = TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).ToString();

            Log.Stop($"RandomData{databaseService.GetType().Name}");
        }

    }
}