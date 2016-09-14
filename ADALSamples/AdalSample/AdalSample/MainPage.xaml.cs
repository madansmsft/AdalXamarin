using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AdalSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

       async void OnButtonClicked(object sender, EventArgs args)
        {
            var auth = DependencyService.Get<IAuthenticator>();
            var data = await auth.Authenticate(Configuration.AADAuthority, Configuration.AADEndPointId, Configuration.AADClientId, "https://localhost");

            Configuration.AccessToken = data.AccessToken;

            await Navigation.PushAsync(new Calculate());




        }


    }
}
