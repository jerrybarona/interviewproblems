using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewProblems.Cracking
{
    public class C15P03DiningPhilosophers
    {
        public void Run()
        {
            var chopsticks = new Chopstick[] { new Chopstick(0), new Chopstick(1), new Chopstick(2) };
            var philosophers = new Philosopher[]
            {
                new Philosopher(0, chopsticks[0], chopsticks[1]), new Philosopher(1, chopsticks[1], chopsticks[2]), new Philosopher(2, chopsticks[2], chopsticks[0])
            };

            var tasks = new Task[3];
            //tasks[0] = new Task(() => philosophers[0].Eat());
            //tasks[1] = new Task(() => philosophers[1].Eat());
            //tasks[2] = new Task(() => philosophers[2].Eat());
            Parallel.For(0, 3, i =>
            {
                tasks[i] = new Task(() => philosophers[i].Eat());
            });

            Console.WriteLine("hmmm");
            Thread.Sleep(2000);
            foreach (var task in tasks)
            {
                task.Start();
            }

            Task.WaitAll(tasks);
        }
    }

    public class Philosopher
    {
        public int Id { get; }
        public Chopstick Lower { get; }
        public Chopstick Higher { get; }

        public Philosopher(int id, Chopstick left, Chopstick right)
        {
            Id = id;
            Lower = left.Id < right.Id ? left : right;
            Higher = Lower == left ? right : left;
        }

        public void PickUpChopsticks()
        {
            Lower.PickUp();
            Console.WriteLine($"Philosopher {Id} just picked up chopstick {Lower.Id}");
            Higher.PickUp();
            Console.WriteLine($"Philosopher {Id} just picked up chopstick {Higher.Id}");
        }

        public void PutDownChopsticks()
        {
            Higher.PutDown();
            Console.WriteLine($"Philosopher {Id} just put down chopstick {Higher.Id}");
            Lower.PutDown();
            Console.WriteLine($"Philosopher {Id} just put down chopstick {Lower.Id}");
        }

        public void Eat()
        {
            PickUpChopsticks();
            Console.WriteLine($"Philosopher {Id} just had a bite");
            //Thread.Sleep(500);
            PutDownChopsticks();
        }
    }

    public class Chopstick
    {
        public readonly object _lock = new object();
        public int Id { get; }

        public Chopstick(int id)
        {
            Id = id;
        }

        public void PickUp()
        {
            System.Threading.Monitor.Enter(_lock);
        }

        public void PutDown()
        {
            Monitor.Exit(_lock);
        }
    }
}
