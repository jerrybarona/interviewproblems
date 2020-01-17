using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0126WordLadderII
    {
        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            if (string.IsNullOrEmpty(beginWord) ||
                string.IsNullOrEmpty(endWord) ||
                string.Equals(beginWord, endWord) ||
                !wordList.Contains(endWord)) return new List<IList<string>>();

            var map = GetWordMap();
            var beginParents = new Dictionary<string, string>(wordList.Count) { { beginWord, null } };
            var endParents = new Dictionary<string, string>(wordList.Count) { { endWord, null } };
            var beginQueue = new Queue<string>(new[] { beginWord });
            var endQueue = new Queue<string>(new[] { endWord });

            var result = new List<IList<string>>();
            List<string> sequence;
            while (beginQueue.Count > 0 || endQueue.Count > 0)
            {
                var beginMatched = FindWords(beginQueue, beginParents, endParents);
                foreach (var matched in beginMatched)
                {
                    sequence = new List<string> { matched };
                    AddParents(beginParents[matched], beginParents, true);
                    AddParents(endParents[matched], endParents, false);
                    result.Add(sequence);
                }

                if (result.Any()) break;

                var endMatched = FindWords(endQueue, endParents, beginParents);
                foreach (var matched in endMatched)
                {
                    sequence = new List<string> { matched };
                    AddParents(beginParents[matched], beginParents, true);
                    AddParents(endParents[matched], endParents, false);
                    result.Add(sequence);
                }

                if (result.Any()) break;
            }

            return result;

            void AddParents(string word, Dictionary<string, string> parents, bool insert)
            {
                if (string.IsNullOrEmpty(word)) return;

                if (insert) sequence.Insert(0, word);
                else sequence.Add(word);

                AddParents(parents[word], parents, insert);
            }

            IEnumerable<string> FindWords(Queue<string> queue, Dictionary<string, string> parents, Dictionary<string, string> oppositeParents)
            {
                var ans = new List<string>();
                for (var count = queue.Count; count > 0; --count)
                {
                    var node = queue.Dequeue();
                    var children = GetSearchKeys(node).SelectMany(k => map[k]);
                    foreach (var child in children)
                    {
                        if (parents.ContainsKey(child)) continue;
                        parents.Add(child, node);
                        if (oppositeParents.ContainsKey(child)) ans.Add(child);
                        queue.Enqueue(child);
                    }
                }

                return ans;
            }

            Dictionary<string, List<string>> GetWordMap() =>
                wordList
                    .Concat(new[] { beginWord, endWord })
                    .Select(x => new { x, keys = GetSearchKeys(x) })
                    .SelectMany(y => y.keys.Select(z => (key: z, word: y.x)))
                    .Aggregate(new Dictionary<string, List<string>>(),
                              (dict, elem) =>
                              {
                                  if (!dict.ContainsKey(elem.key)) dict.Add(elem.key, new List<string>());
                                  dict[elem.key].Add(elem.word);
                                  return dict;
                              });

            IEnumerable<string> GetSearchKeys(string word) =>
                word.Select((c, idx) => (c, idx))
                    .Aggregate(Enumerable.Empty<string>(),
                               (res, x) => res.Append($"{word.Substring(0, x.idx)}_{word.Substring(x.idx + 1)}"));

        }
    }
}
