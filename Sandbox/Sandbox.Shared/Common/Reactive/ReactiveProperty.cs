// -----------------------------------------------------------------------
// <copyright file="ReactiveProperty.cs" company="NOKIA">
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

namespace MapsW8.Base.Reactive
{
    using System;
    using System.Reactive.Concurrency;

    public class ReactiveProperty<T> : ReactivePropertyBase<T>, IReactiveProperty
    {
        /// <summary>PropertyChanged raise on UIDispatcherScheduler</summary>
        public ReactiveProperty()
            : this(default(T), ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe)
        {
        }

        /// <summary>PropertyChanged raise on UIDispatcherScheduler</summary>
        public ReactiveProperty(T initialValue = default(T),
                                        ReactivePropertyMode mode = ReactivePropertyMode.DistinctUntilChanged|ReactivePropertyMode.RaiseLatestValueOnSubscribe)
            : this(UIDispatcherScheduler.Default, initialValue, mode)
        { }

        /// <summary>PropertyChanged raise on selected scheduler</summary>
        public ReactiveProperty(IScheduler raiseEventScheduler,
                                        T initialValue = default(T),
            ReactivePropertyMode mode = ReactivePropertyMode.DistinctUntilChanged|ReactivePropertyMode.RaiseLatestValueOnSubscribe)
            : this(System.Reactive.Linq.Observable.Never<T>(), raiseEventScheduler, initialValue, mode)
        {
        }

        // ToReactiveProperty Only
        internal ReactiveProperty(IObservable<T> source,
                                          T initialValue = default(T),
                                          ReactivePropertyMode mode = ReactivePropertyMode.DistinctUntilChanged|ReactivePropertyMode.RaiseLatestValueOnSubscribe)
            : this(source, UIDispatcherScheduler.Default, initialValue, mode)
        {
        }

        internal ReactiveProperty(IObservable<T> source,
                                          IScheduler raiseEventScheduler,
                                          T initialValue = default(T),
                                          ReactivePropertyMode mode = ReactivePropertyMode.DistinctUntilChanged|ReactivePropertyMode.RaiseLatestValueOnSubscribe) :
            base(source, raiseEventScheduler, initialValue, mode)
        {
        }

        /// <summary>
        /// Get latestValue or push(set) value.
        /// </summary>
        public T Value
        {
            get { return InternalValue; }
            set { InternalValue = value; }
        }

        object IReactiveProperty.Value
        {
            get { return Value; }
        }
    }
}