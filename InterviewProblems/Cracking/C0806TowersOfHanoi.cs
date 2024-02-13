using System;
using System.Collections.Generic;

namespace InterviewProblems.Cracking
{
    public class C0806TowersOfHanoi : ITestable
    {
        public void RunTest()
        {
            foreach (var n in new[] { 5 })
            {
                var h = new Hanoi(n);
                h.Move();
            }
        }

        internal class Hanoi
        {
            private readonly int _n;
            private readonly Stack<int> _a = new Stack<int>();
            private readonly Stack<int> _b = new Stack<int>();
            private readonly Stack<int> _c = new Stack<int>();

            public Hanoi(int n)
            {
                _n = n;
                for (var i = n; i > 0; --i) _a.Push(i);
                Console.WriteLine("Initial state:\n");
                PrintTowers();
            }

            public void Move()
            {
                var iteration = 0;
                MoveTower(_n, _a, _c, _b);

                void MoveTower(int numDisks, Stack<int> source, Stack<int> destination, Stack<int> buffer)
                {
                    if (numDisks == 0) return;
                    MoveTower(numDisks - 1, source, buffer, destination);
                    destination.Push(source.Pop());
                    Console.WriteLine($"\nIteration {++iteration}:");
                    PrintTowers();
                    MoveTower(numDisks - 1, buffer, destination, source);
                }
            }

            public void PrintTowers()
            {
                Console.WriteLine($"A: [{string.Join(", ", _a)}]\nB: [{string.Join(", ", _b)}]\nC: [{string.Join(", ", _c)}]");
            }
        }
    }
}
