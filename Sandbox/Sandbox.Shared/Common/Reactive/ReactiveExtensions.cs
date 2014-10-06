// -----------------------------------------------------------------------
// <copyright file="ReactiveExtensions.cs" company="NOKIA">
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
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Reactive;
    using System.Reactive.Concurrency;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public static class ReactiveExtensions
    {
        public static IObservable<TSource> ObserveOnUI<TSource>(this IObservable<TSource> source)
        {
            if (SynchronizationContext.Current == null)
            {
                return source.ObserveOn(Scheduler.CurrentThread);
            }
            else
            {
                return source.ObserveOn(SynchronizationContext.Current);
            }
        }

        public static IObservable<TSource> SubscribeOnUI<TSource>(this IObservable<TSource> source)
        {
            if (SynchronizationContext.Current == null)
            {
                return source.SubscribeOn(Scheduler.CurrentThread);
            }
            else
            {
                return source.SubscribeOn(SynchronizationContext.Current);
            }
        }

        public static IObservable<TSource> SelectArgs<TSource>(this IObservable<EventPattern<TSource>> source)
        {
            return source.Select(ev => ev.EventArgs);
        }

        public static IObservable<TSource> WhereIsNotNull<TSource>(this IObservable<TSource> source) where TSource : class
        {
            return source.Where(arg => arg != null);
        }

        public static IObservable<TSource> Log<TSource>(this IObservable<TSource> source, Action<string, string> action, [CallerMemberName] string memberName = null)
        {
            return source.Do(arg => action("" + arg, memberName));
        }

        public static IObservable<int> CountObservable<TSource>(this TSource source) where TSource : ICollection, INotifyCollectionChanged
        {
            return Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                                                                     h => source.CollectionChanged += h,
                                                                     h => source.CollectionChanged -= h)
                             .Select(_ => source.Count)
                             .StartWith(source.Count);
        }

        /// <summary>
        /// Works similarly to SelectMany but when the source triggers new value,
        /// we unsubscribe from previous stream and use only values from new stream
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResults"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>

        public static IObservable<TResults> SelectManyOneAtATime<TSource, TResults>(this IObservable<TSource> source, Func<TSource, IObservable<TResults>> action)
        {
            return source.Select(arg => action(arg)).Switch();
        }
    }
}