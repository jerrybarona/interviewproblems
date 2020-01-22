using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0907SumOfSubarrayMinimums
    {
        public void SumSubarrayMinsTest()
        {
            var input = new[] { 3, 1, 2, 4, 1 };
            Console.WriteLine(string.Join(',', input));
            Console.WriteLine("Result:");
            Console.WriteLine(SumSubarrayMins(input));
        }

        public int SumSubarrayMins(int[] A)
        {
            var mins = new LinkedList<int>();
            var result = 0;
            var sum = 0;

            for (var i = 0; i < A.Length; ++i)
            {
                var removed = 1;
                for (; mins.Count > 0 && A[i] < A[mins.Last.Value]; ++removed, sum -= A[mins.Last.Value], mins.RemoveLast()) ;
                for (; removed > 0; --removed, sum += A[i], mins.AddLast(i)) ;

                result += sum;
            }

            return result;
        }
    }
}
