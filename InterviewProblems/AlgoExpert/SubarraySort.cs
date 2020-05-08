using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.AlgoExpert
{
    public class SubarraySort
    {
		public void SubSortTest()
		{
			var arrays = new[] { new[] { 1, 2 }, new[] { 2,1}, new[] { 1,2,4,7,10,11,7,12,6,7,16,18,19}, new[] { 1, 2, 4, 7, 10, 11, 7, 12, 7, 7, 16, 18, 19 },
			new[]{1,2,4,7,10,11,7,12,13,14,16,18,19 },new[]{4,8,7,12,11,9,-1,3,9,16,-15,51,7 },new[]{4,8,7,12,11,9,-1,3,9,16,-15,11,57 }, new[]{-41,8,7,12,11,9,-1,3,9,16,-15,11,57 }
			};

			foreach (var array in arrays)
			{
				var r = SubSort(array);
				Console.WriteLine($"{r[0]}, {r[1]}");
			}
		}

        public int[] SubSort(int[] array)
        {
			var len = array.Length;
			var lo = 1;
			var hi = len - 2;

			while (lo < len && array[lo] >= array[lo - 1]) ++lo;
			if (lo == len) return new[] { -1, -1 };

			while (hi >= 0 && array[hi] <= array[hi + 1]) --hi;
			--lo;
			++hi;
			var min = array[hi];
			var max = array[lo];

			if (hi > lo + 1)
			{
				for (var i = lo + 1; i < hi; ++i)
				{
					min = Math.Min(min, array[i]);
					max = Math.Max(max, array[i]);
				}
			}			

			while (lo >= 0 && array[lo] > min) --lo;
			while (hi < len && array[hi] < max) ++hi;

			return new[] { Math.Max(0, lo + 1), Math.Min(len - 1, hi - 1) };
		}
    }
}
