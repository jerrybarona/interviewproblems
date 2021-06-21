using System;

namespace InterviewProblems.Microsoft
{
	public class P040KthSmallestElementOutOf2SortedArrays
	{
		private int[] arr1;
		private int[] arr2;
		private int k;

		public void KthSmallestTest()
		{
			arr1 = new[] { 1, 5, 6, 11, 20, 23, 30 };
			arr2 = new[] { 2, 4, 9, 16, 17, 25 };
			k = 9;

			Console.WriteLine($"arr 1: [{string.Join(", ", arr1)}]\narr 2: [{string.Join(", ", arr2)}]\nk: {k}");
			Console.WriteLine($"\nANS: {KthSmallest(arr1, arr2, k)}");
		}

		public int KthSmallest(int[] arr1, int[] arr2, int k)
		{
			var small = arr1.Length <= arr2.Length ? arr1 : arr2;
			var large = small == arr1 ? arr2 : arr1;

			var slen = small.Length;
			var llen = large.Length;

			var len = Math.Min(small.Length, k);
			
			for (var (s, e, smid) = (0, len, len/2); s <= e; smid = s + (e-s)/2)
			{
				var lmid = k - smid;
				var sLeftMax = smid > 0 ? small[smid - 1] : int.MinValue;
				var lLeftMax = lmid > 0 ? large[lmid - 1] : int.MinValue;

				var sRightMin = smid < slen ? small[smid] : int.MaxValue;
				var lRightMin = lmid < llen ? large[lmid] : int.MaxValue;

				if (sLeftMax <= lRightMin && lLeftMax <= sRightMin) return Math.Max(sLeftMax, lLeftMax);
				else if (sLeftMax > lRightMin) e = smid - 1;
				else s = smid + 1;
			}

			return int.MinValue;
		}
	}
}
