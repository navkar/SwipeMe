using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwipeMe
{
    public class SwipeListener : PanGestureRecognizer
    {
        private ISwipeCallBack mISwipeCallback;
        private double translatedX = 0;
        private double translatedY = 0;

        public SwipeListener(View view, ISwipeCallBack callBack)
        {
            mISwipeCallback = callBack;
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += HandlePanUpdated;
            view.GestureRecognizers.Add(panGesture);
        }

        void HandlePanUpdated(object sender, PanUpdatedEventArgs e)
        {
            View content = (View)sender;

            switch (e.StatusType)
            {
                case GestureStatus.Running:

                    try
                    {
                        translatedX = e.TotalX;
                        translatedY = e.TotalY;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    break;

                case GestureStatus.Completed:

                    if (translatedX < 0 && Math.Abs(translatedX) > Math.Abs(translatedY))
                    {
                        mISwipeCallback.onLeftSwipe(content);
                    }
                    else if (translatedX > 0 && translatedX > Math.Abs(translatedY))
                    {
                        mISwipeCallback.onRightSwipe(content);
                    }
                    else if (translatedY < 0 && Math.Abs(translatedY) > Math.Abs(translatedX))
                    {
                        mISwipeCallback.onTopSwipe(content);
                    }
                    else if (translatedY > 0 && translatedY > Math.Abs(translatedX))
                    {
                        mISwipeCallback.onBottomSwipe(content);
                    }
                    else
                    {
                        mISwipeCallback.onNothingSwiped(content);
                    }

                    break;
            }
        }
    }
}
