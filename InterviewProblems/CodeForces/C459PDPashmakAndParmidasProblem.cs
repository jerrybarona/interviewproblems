using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace InterviewProblems.CodeForces
{
    public class C459PDPashmakAndParmidasProblem : ITestable
    {
        // https://codeforces.com/contest/459/problem/D

        public void RunTest()
        {
            foreach (var a in new int[][]
            {
                new[]{ 1, 2, 1, 1, 2, 2, 1 },
                new[]{1,1,1},
                new[]{1,2,3,4,5}
            })
            {
                Console.WriteLine(Solve(a));
            }
        }

        public int Solve(int[] a)
        {
            var map = new Dictionary<int, int>();
            var jcount = new int[a.Length];
            for (var idx = a.Length - 1; idx >= 0; --idx)
            {
                var j = a[idx];
                if (!map.ContainsKey(j)) map[j] = 0;
                ++map[j];

                jcount[idx] = map[j];
            }

            map.Clear();
            var icount = new int[a.Length];
            for (int idx = 0; idx < a.Length; ++idx)
            {
                var i = a[idx];
                if (!map.ContainsKey(i)) map[i] = 0;
                ++map[i];

                icount[idx] = map[i];
            }
            map.Clear();
            foreach (var (key, idx) in icount.OrderByDescending(x => x).Distinct().Select((key, idx) => (key, idx)))
            {
                map[key] = idx;
            }
            var st = new SegmentTree(map.Count);

            var result = 0;
            for (var index = 0; index < a.Length - 1;  ++index)
            {
                var idx = map[icount[index]];
                st.Add(idx);
                var jidx = map[jcount[index + 1]];
                result += st.Query(0, jidx - 1);
            }

            return result;
        }

        class SegmentTree
        {
            private readonly int _len;
            private readonly int[] _tree;

            public SegmentTree(int len)
            {
                _len = len;
                _tree = new int[2*BitOperations.RoundUpToPowerOf2((uint)_len) - 1];
            }

            public void Add(int index)
            {
                AddVal(0, _len - 1, 0);

                void AddVal(int l, int r, int i)
                {
                    if (l == r)
                    {
                        ++_tree[i];
                        return;
                    }

                    var mid = l + (r - l) / 2;
                    if (index <= mid) AddVal(l, mid, 2 * i + 1);
                    else AddVal(mid + 1, r, 2 * i + 2);

                    _tree[i] = _tree[2*i + 1] + _tree[2*i + 2];
                }
            }

            public int Query(int left, int right)
            {
                return Sum(0, _len - 1, 0);

                int Sum(int l, int r, int i)
                {
                    if (left <= l && right >= r)
                    {
                        return _tree[i];
                    }

                    if (left > r || right < l) return 0;

                    var mid = l + (r - l) / 2;

                    return Sum(l, mid, 2 * i + 1) + Sum(mid + 1, r, 2 * i + 2);
                }
            }
        }
    }
}
