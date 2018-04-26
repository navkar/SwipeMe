using SwipeMe.Models;
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
	public partial class ThankYouPage : ContentPage
	{
		public ThankYouPage ()
		{
			InitializeComponent ();

            var list = new List<LabelViewModel>();

            for (int i = 10; i > 0; i--)
            {
                list.Add(new LabelViewModel() { Caption = "No "+ i, Description = string.Format("This is a long description text for item {0}", i) });
            }
            
            scrollItems.ItemsSource = list;
		}
	}
}