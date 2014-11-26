using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grappr
{
    public static class EnumerableExtensions
    {
        private static Random _random = new Random(DateTime.Now.Millisecond);
        public static T Rand<T>(this IEnumerable<T> items)
        {
            var count = items.Count();
            var d = _random.Next(count);
            return items.ElementAt(d);
        }
    }
}
