using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdalSamples.ViewModels
{
    public class RelayCommand : ICommand
    {
        private bool _canExecute;
        private Action _execute;

        public RelayCommand(Action action, bool canExecute)
        {
            this._canExecute = canExecute;
            this._execute = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Execute(object parameter)
        {
            this._execute();
        }
    }
}
