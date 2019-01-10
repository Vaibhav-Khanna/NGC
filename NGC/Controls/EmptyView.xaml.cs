using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NGC.Controls
{
    public partial class EmptyView : ContentView
    {
        string _text = "No new items" + Environment.NewLine + "Any new items will appear here";
        public string Text { get { return _text; } set { _text = value; if(lbT!=null)lbT.Text = value; } }


        public EmptyView()
        {
            InitializeComponent();

            lbT.Text = Text;
        }

    }
}
