using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NGC.Helpers.MapViews
{
    public partial class BindingPinView
    {
		private string _display;
        private Color _color;

        public BindingPinView(string display, Color color)
		{
			InitializeComponent();
			
            _display = display;
            _color = color;

			BindingContext = this;

            lb.Text = display;
            back.BackgroundColor = color;
		}

        public Color Color
        {
            get { return _color; }
        }

        public string Display
		{
			get { return _display; }
		}
    }
}
