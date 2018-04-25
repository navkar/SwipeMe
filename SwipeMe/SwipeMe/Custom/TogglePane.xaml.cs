using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwipeMe
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TogglePane : ContentView
	{
		public TogglePane ()
		{
			InitializeComponent ();
		}
	}
}