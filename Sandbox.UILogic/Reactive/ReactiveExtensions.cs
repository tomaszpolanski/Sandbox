using System;
using System.Collections;
using System.Collections.Specialized;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Sandbox.UILogic.Reactive
{
    public static class ReactiveExtensions
    {
        public static IObservable<TSource> ObserveOnUI<TSource>(this IObservable<TSource> source)
        {
            if (SynchronizationContext.Current == null)
            {
                return source.ObserveOn(Scheduler.CurrentThread);
            }
            return source.ObserveOn(SynchronizationContext.Current);
        }

        public static IObservable<TSource> SubscribeOnUI<TSource>(this IObservable<TSource> source)
        {
            if (SynchronizationContext.Current == null)
            {
                return source.SubscribeOn(Scheduler.CurrentThread);
            }
            return source.SubscribeOn(SynchronizationContext.Current);
        }

        public static IObservable<TSource> SelectArgs<TSource>(this IObservable<EventPattern<TSource>> source)
        {
            return source.Select(ev => ev.EventArgs);
        }

        public static IObservable<TSource> WhereIsNotNull<TSource>(this IObservable<TSource> source)
            where TSource : class
        {
            return source.Where(arg => arg != null);
        }

        public static IObservable<TSource> Log<TSource>(this IObservable<TSource> source, Action<string, string> action,
            [CallerMemberName] string memberName = null)
        {
            return source.Do(arg => action("" + arg, memberName));
        }

        public static IObservable<int> CountObservable<TSource>(this TSource source)
            where TSource : ICollection, INotifyCollectionChanged
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
        public static IObservable<TResults> SelectManyOneAtATime<TSource, TResults>(this IObservable<TSource> source,
            Func<TSource, IObservable<TResults>> action)
        {
            return source.Select(arg => action(arg)).Switch();
        }
    }
}