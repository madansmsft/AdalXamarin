
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using AdalSamples.ViewModels;

namespace AdalSample
{
    public partial class Calculate : ContentPage
    {
        public Calculate()
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            

            var vm = new CalculateViewModel(Configuration.BaseUri, Configuration.AccessToken);
            vm.Number1 = Convert.ToInt32( Num1.Text);
            vm.Number2 = Convert.ToInt32( Num2.Text);

           


            await vm.AddNumbersExecute();

            await DisplayAlert("result", vm.Result, "OK");


        }

        async void OnButtonClicked2(object sender, EventArgs args)
        {


            var vm = new CalculateViewModel(Configuration.BaseUri, Configuration.AccessToken);
            vm.Number1 = Convert.ToInt32(Num1.Text);
            vm.Number2 = Convert.ToInt32(Num2.Text);




            await vm.PowerExecute();

            await DisplayAlert("result", vm.Result, "OK");


        }


    }
}
