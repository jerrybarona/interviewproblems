using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.LeetCode
{
    internal class P1434NumberOfWaysToWearDifferentHatsToEachOther : ITestable
    {
        public void RunTest()
        {
            foreach (var hats in new List<IList<int>>[]
            {
                new() { new List<int> { 3, 5, 1 }, new List<int> { 3, 5 } }
            })
            {
                Console.WriteLine(NumberWays(hats));
            }
        }

        public int NumberWays(IList<IList<int>> hats)
        {
            var people = new List<int>[41];
            for (var i = 0; i < hats.Count; ++i)
            {
                foreach (var hat in hats[i])
                {
                    if (people[hat] == null) people[hat] = new List<int>();
                    people[hat].Add(i);
                }
            }

            var wearing = new bool[hats.Count];
            long result = 0;
            Process();
            return (int)(result % 1000000007);

            void Process(int idx = 1)
            {
                if (idx == people.Length)
                {
                    ++result;
                    return;
                }

                if (people[idx] == null)
                {
                    Process(idx + 1);
                    return;
                }

                foreach (var person in people[idx])
                {
                    if (!wearing[person])
                    {
                        wearing[person] = true;
                        Process(idx + 1);
                        wearing[person] = false;
                    }
                }
            }
        }
    }
}
