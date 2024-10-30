using System;

namespace InterviewProblems.Amazon
{
    internal class P015FindServerAssignmentInLoadBalancer : ITestable
    {
        // https://leetcode.com/discuss/interview-question/5482792/Amazon-OA

        public void RunTest()
        {
            foreach (var (numServers, requests) in new (int, int[])[]
            {
                (5, new [] { 3, 2, 3, 2, 4 }), // 0, 1, 2, 0, 3                
            })
            {
                Console.WriteLine(string.Join(", ", AssignServer(numServers, requests)));
            }
        }

        private int[] AssignServer(int numServers, int[] requests)
        {           
            var st = new SegmentTree(numServers);
            var requestsLen = requests.Length;
            var result = new int[requestsLen];

            for (var i = 0; i < requestsLen; i++)
            {
                var (assignedId, currAllocation) = st.Query(0, requests[i], 0, numServers - 1, 0);
                result[i] = assignedId;

                st.Update(currAllocation + 1, assignedId, 0, numServers - 1, 0);
            }

            return result;
        }

        class SegmentTree
        {
            private readonly (int id, int minVal)[] _tree;
            private readonly int _n;

            public SegmentTree(int n)
            {
                for (var i = n; i > 0; )
                {
                    ++_n;
                    i >>= 1;
                }

                ++_n;
                _tree = new (int id, int minVal)[1 << _n];
                Build(0, n - 1, 0);
            }

            private (int id, int minVal) Min(int idx1, int idx2)
            {
                var t1 = _tree[idx1];
                var t2 = _tree[idx2];

                if (t1.minVal == t2.minVal)
                {
                    return t1.id < t2.id ? t1 : t2;
                }

                return t1.minVal < t2.minVal ? t1 : t2;
            }

            private (int id, int minVal) Min((int id, int minVal) t1, (int id, int minVal) t2)
            {
                if (t1.minVal == t2.minVal)
                {
                    return t1.id < t2.id ? t1 : t2;
                }

                return t1.minVal < t2.minVal ? t1 : t2;
            }

            private void Build(int l, int r, int i)
            {
                if (l == r)
                {
                    _tree[i] = (l, 0);
                    return;
                }

                var m = l + (r - l) / 2;
                Build(l, m, i*2 + 1);
                Build(m + 1, r, i * 2 + 2);

                _tree[i] = Min(i*2 + 1, i*2 + 2);
            }

            public void Update(int val, int idx, int l, int r, int i)
            {
                if (l == r)
                {
                    _tree[i] = (l, val);
                    return;
                }

                var m = l + (r - l) / 2;
                if (idx <= m)
                {
                    Update(val, idx, l, m, i * 2 + 1);
                }
                else
                {
                    Update(val, idx, m+1, r, i * 2 + 2);
                }

                _tree[i] = Min(i * 2 + 1, i * 2 + 2);
            }

            public (int id, int minVal) Query(int x, int y, int l, int r, int i)
            {
                if (x > r || y < l)
                {
                    return (int.MaxValue, int.MaxValue);
                }

                if (l >= x && r <= y)
                {
                    return _tree[i];
                }

                var m = l + (r - l) / 2;

                return Min(Query(x, y, l, m, i * 2 + 1), Query(x, y, m + 1, r, i * 2 + 2));
            }
        }
    }
}
