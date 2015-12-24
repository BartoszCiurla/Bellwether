using System;
using System.Windows.Input;

namespace Bellwether.Commands
{
    public class RelayCommand:ICommand
    {
        private readonly Action _action;
        public RelayCommand(Action action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }
    }
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _action;
        public RelayCommand(Action<T> action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke((T)parameter);
        }
    }
}
