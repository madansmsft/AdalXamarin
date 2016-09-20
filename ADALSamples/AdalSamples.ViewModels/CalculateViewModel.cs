using AdalSamples.WebApiClient;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdalSamples.ViewModels
{
    public class CalculateViewModel : ViewModelBase
    {
        public CalculateViewModel()
        {
        }


        private bool _isBusy = false;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }


        public CalculateViewModel(string webApiUri, string accessToken) : base(webApiUri, accessToken)
        {
            this.AddNumbersCommand = new RelayCommand(this.AddNumbersExecute, () => !this.IsBusy);
            this.PowerCommand = new RelayCommand(this.PowerExecute, () => !this.IsBusy);
            

        }

        private int _number1 = 1 ;
        private int _number2 = 1;
        private string _result;

        public int Number1
        {
            get
            {
                return _number1;
            }

            set
            {
                _number1 = value;
                RaisePropertyChanged("Number1");
            }
        }

        public int Number2
        {
            get
            {
                return _number2;
            }

            set
            {
                _number2 = value;
                RaisePropertyChanged("Number2");
            }
        }




        public ICommand AddNumbersCommand { get; private set; }
        public ICommand PowerCommand { get; private set; }

        public string Result
        {
            get
            {
                return _result;
            }

            set
            {
                _result = value;
                RaisePropertyChanged("Result");
            }
        }



        private async void AddNumbersExecute()
        {
            this.IsBusy = true;
            var client = new SimpleMathClient(this.WebApiUri, this.AccessToken);
            this.Result = await client.AddNumbers(this.Number1,  this.Number2);
            this.IsBusy = false;
            Messenger.Default.Send<string>(Result);
        }


        private async void PowerExecute()
        {
            this.IsBusy = true;
            var client = new SimpleMathClient(this.WebApiUri, this.AccessToken);
            this.Result = await client.PowerEx(this.Number1, this.Number2);
            this.IsBusy = false;
            Messenger.Default.Send<string>(Result);
        }


    }
}
