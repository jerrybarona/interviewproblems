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
			foreach (var (lengths, queries) 
				in new (int[] lengths, int[] queries)[]
				{
					(new[] { 21, 9, 5, 8, 10 }, new[] { 10, 15 }),
					(new[] { 1, 2, 3, 4, 5 }, new[] { 100, 6, 3, 1, 9, 8 })
				})
			{
				Console.WriteLine($"\nSnakes:\n[ {string.Join(", ", lengths)} ]");
				Console.WriteLine($"Queries:\n[ {string.Join(", ", queries)} ]");
				Console.WriteLine($"Result:\n[ {string.Join(", ", MaxNumberOfSnakes(lengths, queries))} ]");
			}
		}

		/*
		 Optimal solution.
		
		Time Complexity analysis
		------------------------
		Let L be the number of snakes and Q be the number of queries.
		Then, the time complexity will be:
		  O(L * log(L)) + O(Q * log(L)) = O ( (L+Q)*log(L) ) 
		  
		  Sorting will take L * log (L).
		  For each query in queries, a binary search operation will be performed
		 */
		public IEnumerable<int> MaxNumberOfSnakes(int[] lengths, IEnumerable<int> queries)
		{
			Array.Sort(lengths, (a, b) => b - a);
			var len = lengths.Length;

			var prefixSum = lengths
				.Aggregate(
					(list: Enumerable.Empty<int>(), acumm: 0),
					(t, num) => (t.list.Append(num + t.acumm), num + t.acumm))
				.list
				.ToArray();

			return queries.Select(q => Query(q));

			int Query(int val)
			{
				var orig = FindClosestFirst(val - 1);
				if (orig == -1) return len;

				var minSum = orig > 0 ? prefixSum[orig - 1] : 0;
				var idx = orig-1;

				for (var (start, end, mid) = (orig, len-1, orig + (len - 1 - orig) /2); start <= end; mid = start + (end-start)/2)
				{
					var queriedSum = (mid - orig + 1)*val - (prefixSum[mid] - minSum);
					
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
