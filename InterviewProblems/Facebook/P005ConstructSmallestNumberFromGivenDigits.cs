using System;
using System.Linq;

namespace InterviewProblems.Facebook
{
    public class P005ConstructSmallestNumberFromGivenDigits
    {
        // https://leetcode.com/discuss/interview-question/522950/Facebook-or-Phone-or-Construct-smallest-number-from-given-digits

        public void ConstructSmallestNumberFromGivenDigitsTest()
        {
            var digits = new[] { 8, 5, 2 };
            var lb = 100;
            Console.WriteLine($"digits: [{string.Join(',', digits)}], lowerBound: {lb}; result: {ConstructSmallestNumberFromGivenDigits(digits, lb)}");

            digits = new[] { 8, 5, 2 };
            lb = 260;
            Console.WriteLine($"digits: [{string.Join(',', digits)}], lowerBound: {lb}; result: {ConstructSmallestNumberFromGivenDigits(digits, lb)}");

            digits = new[] { 8, 5, 2 };
            lb = 500;
            Console.WriteLine($"digits: [{string.Join(',', digits)}], lowerBound: {lb}; result: {ConstructSmallestNumberFromGivenDigits(digits, lb)}");

            digits = new[] { 8, 5, 2 };
            lb = 528;
            Console.WriteLine($"digits: [{string.Join(',', digits)}], lowerBound: {lb}; result: {ConstructSmallestNumberFromGivenDigits(digits, lb)}");

            digits = new[] { 8, 5, 2 };
            lb = 585;
            Console.WriteLine($"digits: [{string.Join(',', digits)}], lowerBound: {lb}; result: {ConstructSmallestNumberFromGivenDigits(digits, lb)}");

            digits = new[] { 9,0,0 };
            lb = 0;
            Console.WriteLine($"digits: [{string.Join(',', digits)}], lowerBound: {lb}; result: {ConstructSmallestNumberFromGivenDigits(digits, lb)}");

            digits = new[] { 9, 0, 0 };
            lb = 10;
            Console.WriteLine($"digits: [{string.Join(',', digits)}], lowerBound: {lb}; result: {ConstructSmallestNumberFromGivenDigits(digits, lb)}");

            digits = new[] { 8,5,2,0 };
            lb = 852;
            Console.WriteLine($"digits: [{string.Join(',', digits)}], lowerBound: {lb}; result: {ConstructSmallestNumberFromGivenDigits(digits, lb)}");

            digits = new[] { 8, 5, 2, 1 };
            lb = 5834;
            Console.WriteLine($"digits: [{string.Join(',', digits)}], lowerBound: {lb}; result: {ConstructSmallestNumberFromGivenDigits(digits, lb)}");

            digits = new[] { 8, 5, 3, 4 };
            lb = 5821;
            Console.WriteLine($"digits: [{string.Join(',', digits)}], lowerBound: {lb}; result: {ConstructSmallestNumberFromGivenDigits(digits, lb)}");
        }

        public int ConstructSmallestNumberFromGivenDigits(int[] digits, int lowerBound)
        {
            ++lowerBound;
            var lbArr = GetLbArray();
            if (digits.Length > lbArr.Length) return -1;
            var map = digits.Aggregate(new int[10], (arr, digit) =>
            {
                ++arr[digit];
                return arr;
            });

            var result = new int[digits.Length];
            var i = 0;
            for (; i < lbArr.Length; ++i)
            {
                if (map[lbArr[i]] > 0)
                {
                    result[i] = lbArr[i];
                    --map[lbArr[i]];
                }
                else break;
            }

            if (i == lbArr.Length) return ToNumber(result);

            var lastDigit = lbArr[i];
            for (var j = lastDigit; j <= 9; ++j)
            {
                if (map[j] <= 0) continue;

                result[i++] = j;
                --map[j];
                for (var k = 0; k <= 9;)
                {
                    if (map[k] == 0)
                    {
                        ++k;
                        continue;
                    }
                    result[i++] = k;
                    --map[k];
                }

                return ToNumber(result);
            }

            for (var k = lastDigit; k >= 0;)
            {
                if (map[k] == 0)
                {
                    --k;
                    continue;
                }
                result[i++] = k;
                --map[k];
            }

            return ToNumber(NextGreaterPermutation(result));

            int[] GetLbArray()
            {
                var str = lowerBound.ToString();
                var arr = new int[Math.Max(digits.Length, str.Length)];

                for (var (aidx, sidx) = (arr.Length - 1, str.Length - 1); aidx >= 0; --aidx, --sidx)
                {
                    arr[aidx] = sidx >= 0 ? str[sidx] - '0' : 0;
                }

                return arr;
            }

            int[] NextGreaterPermutation(int[] arr)
            {
                var idx1 = arr.Length - 2;
                for (; idx1 >= 0 && arr[idx1] >= arr[idx1 + 1]; --idx1)
                {
                }

                if (idx1 >= 0)
                {
                    for (var idx2 = arr.Length - 1; idx2 > idx1; --idx2)
                    {
                        if (arr[idx2] > arr[idx1])
                        {
                            (arr[idx1], arr[idx2]) = (arr[idx2], arr[idx1]);
                            break;
                        }
                    }
                }
                Array.Reverse(arr, idx1+1, arr.Length - idx1 - 1);
                return arr;
            }

            int ToNumber(int[] arr) => arr.Aggregate(0, (current, n) => 10 * current + n);
        }
    }
}
