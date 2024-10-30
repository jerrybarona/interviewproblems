using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.LeetCode
{
    public class P2468SplitMessageBasedOnLimit : ITestable
    {
        public void RunTest()
        {
            foreach (var (message, limit) in new (string, int)[]
            {
                ("this is really a very awesome message", 9)
            })
            {
                Console.WriteLine(string.Join(", ", SplitMessage(message, limit).Select(m => $"'{m}'")));
            }
        }

        public string[] SplitMessage(string message, int limit)
        {
            var messageLen = message.Length;
            var numParts = 0;
            var factor = 9;
            var factorLen = 1;
            var rem = 9;

            while (messageLen > 0)
            {
                var actualLimit = limit - (3 + 2 * factorLen);
                if (actualLimit <= 0) return new string[] { };

                var currentNumParts = messageLen / actualLimit;
                if (messageLen % actualLimit != 0) ++currentNumParts;

                if (currentNumParts > factor)
                {
                    numParts += factor;
                    messageLen += rem - (factor * actualLimit);
                    ++factorLen;
                    factor = 10 * factor;
                    rem = 10 * rem + 9;
                    continue;
                }

                numParts += currentNumParts;
                break;
            }

            var result = new string[numParts];
            for (var (i, textStart) = (0, 0); i < numParts; ++i)
            {
                var suffix = $"<{i + 1}/{numParts}>";
                var textLen = limit - suffix.Length;
                var text = textStart + textLen < message.Length
                    ? message[textStart..(textStart + textLen)]
                    : message[textStart..];
                result[i] = text + suffix;
                textStart += textLen;
            }

            return result;
        }
    }
}
