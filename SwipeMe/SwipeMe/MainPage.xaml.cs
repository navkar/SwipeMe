using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwipeMe
{
	public partial class MainPage : ContentPage, ISwipeCallBack
    {
		public MainPage()
		{
			InitializeComponent();
            btnOK.Clicked += btnOK_Clicked;

            //Activate Swipe
            var swipeListener = new SwipeListener(SwipeArea, this);
        }

        public void onBottomSwipe(View view)
        {
            if (view == SwipeArea)
            {
                SwipeComment.Text = string.Format("You swiped Down");
            }
        }

        public void onLeftSwipe(View view)
        {
            if (view == SwipeArea)
            {
                SwipeComment.Text = string.Format("You swiped Left");
            }
        }

        public void onNothingSwiped(View view)
        {
        }

        public void onRightSwipe(View view)
        {
            if (view == SwipeArea)
            {
                SwipeComment.Text = string.Format("You swiped Right");
            }
        }

        public void onTopSwipe(View view)
        {
            if (view == SwipeArea)
            {
                SwipeComment.Text = string.Format("You swiped UP");
            }
        }

        protected void btnOK_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ThankYouPage());
        }

	}
}
