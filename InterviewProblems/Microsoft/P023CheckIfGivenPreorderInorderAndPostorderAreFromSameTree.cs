using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Microsoft
{
    public class P023CheckIfGivenPreorderInorderAndPostorderAreFromSameTree
    {
        // https://leetcode.com/discuss/interview-experience/458771/Microsoft-or-Intern-or-Hyderabad-or-Sep-2019-Reject

        public void AreSameTreeTest()
        {
            var a = new[] { 1, 2, 4, 5, 3 };
            var b = new[] { 4, 2, 5, 1, 3 };
            var c = new[] { 4, 5, 2, 3, 1 };

            Console.WriteLine("preorder: " + string.Join(',', a));
            Console.WriteLine("inorder: " + string.Join(',', b));
            Console.WriteLine("postorder: " + string.Join(',', c));
            Console.WriteLine("Result:");
            Console.WriteLine(AreSameTree(a, b, c));

            a = new[] { 1, 5, 4, 2, 3 };
            b = new[] { 4, 2, 5, 1, 3 };
            c = new[] { 4, 1, 2, 3, 5 };

            Console.WriteLine("preorder: " + string.Join(',', a));
            Console.WriteLine("inorder: " + string.Join(',', b));
            Console.WriteLine("postorder: " + string.Join(',', c));
            Console.WriteLine("Result:");
            Console.WriteLine(AreSameTree(a, b, c));
        }

        public bool AreSameTree(int[] preorder, int[] inorder, int[] postorder)
        {
            var len = preorder.Length;
            return AreSame(0, len - 1, 0, len - 1, 0, len - 1);

            bool AreSame(int prestart, int preend, int instart, int inend, int poststart, int postend)
            {
                if (prestart == preend && preorder[prestart] == inorder[instart] && preorder[prestart] == postorder[poststart]) return true;
                if (preorder[prestart] == postorder[postend])
                {
                    var root = preorder[prestart];
                    var mid = -1;
                    for (var i = instart; i <= inend; ++i)
                    {
                        if (inorder[i] == root)
                        {
                            mid = i;
                            break;
                        }
                    }

                    var leftLen = mid - instart;

                    return AreSame(prestart + 1, prestart + leftLen, instart, mid - 1, poststart, poststart + leftLen - 1) &&
                        AreSame(prestart + leftLen + 1, preend, mid + 1, inend, poststart + leftLen, postend - 1);
                }
                return false;
            }
        }
    }
}
