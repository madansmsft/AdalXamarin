using AdalSamples.WebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdalSamples.ViewModels
{
    public class HelloViewModel : ViewModelBase
    {

        private string _responseString; 


        public HelloViewModel(string webApiUri, string accessToken): base(webApiUri, accessToken){}


        public string ResponseString
        {
            get
            {
                return _responseString;
            }

            set
            {
                _responseString = value;
                RaisePropertyChanged("ResponseString");
            }
        }

        public async Task LoadData()
        {
            this.IsBusy = true;
            var client = new HelloClient(this.WebApiUri, this.AccessToken);
            this.ResponseString = await client.SayHello();
            this.IsBusy = false;
        }

    }
}
