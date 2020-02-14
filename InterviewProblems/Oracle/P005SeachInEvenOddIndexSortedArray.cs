using System;

namespace InterviewProblems.Oracle
{
    public class P005SeachInEvenOddIndexSortedArray
    {
        public void SearchInEvenOddIndexSortedArrayTest()
        {
            var arr = new[] { 10, 4, 6, 5, 5, 6, 2 };

            Console.WriteLine(SearchInEvenOddIndexSortedArray(arr, 3));

            var arr1 = new[] { 5, 2 };
            Console.WriteLine(SearchInEvenOddIndexSortedArray(arr1, 3));
        }

        public int SearchInEvenOddIndexSortedArray(int[] arr, int target)
        {
            if (arr == null || arr.Length == 0) return -1;
            if (arr.Length == 1) return arr[0] == target ? 0 : -1;

            var len = arr.Length;
            var estart = 0;
            var ostart = 1;
            var eend = (len - 1) % 2 == 0 ? len - 1 : len - 2;
            var oend = (len - 1) % 2 == 1 ? len - 1 : len - 2;

            // even index binary search

            for (var mid = (estart + (eend - estart) / 2) % 2 == 0
                     ? estart + (eend - estart) / 2
                     : (estart + (eend - estart) / 2) - 1;
                 estart <= eend;
                 mid = (estart + (eend - estart) / 2) % 2 == 0
                     ? estart + (eend - estart) / 2
                     : (estart + (eend - estart) / 2) - 1)
            {
                if (arr[mid] == target) return mid;
                if (target > arr[mid]) eend = mid - 2;
                else estart = mid + 2;
            }

            // odd index binary search

            for (var mid = (ostart + (oend - ostart) / 2) % 2 == 1
                     ? ostart + (oend - ostart) / 2
                     : (ostart + (oend - ostart) / 2) - 1;
                 ostart <= oend;
                 mid = (ostart + (oend - ostart) / 2) % 2 == 1
                     ? ostart + (oend - ostart) / 2
                     : (ostart + (oend - ostart) / 2) - 1)
            {
                if (arr[mid] == target) return mid;
                if (target > arr[mid]) ostart = mid + 2;
                else oend = mid - 2;
            }

            return -1;
        }
    }
}
