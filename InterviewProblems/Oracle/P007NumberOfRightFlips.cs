using System;

namespace InterviewProblems.Oracle
{
    public class P007NumberOfRightFlips
    {
        // https://leetcode.com/discuss/interview-experience/516188/Oracle-OCI-oror-Bangalore-oror-Offer
        // Use merge sort

        // https://www.geeksforgeeks.org/number-swaps-sort-adjacent-swapping-allowed/
        public void NumberOfRightFlipsTest()
        {
            var arr = new[] { 3, 2, 1 };
            Console.WriteLine($"[{string.Join(',', arr)}] => {NumberOfRightFlips(arr)}");

            var arr2 = new[] { 1,3,4,2,6,8,5,7 };
            Console.WriteLine($"[{string.Join(',', arr2)}] => {NumberOfRightFlips(arr2)}");
        }

        public int NumberOfRightFlips(int[] arr)
        {
            var result = 0;
            var buffer = new int[arr.Length];
            MergeSort(0, arr.Length-1);

            void MergeSort(int start, int end)
            {
                if (start == end) return;
                var mid = start + (end - start) / 2;
                MergeSort(start,mid);
                MergeSort(mid+1,end);
                Merge(start, mid, mid+1, end);
            }

            void Merge(int s1, int e1, int s2, int e2)
            {
                var idx = s1;
                var (i, j) = (s1, s2);
                for ( ; i <= e1 && j <= e2; ++idx)
                {
                    if (arr[i] <= arr[j]) buffer[idx] = arr[i++];
                    else
                    {
                        result += e1 - i + 1;
                        buffer[idx] = arr[j++];
                    }
                }

                for (; i <= e1; ++idx, ++i) buffer[idx] = arr[i];
                for (; j <= e2; ++idx, ++j) buffer[idx] = arr[j];
                Array.Copy(buffer, s1, arr, s1, e2-s1+1);
            }

            return result;
        }
    }
}
