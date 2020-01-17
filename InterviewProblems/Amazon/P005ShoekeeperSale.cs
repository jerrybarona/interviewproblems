using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Amazon
{
    public class P005ShoekeeperSale
    {
        // https://aonecode.com/amazon-online-assessment-questions

        public (int total,int[] nondisc) Sales(int[] items)
        {
            var stack = new Stack<int>(items.Length);
            stack.Push(0);

            var costs = new int[items.Length];
            Array.Copy(items, costs, items.Length);

            for (var i = 1; i < items.Length; ++i)
            {
                while (stack.Count > 0 && items[i] <= items[stack.Peek()])
                {
                    var idx = stack.Pop();
                    costs[idx] -= items[i];
                }
                stack.Push(i);
            }

            var total = costs.Sum();
            var nondisc = Enumerable.Range(0, items.Length).Where(x => items[x] == costs[x]).ToArray();

            return (total, nondisc);
        }
    }
}
