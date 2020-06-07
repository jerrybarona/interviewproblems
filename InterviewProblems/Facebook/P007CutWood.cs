using System;
using System.Linq;

namespace InterviewProblems.Facebook
{
    public class P007CutWood
    {
        // https://leetcode.com/discuss/interview-question/354854/

        public void CutWoodTest()
        {
            var wood = new[] { 5, 9, 7 };
            var k = 3;
            Console.WriteLine($"wood = [{string.Join(',', wood)}], k = {k}, Output = {CutWood(wood, k)}");

            wood = new[] { 5, 9, 7 };
            k = 4;
            Console.WriteLine($"wood = [{string.Join(',', wood)}], k = {k}, Output = {CutWood(wood, k)}");

            wood = new[] { 1,2,3 };
            k = 7;
            Console.WriteLine($"wood = [{string.Join(',', wood)}], k = {k}, Output = {CutWood(wood, k)}");

            wood = new[] { 232, 124, 456 };
            k = 7;
            Console.WriteLine($"wood = [{string.Join(',', wood)}], k = {k}, Output = {CutWood(wood, k)}");

            wood = new[] { 5, 20, 20 };
            k = 4;
            Console.WriteLine($"wood = [{string.Join(',', wood)}], k = {k}, Output = {CutWood(wood, k)}");
        }

        public int CutWood(int[] wood, int k)
        {
            var start = 0;
            var end = wood.Min();

            for (var mid = start + 1 + (end - start - 1) / 2; start < end; mid = start + 1 + (end - start - 1) / 2)
            {
                if (CanCut(mid)) start = mid;
                else end = mid - 1;
            }

            return start;

            bool CanCut(int n) => wood.Aggregate(0, (total, piece) => total + piece / n) >= k;
        }
    }
}
