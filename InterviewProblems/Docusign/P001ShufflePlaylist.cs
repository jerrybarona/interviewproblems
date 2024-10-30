using System;
using System.Linq;

namespace InterviewProblems.Docusign
{
    internal class P001ShufflePlaylist : ITestable
    {
        private readonly Random _random = new Random();
        public void RunTest()
        {
            foreach (var arr in new int[][]
            {
                new [] { 3, 2, 5, 4, 58, 93, 44, 53, 76, 11, 27 },
            })
            {
                Console.WriteLine("Orig. array:");
                Console.WriteLine(string.Join(", ", arr));
                Console.WriteLine("Shuffles:");
                for (var i = 0; i < 100; i++)
                {
                    Console.WriteLine(string.Join(", ", Shuffle(arr)));
                }
            }
        }

        private int[] Shuffle(int[] arr)
        {
            var idxarr = Enumerable.Range(0, arr.Length).Select(i => (num: arr[i], idx: i)).ToArray();
            var prevIdx = -2;

            for (var i = 0; i < arr.Length; i++)
            {
                var ridx = _random.Next(i, arr.Length);
                var nextIdx = idxarr[ridx].idx;

                if (i == arr.Length - 1 && nextIdx == prevIdx + 1) return Shuffle(arr);

                while (nextIdx == prevIdx + 1)
                {
                    ridx = _random.Next(i, arr.Length);
                    nextIdx = idxarr[ridx].idx;
                }
                (idxarr[i], idxarr[ridx]) = (idxarr[ridx], idxarr[i]);
                prevIdx = nextIdx;
            }

            return idxarr.Select(x => x.num).ToArray();
        }
    }
}
