using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.CodeChef
{
	public class SnakeEat
	{
		// https://www.codechef.com/SNCKQL17/problems/SNAKEEAT
		public void SnakeEatTest()
		{
			int[] l;
			int[] q;

			l = new[] { 21, 9, 5, 8, 10 };
			q = new[] { 10, 15 };
			Console.WriteLine($"[ {string.Join(", ", MaxNumberOfSnakes(l, q))} ]");

			l = new[] { 1, 2, 3, 4, 5 };
			q = new[] { 100, 6, 3, 1, 9, 8 };
			Console.WriteLine($"[ {string.Join(", ", MaxNumberOfSnakes(l, q))} ]");
		}

		public IEnumerable<int> MaxNumberOfSnakes(int[] lengths, IEnumerable<int> queries)
		{
			Array.Sort(lengths, (a, b) => b - a);
			var len = lengths.Length;
			var cummSum = lengths.Aggregate(new List<int>(), (list, num) =>
			{
				var count = list.Count;
				if (count == 0) list.Add(num);
				else list.Add(list.Last() + num);

				return list;
			});

			return queries.Select(q => Query(q));

			int Query(int val)
			{
				var orig = FindClosestFirst(val - 1);
				if (orig == -1) return len;

				var minSum = orig > 0 ? cummSum[orig - 1] : 0;
				var idx = orig-1;

				for (var (start, end, mid) = (orig, len-1, orig + (len - 1 - orig) /2); start <= end; mid = start + (end-start)/2)
				{
					var queriedSum = (mid - orig + 1)*val - (cummSum[mid] - minSum);
					
					if (len - 1 - mid < queriedSum) end = mid - 1;
					else
					{
						idx = mid;
						start = mid + 1;
					}
				}

				return idx + 1;
			}

			int FindClosestFirst(int val)
			{
				var result = -1;
				for (var (start, end, mid) = (0, len-1, (len-1)/2); start <= end; mid = start + (end-start)/2)
				{
					if (lengths[mid] > val) start = mid + 1;
					else
					{
						result = mid;
						end = mid - 1;
					}
				}

				return result;
			}
		}
	}
}
