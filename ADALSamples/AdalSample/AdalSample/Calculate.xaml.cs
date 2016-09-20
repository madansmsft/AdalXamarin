using System;
using Xamarin.Forms;

using AdalSamples.ViewModels;
using GalaSoft.MvvmLight.Messaging;

namespace AdalSample
{
    public partial class Calculate : ContentPage
    {
        public CalculateViewModel CalcVM { get; set; }


        public Calculate()
        {
            InitializeComponent();
            this.CalcVM = new CalculateViewModel(Configuration.BaseUri, Configuration.AccessToken);
            activityIndicator.IsRunning = false;
            BindingContext = this.CalcVM;

            Messenger.Default.Register<string>(this, (string result) =>
            {
                DisplayAlert("Message From ViewModel", result, "OK");
            });


        }

        //async void OnButtonClicked(object sender, EventArgs args)
        //{
            

        //    //vm.Number1 = Convert.ToInt32( Num1.Text);
        //    //vm.Number2 = Convert.ToInt32( Num2.Text);

           


        //    //await vm.AddNumbersExecute();

        //    //await DisplayAlert("result", vm.Result, "OK");


        //}

        //async void OnButtonClicked2(object sender, EventArgs args)
        //{


        //    var vm = new CalculateViewModel(Configuration.BaseUri, Configuration.AccessToken);
        //    //vm.Number1 = Convert.ToInt32(Num1.Text);
        //    //vm.Number2 = Convert.ToInt32(Num2.Text);




        //    //await vm.PowerExecute();

        //    //await DisplayAlert("result", vm.Result, "OK");


        //}


    }
}
