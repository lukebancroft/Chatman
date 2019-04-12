using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chatman
{
    public class RelayCommand : ICommand
    {
        private readonly Action actionToExecute;
        public RelayCommand(Action action)
        {
            actionToExecute = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            actionToExecute();
        }
    }

    public class RelayCommandParameter : ICommand
    {
        private Action<object> _action;
        private bool _canExecute;
        public RelayCommandParameter(Action<object> action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }



}
