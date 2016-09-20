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

    }
}
