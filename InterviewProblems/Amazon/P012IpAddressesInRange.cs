using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Amazon
{
    public class P012IpAddressesInRange
    {
        // https://leetcode.com/discuss/interview-question/553305/Amazon-Phone-Screen

        public void IpAddressesTest()
        {
            var start = "1.1.255.255";
            var end = "1.2.0.1";
            Console.WriteLine(string.Join('\n', IpAddresses(start, end)) + "\n");

            start = "192.168.0.254";
            end = "192.168.1.2";
            Console.WriteLine(string.Join('\n', IpAddresses(start, end)) + "\n");

            start = "0.0.0.254";
            end = "0.0.1.2";
            Console.WriteLine(string.Join('\n', IpAddresses(start, end)) + "\n");
        }

        public IEnumerable<string> IpAddresses(string start, string end)
        {
            for (var (curr, e) = (ToUint(start), ToUint(end)); curr <= e; ++curr)
            {
                yield return ToStr(curr);
            }

            static uint ToUint(string str)
            {
                uint result = 0;
                for (var i = 0; i < str.Length; ++i)
                {
                    uint segment = 0;
                    for (; i < str.Length && str[i] != '.'; ++i)
                    {
                        segment = 10 * segment + str[i] - '0';
                    }
                    result = 256 * result + segment;
                }

                return result;
            }

            static string ToStr(uint x)
            {
                var stack = new Stack<uint>();
                while (stack.Count < 4)
                {
                    stack.Push(x & 0xff);
                    x >>= 8;
                }

                return string.Join('.', stack.Select(e => e.ToString()));
            }
        }

    }
}
