using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdalSamples.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private bool _isBusy = false;
        private string _webApiUri;
        private string _accessToken;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    RaisePropertyChanged("IsBusy");
                }
            }
        }



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

        public ViewModelBase(string webApiUri, string accessToken)
        {
            this.AccessToken = accessToken;
            this.WebApiUri = webApiUri;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName) { });

        }
    }
}
