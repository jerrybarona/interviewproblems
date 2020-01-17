using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0449SerializeAndDeserializeBst
    {
        public void Test(int num)
        {
            var byteArray = new List<byte>();
            var charArray = new List<char>();
            
            while (num > 0)
            {
                byteArray.Add((byte)(num & 255));
                charArray.Add((char)(num & 255));
                num >>= 8;
            }

            foreach (var b in byteArray) Console.Write($"{b}, ");
            Console.Write("\n");
            foreach (var c in charArray) Console.Write($"{c}, ");
            Console.Write("\n");
            foreach (var c in new string(charArray.ToArray())) Console.Write($"{c}, ");
            Console.Write("\n");
            foreach (var c in System.Text.Encoding.UTF8.GetString(byteArray.ToArray())) Console.Write($"{c}, ");
            Console.Write("\n");
            var a = (0xff << 8) | 0xff;
            Console.WriteLine(a);
            var x = new char[] { Convert.ToChar(a) };
            var sb = new StringBuilder();
            sb.Append(x);
            var y = new string(x);
            foreach (var z in y) Console.WriteLine((int)z);
            Console.Write("\n");
        }
    }
}
