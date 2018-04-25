using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwipeMe
{
    public class CircleView : ContentView
    {
        #region Private Variables

        private Button _canvasView;
        #endregion Private Variables

        #region Constructor
        public CircleView()
        {
            PropertyChanged += CircleView_PropertyChanged;
        }

        #endregion Constructor

        #region Bindable Properties
        public static readonly BindableProperty FillColorProperty =
           BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(CircleView), Color.Transparent);

        public Color FillColor
        {
            get => (Color)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }

        public static readonly BindableProperty StrokeColorProperty =
           BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(CircleView), Color.Default);

        public Color StrokeColor
        {
            get => (Color)GetValue(StrokeColorProperty);
            set => SetValue(StrokeColorProperty, value);
        }


        public static readonly BindableProperty RadiusProperty =
           BindableProperty.Create(nameof(Radius), typeof(float), typeof(CircleView), -1f);

        public float Radius
        {
            get => (float)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        public static readonly BindableProperty StrokeWidthProperty =
           BindableProperty.Create(nameof(StrokeWidth), typeof(float), typeof(CircleView), 0f);

        public float StrokeWidth
        {
            get => (float)GetValue(StrokeWidthProperty);
            set => SetValue(StrokeWidthProperty, value);
        }

        public static readonly BindableProperty ViewSizeProperty =
        BindableProperty.Create(nameof(ViewSize), typeof(double), typeof(CircleView), 0.0);
        public double ViewSize
        {
            get => (double)GetValue(ViewSizeProperty);
            set => SetValue(ViewSizeProperty, value);
        }
        #endregion Bindable Properties

        #region Private Events

        private void _canvasView_PaintSurface()
        {
            if (_canvasView != null)
                _canvasView = null;
            _canvasView = new Button
            {
                WidthRequest = ViewSize,
                HeightRequest = ViewSize
            };
            if (ViewSize != 0.0)
            {
                _canvasView.BackgroundColor = FillColor;
                _canvasView.BorderColor = StrokeColor;
                _canvasView.BorderRadius = (int)(ViewSize / 2);
                _canvasView.BorderWidth = StrokeWidth;
                Content = _canvasView;
            }
        }

        /// <summary>
        /// This method is used to handle the property changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CircleView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Invalidating the canvas surface for redrawing the control.
            if (e.PropertyName == ViewSizeProperty.PropertyName ||
               e.PropertyName == FillColorProperty.PropertyName)
            {
                _canvasView_PaintSurface();
            }
        }
        #endregion
    }
}
