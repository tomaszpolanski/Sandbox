using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Sandbox.UILogic
{
    public static class ReactiveExtensions
    {
        public static IObservable<long> BucketSum<TSource>(this IObservable<TSource> self, Func<TSource, long> selector)
        {
            var bucket = new Dictionary<TSource, long>();
            return self.Select(source =>
            {
                bucket[source] = selector(source);
                return bucket.Values.Sum();
            });
        }
    }
}
