using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyScullion.Features.Test
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestView : ContentPage
	{
		public TestView ()
		{
			InitializeComponent ();
		}
	}
}