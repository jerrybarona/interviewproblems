using System;
using System.Collections.Generic;

namespace InterviewProblems.Microsoft
{
    public class P026GetSetSetAll
    {
        public void GetSetSetAllTest()
        {
            var memory = new CustomMemory();
            memory.Set(0,1);
            Console.WriteLine(memory.Get(0));
            memory.Set(1,2);
            Console.WriteLine(memory.Get(1));
            memory.SetAll(5);
            Console.WriteLine(memory.Get(0));
            Console.WriteLine(memory.Get(1));
            Console.WriteLine(memory.Get(2));
            memory.Set(2,7);
            Console.WriteLine(memory.Get(0));
            Console.WriteLine(memory.Get(1));
            Console.WriteLine(memory.Get(2));
        }

        class CustomMemory
        {
            private readonly Dictionary<int,Node> _cache = new Dictionary<int, Node>();
            private int _timer = 1;
            private int _lastSetAllTimestamp;
            private int _lastSetAllValue;

            public int Get(int key)
            {
                if (!_cache.ContainsKey(key)) return int.MinValue;
                if (_cache[key].Timestamp < _lastSetAllTimestamp) return _lastSetAllValue;
                return _cache[key].Value;
            }

            public void Set(int key, int value)
            {
                if (!_cache.ContainsKey(key)) _cache.Add(key, new Node());
                _cache[key].Value = value;
                _cache[key].Timestamp = _timer++;
            }

            public void SetAll(int value)
            {
                _lastSetAllValue = value;
                _lastSetAllTimestamp = _timer++;
            }
        }

        class Node
        {
            public int Value { get; set; }
            public int Timestamp { get; set; }
        }
    
    }
}
