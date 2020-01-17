using InterviewProblems.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace InterviewProblems.Concurrency
{
    public class BlockingHeapSimulation
    {
        private readonly Random random = new Random();
        private volatile bool _shutdown = false;

        public void RunWithMutex()
        {
            var heap = new BlockingHeap(1);
            var addThreads = Enumerable.Range(0, 10).Select(x => new Thread(() => heap.Add(random.Next(0, 500)))).ToArray();
            var popThreads = Enumerable.Range(0, 10).Select(x => new Thread(() => heap.Pop())).ToArray();

            for (var i = 0; i < 10; ++i)
            {
                addThreads[i].Start();
                popThreads[i].Start();
            }

            for (var i = 0; i < 10; ++i)
            {
                addThreads[i].Join();
                popThreads[i].Join();
            }
        }

        public void RunWithMonitor()
        {
            var heap = new MonitorBlockingHeap(2);
            var addThreads = Enumerable.Range(0, 10).Select(x => new Thread(() => addThreadStart())).ToArray();
            var popThreads = Enumerable.Range(0, 10).Select(x => new Thread(() => popThreadStart())).ToArray();

            Array.ForEach(addThreads, t => t.Start());
            Array.ForEach(popThreads, t => t.Start());            

            Thread.Sleep(5000);
            _shutdown = true;

            Array.ForEach(addThreads, t => t.Join());
            Array.ForEach(popThreads, t => t.Join());

            void addThreadStart()
            {
                while (!_shutdown) heap.Add(random.Next(0, 500));
            }

            void popThreadStart()
            {
                while (!_shutdown) heap.Pop();
            }
        }
    }

    public class MonitorBlockingHeap
    {
        private readonly Heap<Enode, int> _heap;
        private readonly int _size;
        private object _lock = new object();

        public MonitorBlockingHeap(int size)
        {
            _size = size;
            _heap = new Heap<Enode, int>(size, (a, b) => a < b);
        }

        public void Add(int val)
        {
            Console.WriteLine($"addThread {Thread.CurrentThread.ManagedThreadId} attempting to enter Monitor...");
            Monitor.Enter(_lock);
            Console.WriteLine($"addThread {Thread.CurrentThread.ManagedThreadId}: Waiting to Add {val}...");
            while (_heap.Count == _size) Monitor.Wait(_lock);

            _heap.Add(new Enode(val));
            Console.WriteLine($"addThread {Thread.CurrentThread.ManagedThreadId}: Added {val}.");
            Monitor.PulseAll(_lock);
            Thread.Sleep(200);
            Monitor.Exit(_lock);
        }

        public void Pop()
        {
            Console.WriteLine($"popThread {Thread.CurrentThread.ManagedThreadId} attempting to enter Monitor...");
            Monitor.Enter(_lock);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Waiting to Pop...");
            while (_heap.Count == 0) Monitor.Wait(_lock);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Popped {_heap.Pop().Val}.");
            Monitor.PulseAll(_lock);
            Thread.Sleep(200);
            Monitor.Exit(_lock);
        }
    }

    public class BlockingHeap
    {
        private Heap<Enode, int> _heap;
        private readonly int _size;
        private readonly Mutex _mutex = new Mutex();

        public BlockingHeap(int size)
        {
            _size = size;
            _heap = new Heap<Enode, int>(size, (a, b) => a < b);
        }

        public void Add(int val)
        {
            _mutex.WaitOne();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Waiting to Add {val}...");
            while (_heap.Count == _size)
            {
                _mutex.ReleaseMutex();
                _mutex.WaitOne();
            }

            _heap.Add(new Enode(val));
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Added {val}.");
            _mutex.ReleaseMutex();
        }

        public void Pop()
        {
            _mutex.WaitOne();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Waiting to Pop...");
            while (_heap.Count == 0)
            {
                _mutex.ReleaseMutex();
                _mutex.WaitOne();
            }

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Popped {_heap.Pop().Val}.");
            _mutex.ReleaseMutex();
        }
    }
    class Enode : IHeapNode<int>
    {
        public Enode(int val)
        {
            Val = val;
        }

        public int Val { get; }
    }
}
