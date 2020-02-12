using System;

namespace InterviewProblems.LeetCode
{
    public class P0362DesignHitCounter
    {
        public void HitCounterTest()
        {
            var counter = new HitCounter();
            counter.Hit(1);
            counter.Hit(1);
            counter.Hit(1);
            counter.Hit(300);
            Console.WriteLine(counter.GetHits(300));
            counter.Hit(300);
            Console.WriteLine(counter.GetHits(300));
            counter.Hit(301);
            Console.WriteLine(counter.GetHits(301));
        }

        public class HitCounter
        {

            private int[] _hits = new int[300];
            private int _lastTimestamp = 1;
            private int _idx;

            /** Initialize your data structure here. */
            public HitCounter()
            {

            }

            /** Record a hit.
                @param timestamp - The current timestamp (in seconds granularity). */
            public void Hit(int timestamp)
            {
                if (timestamp > _lastTimestamp + 300) _hits = new int[300];
                else
                {
                    for (var i = _lastTimestamp + 1; i < timestamp; ++i)
                    {
                        _hits[(i - 1) % 300] = 0;
                    }
                }
                _hits[(timestamp - 1) % 300]++;
                _lastTimestamp = timestamp;
            }

            /** Return the number of hits in the past 5 minutes.
                @param timestamp - The current timestamp (in seconds granularity). */
            public int GetHits(int timestamp)
            {
                if (timestamp >= _lastTimestamp + 300) return 0;
                var result = 0;

                for (var i = Math.Max(1, timestamp - 299); i <= _lastTimestamp; ++i)
                {
                    result += _hits[(i - 1) % 300];
                }

                return result;
            }
        }
    }
}
