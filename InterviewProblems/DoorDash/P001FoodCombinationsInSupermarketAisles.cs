using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.DoorDash
{
    internal class P001FoodCombinationsInSupermarketAisles : ITestable
    {
        // https://leetcode.com/discuss/interview-question/5121764/DoorDash-Phone-Screen

        public void RunTest()
        {
            foreach (var (contiguous, aisle) in new (int[], string)[]
            {
                (new [] { 1,2 }, "?F?WW??"), // output: WFWWWFF
                (new [] { 1,1 }, "?F?WW??"), // output: WFWWWFW,WFWWWWF
                (new [] { 3 }, "WWWWWWW"), // output: ...
                (new [] { 3 }, "??????"), // output: FFFWWW,WFFFWW,WWFFFW,WWWFFF
                (new [] { 1, 3 }, "??????"), // output: FWFFFW,FWWFFF,WFWFFF
            })
            {
                Console.WriteLine(string.Join(",", FoodCombinations(contiguous, aisle)));
            }
        }

        public IList<string> FoodCombinations(int[] contiguous, string aisle)
        {
            var sb = new StringBuilder();
            var result = new List<string>();
            Combine(0, 0);

            return result;

            void Combine(int cidx, int aidx)
            {
                if (cidx >= contiguous.Length)
                {
                    var rem = new StringBuilder();
                    while (aidx < aisle.Length)
                    {
                        rem.Append(aisle[aidx] == 'F' ? 'F' : 'W');
                        ++aidx;
                    }
                    result.Add(sb.ToString() + rem.ToString());
                    return;
                }

                var count = contiguous[cidx];
                for (var (s,e) = (aidx,aidx); e < aisle.Length; )
                {
                    while (e < aisle.Length && aisle[e] == 'W')
                    {
                        sb.Append('W');
                        ++s;
                        ++e;
                    }

                    while (e < aisle.Length && (aisle[e] == 'F' || aisle[e] == '?'))
                    {
                        sb.Append('F');
                        if (e - s + 1 == count) break;
                        ++e;
                    }

                    if ((e == aisle.Length - 1 || (e < aisle.Length - 1 && aisle[e + 1] != 'F')) && e - s + 1 == count)
                    {
                        if (e + 1 < aisle.Length) sb.Append('W');
                        ++e;
                        Combine(cidx + 1, e + 1);
                    }

                    sb.Length = s;
                    while (s < aisle.Length && aisle[s] == 'F')
                    {
                        sb.Append('F');
                        ++s;
                    }

                    if (s < aisle.Length && (aisle[s] == '?' || aisle[s] == 'W'))
                    {
                        sb.Append('W');
                        ++s;
                    }

                    e = s;
                }
            }
        }
    }
}
