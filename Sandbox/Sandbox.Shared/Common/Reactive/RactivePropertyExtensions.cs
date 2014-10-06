// -----------------------------------------------------------------------
// <copyright file="RactivePropertyExtensions.cs" company="NOKIA">
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

    public static class RactivePropertyExtensions
    {
        public static ReactiveProperty<T> ToReactiveProperty<T>(this IObservable<T> source,
            T initialValue = default(T),
            ReactivePropertyMode mode = ReactivePropertyMode.DistinctUntilChanged|ReactivePropertyMode.RaiseLatestValueOnSubscribe)
        {
            return new ReactiveProperty<T>(source, initialValue, mode);
        }

        public static ReactiveProperty<T> ToReactiveProperty<T>(this IObservable<T> source,
            IScheduler raiseEventScheduler,
            T initialValue = default(T),
            ReactivePropertyMode mode = ReactivePropertyMode.DistinctUntilChanged|ReactivePropertyMode.RaiseLatestValueOnSubscribe)
        {
            return new ReactiveProperty<T>(source, raiseEventScheduler, initialValue, mode);
        }

        public static ReadonlyReactiveProperty<T> ToReadonlyReactiveProperty<T>(this IObservable<T> source,
            T initialValue = default(T),
            ReactivePropertyMode mode = ReactivePropertyMode.DistinctUntilChanged|ReactivePropertyMode.RaiseLatestValueOnSubscribe)
        {
            return new ReadonlyReactiveProperty<T>(source, initialValue, mode);
        }

        public static ReadonlyReactiveProperty<T> ToReadonlyReactiveProperty<T>(this IObservable<T> source,
            IScheduler raiseEventScheduler,
            T initialValue = default(T),
            ReactivePropertyMode mode = ReactivePropertyMode.DistinctUntilChanged|ReactivePropertyMode.RaiseLatestValueOnSubscribe)
        {
            return new ReadonlyReactiveProperty<T>(source, raiseEventScheduler, initialValue, mode);
        }
    }
}