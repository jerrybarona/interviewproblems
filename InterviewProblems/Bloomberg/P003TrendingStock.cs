using System;
using System.Collections.Generic;

namespace InterviewProblems.Bloomberg
{
    public class P003TrendingStock
    {
        // https://leetcode.com/discuss/interview-question/627139/

        public void TrendingStockTest()
        {
            Console.WriteLine("var obj = new TrendingStock()");
            var obj = new TrendingStock();

            Console.WriteLine("obj.ProcessStock(\"TSLA\")");
            obj.ProcessStock("TSLA");

            Console.WriteLine("obj.ProcessStock(\"APPL\")");
            obj.ProcessStock("APPL");

            Console.WriteLine("obj.ProcessStock(\"TSLA\")");
            obj.ProcessStock("TSLA");

            Console.WriteLine("obj.ProcessStock(\"APPL\")");
            obj.ProcessStock("APPL");

            Console.WriteLine("obj.ProcessStock(\"NTFX\")");
            obj.ProcessStock("NTFX");

            Console.WriteLine("obj.ProcessStock(\"TSLA\")");
            obj.ProcessStock("TSLA");

            Console.WriteLine($"obj.GetTrendingStock() -> {obj.GetTrendingStock()}");
            Console.WriteLine($"obj.GetTrendingStock() -> {obj.GetTrendingStock()}");
            Console.WriteLine($"obj.GetTrendingStock() -> {obj.GetTrendingStock()}");
            Console.WriteLine($"obj.GetTrendingStock() -> {obj.GetTrendingStock()}");
            Console.WriteLine($"obj.GetTrendingStock() -> {obj.GetTrendingStock()}");
            Console.WriteLine($"obj.GetTrendingStock() -> {obj.GetTrendingStock()}");
            Console.WriteLine($"obj.GetTrendingStock() -> {obj.GetTrendingStock()}");
            Console.WriteLine($"obj.GetTrendingStock() -> {obj.GetTrendingStock()}");
        }

        /// <summary>
        /// Similar principle as to LFU cache
        /// </summary>
        public class TrendingStock
        {
            private readonly Dictionary<string,LinkedListNode<TsNode>> _stocks = new Dictionary<string, LinkedListNode<TsNode>>();
            private readonly Dictionary<int,LinkedList<TsNode>> _freqs = new Dictionary<int, LinkedList<TsNode>> { { 0, new LinkedList<TsNode>()} };
            private int _maxFreq;

            public void ProcessStock(string name)
            {
                if (!_stocks.ContainsKey(name))
                {
                    _stocks.Add(name, new LinkedListNode<TsNode>(new TsNode(name)));
                    _freqs[0].AddLast(_stocks[name]);
                }

                var node = _stocks[name];
                var currFreq = node.Value.Freq;
                _freqs[currFreq].Remove(node);
                ++currFreq;
                ++node.Value.Freq;
                if (!_freqs.ContainsKey(currFreq)) _freqs.Add(currFreq, new LinkedList<TsNode>());
                _freqs[currFreq].AddLast(node);
                _maxFreq = Math.Max(_maxFreq, currFreq);
            }

            public string GetTrendingStock()
            {
                if (_maxFreq == 0) return string.Empty;
                var node = _freqs[_maxFreq].Last;
                var result = node?.Value.Name;
                _freqs[_maxFreq].RemoveLast();
                if (_freqs[_maxFreq].Count == 0) --_maxFreq;
                --node.Value.Freq;
                var currFreq = node.Value.Freq;
                _freqs[currFreq].AddLast(node);

                return result;
            }
        }

        public class TsNode
        {
            public string Name { get; set; }
            public int Freq { get; set; }

            public TsNode(string name)
            {
                Name = name;
            }
        }
    }
}
