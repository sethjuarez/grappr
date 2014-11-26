using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    class PriorityQueue<P, V>
    {
        private SortedDictionary<P, Queue<V>> list = new SortedDictionary<P, Queue<V>>();
        private int _count = 0;
        public void Enqueue(P priority, V value)
        {
            Queue<V> q;
            if (!list.TryGetValue(priority, out q))
            {
                q = new Queue<V>();
                list.Add(priority, q);
            }
            q.Enqueue(value);
            _count++;
        }

        public V Dequeue()
        {
            // will throw if there isn’t any first element!
            var pair = list.First();
            var v = pair.Value.Dequeue();
            if (pair.Value.Count == 0) // nothing left of the top priority.
                list.Remove(pair.Key);
            _count--;
            return v;
        }

        public bool IsEmpty
        {
            get { return !list.Any(); }
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }
    }
}
