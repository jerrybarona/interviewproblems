using System;
using System.Linq;

namespace InterviewProblems.InterviewCake
{
    public class CakeThief
    {
        public void CakeThiefTest()
        {
            CakeType[] cakeTypes = new[] { new CakeType(2, 1) };
            var weightCapacity = 9;
            Console.WriteLine(MaxDuffelBagValue(cakeTypes, weightCapacity));

            cakeTypes = new[] { new CakeType(4, 4), new CakeType(5, 5) };
            weightCapacity = 9;
            Console.WriteLine(MaxDuffelBagValue(cakeTypes, weightCapacity));

            cakeTypes = new[] { new CakeType(4, 4), new CakeType(5, 5) };
            weightCapacity = 12;
            Console.WriteLine(MaxDuffelBagValue(cakeTypes, weightCapacity));

            cakeTypes = new[]
            {
                new CakeType(2, 3), new CakeType(3, 6), new CakeType(5, 1),
                new CakeType(6, 1), new CakeType(7, 1), new CakeType(8, 1)
            };
            weightCapacity = 7;
            Console.WriteLine(MaxDuffelBagValue(cakeTypes, weightCapacity));

            cakeTypes = new[] { new CakeType(51, 52), new CakeType(50, 50) };
            weightCapacity = 100;
            Console.WriteLine(MaxDuffelBagValue(cakeTypes, weightCapacity));

            cakeTypes = new[] { new CakeType(1, 2) };
            weightCapacity = 0;
            Console.WriteLine(MaxDuffelBagValue(cakeTypes, weightCapacity));

            cakeTypes = new[] { new CakeType(0, 0), new CakeType(2, 1) };
            weightCapacity = 7;
            Console.WriteLine(MaxDuffelBagValue(cakeTypes, weightCapacity));

            cakeTypes = new[] { new CakeType(0, 5) };
            weightCapacity = 5;
            Console.WriteLine(MaxDuffelBagValue(cakeTypes, weightCapacity));
        }

        public class CakeType
        {
            public readonly int Weight;
            public readonly int Value;

            public CakeType(int weight, int value)
            {
                Weight = weight;
                Value = value;
            }
        }

        public static long MaxDuffelBagValue(CakeType[] cakeTypes, int weightCapacity)
        {
            // Calculate the maximum value that we can carry
            var memo = Enumerable.Repeat(0, cakeTypes.Length)
                .Select(x => Enumerable.Repeat((long)-1, weightCapacity + 1).ToArray())
                .ToArray();

            return Take(memo, cakeTypes, 0, weightCapacity);
        }

        private static long Take(long[][] memo, CakeType[] cakeTypes, int idx, int weight)
        {
            if (idx == cakeTypes.Length || weight == 0) return 0;
            if (memo[idx][weight] > -1) return memo[idx][weight];

            var result = Take(memo, cakeTypes, idx + 1, weight);
            
            var w = cakeTypes[idx].Weight;
            var v = cakeTypes[idx].Value;

            if (w > 0)
            {
                for (; w <= weight;)
                {
                    result = Math.Max(result, v + Take(memo, cakeTypes, idx + 1, weight - w));
                    w += cakeTypes[idx].Weight;
                    v += cakeTypes[idx].Value;
                }
            }

            memo[idx][weight] = result;
            return result;
        }
    }
}
