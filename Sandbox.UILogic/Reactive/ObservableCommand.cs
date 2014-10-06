using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Sandbox.UILogic.Reactive
{
    public class ObservableCommand : ICommand, IObservable<object>
    {
        protected readonly Func<object, bool> _canExecute;
        private readonly List<IObserver<object>> _observers = new List<IObserver<object>>();

        public ObservableCommand(Func<object, bool> canExecute)
        {
            _canExecute = canExecute;
        }

        public ObservableCommand() :
            this(null)
        {
        }

        public event EventHandler CanExecuteChanged = null;

        public virtual bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public virtual void Execute(object parameter)
        {
            foreach (var observer in _observers.ToArray())
            {
                observer.OnNext(parameter);
            }
        }

        public IDisposable Subscribe(IObserver<object> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber<object>(_observers, observer);
        }

        public void NotifyCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, null);
            }
        }
    }
}