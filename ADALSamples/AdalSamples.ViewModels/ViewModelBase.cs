using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdalSamples.ViewModels
{
    public class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        private string _webApiUri;
        private string _accessToken;


        public string WebApiUri
        {
            get
            {
                return _webApiUri;
            }

            set
            {
                _webApiUri = value;
                RaisePropertyChanged("WebApiUri");
            }
        }

        public string AccessToken
        {
            get
            {
                return _accessToken;
            }

            set
            {
                _accessToken = value;
                RaisePropertyChanged("AccessToken");
            }
        }

        public ViewModelBase() : base() { }
        public ViewModelBase(string webApiUri, string accessToken): base()
        {
            this.AccessToken = accessToken;
            this.WebApiUri = webApiUri;
        }

    }
}
