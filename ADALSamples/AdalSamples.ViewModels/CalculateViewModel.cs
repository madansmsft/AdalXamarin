using AdalSamples.WebApiClient;
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
        public CalculateViewModel(string webApiUri, string accessToken) : base(webApiUri, accessToken)
        {
          //  this.AddNumbers = new RelayCommand()

        }

        private int _number1;
        private int _number2;
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




        public ICommand AddNumbers { get; private set; }
        public ICommand Power { get; private set; }

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



        public async Task AddNumbersExecute()
        {
            this.IsBusy = true;
            var client = new SimpleMathClient(this.WebApiUri, this.AccessToken);
            this.Result = await client.AddNumbers(this.Number1,  this.Number2);
            this.IsBusy = false;
        }


        public async Task PowerExecute()
        {
            this.IsBusy = true;
            var client = new SimpleMathClient(this.WebApiUri, this.AccessToken);
            this.Result = await client.PowerEx(this.Number1, this.Number2);
            this.IsBusy = false;
        }


    }
}
