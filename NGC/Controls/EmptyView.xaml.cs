using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NGC.Controls
{
    public partial class EmptyView : ContentView
    {
        string _text;
        public string Text { get { return _text; } set { _text = value; lbT.Text = value; } }


        public EmptyView()
        {
            InitializeComponent();
        }

    }
}
