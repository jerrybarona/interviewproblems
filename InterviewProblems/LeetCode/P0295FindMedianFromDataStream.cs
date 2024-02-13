using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.LeetCode
{
    public class P0295FindMedianFromDataStream : ITestable
    {
        public void RunTest()
        {
            var mf = new MedianFinder();
            mf.AddNum(1);
            mf.AddNum(2);
            Console.WriteLine(mf.FindMedian());
            mf.AddNum(3);
            Console.WriteLine(mf.FindMedian());
        }

        public class MedianFinder
        {
            private readonly PriorityQueue<int, int> _max;
            private readonly PriorityQueue<int, int> _min;

            public MedianFinder()
            {
                _max = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b - a));
                _min = new PriorityQueue<int, int>();
            }

            public void AddNum(int num)
            {
                if (_min.Count == 0 && _max.Count == 0) _min.Enqueue(num, num);

                else if (_min.Count > _max.Count)
                {
                    if (num < _min.Peek()) _max.Enqueue(num, num);
                    else
                    {
                        var min = _min.Dequeue();
                        _max.Enqueue(min, min);
                        _min.Enqueue(num, num);
                    }
                }
                else
                {
                    if (num >= _min.Peek()) _min.Enqueue(num, num);
                    else
                    {
                        _max.Enqueue(num, num);

                        var max = _max.Dequeue();
                        _min.Enqueue(max, max);
                    }
                }
            }

            public double FindMedian()
            {
                //Console.WriteLine($"min: {_min.Peek()}, max: {_max.Peek()}");
                if (_min.Count > _max.Count) return _min.Peek();

                return ((double)_min.Peek() + (double)_max.Peek()) / 2;
            }
        }
    }
}
