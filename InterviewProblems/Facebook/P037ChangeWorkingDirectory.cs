using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Facebook
{
    internal class P037ChangeWorkingDirectory : ITestable
    {
        public void RunTest()
        {
            foreach (var (current, change) in new (string, string)[]
            {
                ("/", "/facebook"),
                ("/facebook/anin", "../abc/def"),
                ("/facebook/instagram", "../../../../.")
            })
            {
                Console.WriteLine(Process(current, change));
            }
        }

        private string Process(string current, string change)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(current);
            sb.Append("/");
            sb.Append(change);

            var path = sb.ToString();
            var list = path.Split("/");
            var stack = new Stack<string>();

            foreach (var item in list.Where(x => !string.IsNullOrEmpty(x)))
            {
                if (stack.Count > 0 && item == "..") stack.Pop();
                else if (item != ".") stack.Push(item);
            }

            sb.Clear();
            sb.Append('/');
            sb.Append(string.Join("/", stack.Reverse()));
            return sb.ToString();
        }
    }
}
