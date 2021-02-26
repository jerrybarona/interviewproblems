using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.LeetCode
{
	public class P0752OpenTheLock
	{
        public void OpenLockTest()
		{
            Console.WriteLine(OpenLock(new[] { "0201", "0101", "0102", "1212", "2002" }, "0202"));
		}

        public int OpenLock(string[] deadends, string target)
        {
            if (string.Equals("0000", target)) return 0;

            var deads = new HashSet<string>(deadends);

            if (deads.Contains("0000")) return -1;

            var seen = new HashSet<string>(10000);
            seen.Add("0000");

            var queue = new Queue<string>(10000);
            queue.Enqueue("0000");


            for (var result = 1; queue.Count > 0; ++result)
            {
                for (var count = queue.Count; count > 0; --count)
                {
                    var s = queue.Dequeue();
                    for (var i = 0; i < 4; ++i)
                    {
                        foreach (var combo in Combos(s, i))
                        {
                            if (string.Equals(target, combo)) return result;
                            if (!deads.Contains(combo) && seen.Add(combo)) queue.Enqueue(combo);
                        }
                    }
                }
            }

            return -1;

            IEnumerable<string> Combos(string str, int idx)
            {
                var chr = str[idx] == '9' ? '0' : (char)(str[idx] + 1);
                yield return $"{str.Substring(0, idx)}{chr}{str.Substring(idx + 1)}";

                chr = str[idx] == '0' ? '9' : (char)(str[idx] - 1);
                yield return $"{str.Substring(0, idx)}{chr}{str.Substring(idx + 1)}";
            }
        }
    }
}
