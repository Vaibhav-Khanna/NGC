using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace NGC.ViewModels
{
    public class NewContactPageModel : BaseViewModel
    {
        public NewContactPageModel()
        {

        }

        public bool IsProfessional { get; set; }

        public bool IsContactTab { get; set; } = true;


        public Command BackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(null, true);
        });

        public Command SaveCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(null, true);
        });


        public override void Init(object initData)
        {
            base.Init(initData);

            IsProfessional = (bool)initData;
        }

        public void TabSelectedChanged(int index)
        {
            IsContactTab = index == 0;
        }

    }
}
