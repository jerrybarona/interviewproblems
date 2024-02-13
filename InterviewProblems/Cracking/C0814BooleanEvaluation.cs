using System;
using System.Collections.Generic;

namespace InterviewProblems.Cracking
{
    public class C0814BooleanEvaluation : ITestable
    {
        public void RunTest()
        {
            foreach (var (s, r) in new (string, bool)[]
            {
                ("1^0|0|1", false),
                ("0&0&0&1^1|0", true),
            })
            {
                Console.WriteLine($"countEval(\"{s}\", {r}) -> {CountEval(s, r)}");
            }
        }

        private int CountEval(string s, bool r)
        {
            var memo = new Dictionary<(int, int, bool), int>();

            return Process(0, s.Length - 1, r);

            int Process(int start, int end, bool result)
            {
                if (start == end)
                {
                    if (result) return s[start] == '1' ? 1 : 0;
                    return s[start] == '0' ? 1 : 0;
                }

                if (memo.ContainsKey((start, end, result))) return memo[(start, end, result)];

                var count = 0;
                for (int i = start; i < end; i++)
                {
                    if (char.IsDigit(s[i])) continue;

                    if (s[i] == '&')
                    {
                        count += result
                            ? (Process(start, i - 1, true) * Process(i + 1, end, true))
                            : (Process(start, i - 1, false) * Process(i + 1, end, true))
                                + (Process(start, i - 1, true) * Process(i + 1, end, false))
                                + (Process(start, i - 1, false) * Process(i + 1, end, false));
                    }
                    else if (s[i] == '|')
                    {
                        count += !result
                            ? (Process(start, i - 1, false) * Process(i + 1, end, false))
                            : (Process(start, i - 1, false) * Process(i + 1, end, true))
                                + (Process(start, i - 1, true) * Process(i + 1, end, false))
                                + (Process(start, i - 1, true) * Process(i + 1, end, true));
                    }
                    else if (s[i] == '^')
                    {
                        count += result
                            ? (Process(start, i - 1, true) * Process(i + 1, end, false))
                                + (Process(start, i - 1, false) * Process(i + 1, end, true))
                            : (Process(start, i - 1, false) * Process(i + 1, end, false))
                                + (Process(start, i - 1, true) * Process(i + 1, end, true));
                    }
                }

                memo.Add((start, end, result), count);

                return count;
            }           
        }
    }
}
