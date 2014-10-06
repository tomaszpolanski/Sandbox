// -----------------------------------------------------------------------
// <copyright file="ObservableCommand.cs" company="NOKIA">
// copyright © 2014 Nokia.  All rights reserved.
// This material, including documentation and any related computer
// programs, is protected by copyright controlled by Nokia.  All
// rights are reserved.  Copying, including reproducing, storing,
// adapting or translating, any or all of this material requires the
// prior written consent of Nokia.  This material also contains
// confidential information which may not be disclosed to others
// without the prior written consent of Nokia.
// </copyright>
// -----------------------------------------------------------------------

namespace MapsW8.Base
{
    using HERE.Common;
    using MapsW8.Base.Observable;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    public class ObservableCommand : ICommand, IObservable<object>
    {
        private readonly List<IObserver<object>> _observers = new List<IObserver<object>>();

        public event EventHandler CanExecuteChanged = null;

        protected readonly Func<object, bool> _canExecute;

        public ObservableCommand(Func<object, bool> canExecute)
        {
            _canExecute = canExecute;
        }

        public ObservableCommand() :
            this(null)
        {
        }

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

        public void NotifyCanExecuteChanged()
        {
            this.Notify(CanExecuteChanged);
        }

        public IDisposable Subscribe(IObserver<object> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber<object>(_observers, observer);
        }
    }
}