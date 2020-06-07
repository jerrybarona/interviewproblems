using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.AlgoExpert
{
    public class SameBsts
    {
        public void SameBstTest()
        {
            var a1 = new List<int> { 10, 15, 8 };
            var a2 = new List<int> { 10, 8, 15 };
            Console.WriteLine(SameBst(a1, a2));
        }

        public bool SameBst(List<int> arrayOne, List<int> arrayTwo)
        {
            if (arrayOne.Count == 0 && arrayTwo.Count == 0) return true;
            if (arrayOne.Count != arrayTwo.Count || !arrayOne.All(arrayTwo.Contains)) return false;

            return IsBst(arrayOne, arrayTwo, 0, int.MinValue, int.MaxValue, int.MinValue, int.MaxValue);

            bool IsBst(List<int> arr1, List<int> arr2, int idx, int min1, int max1, int min2, int max2)
            {
                if (idx == arr1.Count) return true;
                if (arr1[idx] <= min1 || arr1[idx] > max1 || arr2[idx] <= min2 || arr2[idx] > max2) return false;
                return
                    IsBst(arr1, arr2, idx + 1, arr1[idx], max1, arr2[idx], max2) ||
                    IsBst(arr1, arr2, idx + 1, min1, arr1[idx], min2, arr2[idx]) ||
                    IsBst(arr1, arr2, idx + 1, min1, arr1[idx], arr2[idx], max2) ||
                    IsBst(arr1, arr2, idx + 1, arr1[idx], max1, min2, arr2[idx]);
            }
        }
    }
}
