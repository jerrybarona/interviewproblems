using System;
using System.Linq;

namespace InterviewProblems.TikTok
{
    public class P007MaximumXorSuffix : ITestable
    {
        // https://leetcode.com/discuss/interview-question/4734570/tiktok-OA
        
        public void RunTest()
        {
            foreach (var arr in new int[][]
            {
                new [] { 0, 2, 5, 1},
                new [] { 1, 2, 3 },
                new [] { 8, 2, 4, 12, 1},
                new [] { 7611, 417, 7163, 6436 }
            })
            {
                Console.WriteLine(MaxXorSuffix(arr));
            }
        }

        public int MaxXorSuffix(int[] arr)
        {
            var trie = new Trie();

            return arr.Aggregate((result: 0, prefixXor: 0), (t, num) =>
            {
                var (result,  prefixXor) = t;
                prefixXor ^= num;
                trie.Insert(prefixXor);
                result = Math.Max(result, trie.MaxXor(prefixXor));

                return (result,  prefixXor);
            }).result;
        }

        class Trie
        {
            private readonly Node _root = new();

            public Trie()
            {
                Insert(0);
            }

            public void Insert(int num)
            {
                var node = _root;
                for (var i = 31; i >= 0; i--)
                {
                    var bit = (num >> i) & 1;
                    if (node.Children[bit] == null) node.Children[bit] = new Node();

                    node = node.Children[bit];
                }
            }

            public int MaxXor(int num)
            {
                var node = _root;
                var result = 0;
                for (var i = 31; i >= 0; i--)
                {
                    var numBit = (num >> i) & 1;
                    var negBit = 1 - numBit;

                    if (node.Children[negBit] != null)
                    {
                        result |= (1 << i);
                        node = node.Children[negBit];
                    }
                    else
                    {
                        node = node.Children[numBit];
                    }
                }

                return result;
            }
        }
        
        class Node
        {
            public Node[] Children { get; } = new Node[] { null, null };
        }
    }
}
