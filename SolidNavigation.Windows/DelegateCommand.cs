using System;
using System.Windows.Input;

namespace SolidNavigation.Windows
{
    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action executeAction)
            : base(_ => executeAction())
        {
        }

        public DelegateCommand(Action executeAction, Func<bool> canExecute)
            : base(_ => executeAction(), _ => canExecute())
        {
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _executeAction;

        public DelegateCommand(Action<T> executeAction)
        {
            _executeAction = executeAction;
        }

        public DelegateCommand(Action<T> executeAction, Predicate<T> canExecute)
            : this(executeAction)
        {
            _canExecute = canExecute;
        }

        public bool CanExecute(T parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute(parameter);
            }
            return true;
        }

        public void Execute(T parameter)
        {
            if (_executeAction != null)
                _executeAction(parameter);

            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #region ICommand Members

        bool ICommand.CanExecute(object parameter)
        {
            return this.CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            this.Execute((T)parameter);
        }

        #endregion
    }
}
