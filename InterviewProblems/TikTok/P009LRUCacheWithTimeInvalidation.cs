using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.TikTok
{
    internal class P009LRUCacheWithTimeInvalidation : ITestable
    {
        public void RunTest()
        {
            throw new NotImplementedException();
        }

        class LruCache
        {
            private readonly int _capacity;
            private readonly Dictionary<int, LinkedListNode<(int key, int val, DateTime timestamp)>> _dict = new();
            private readonly LinkedList<(int key, int val, DateTime timestamp)> _list = new();
            private readonly TimeSpan _limit;

            public LruCache(int capacity, int limit)
            {
                _capacity = capacity;
                _limit = new TimeSpan(0, 0, limit);
            }

            public int Get(int key)
            {
                if (!_dict.ContainsKey(key)) return -1;

                var node = _dict[key];
                _list.Remove(node);
                var (_, val, timestamp) = node.Value;
                if (DateTime.Now - timestamp > _limit)
                {
                    _dict.Remove(key);
                    return -1;
                }

                node.Value = (key, val, DateTime.Now);
                _list.AddFirst(node);

                return val;
            }

            public void Put(int key, int val)
            {
                if (_dict.TryGetValue(key, out var node))
                {
                    _list.Remove(node);
                    node.Value = (key, val, DateTime.Now);
                    _list.AddFirst(node);

                    return;
                }

                if (_dict.Count == _capacity)
                {
                    var lastKey = _list.Last.Value.key;
                    _list.RemoveLast();
                    _dict.Remove(lastKey);
                }

                _list.AddFirst((key, val, DateTime.Now));
                _dict[key] = _list.First;
            }
        }
    }
}
