using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.Netflix
{
    public class P001KeyValueDatabaseWithNestedTransactions : ITestable
    {
        // https://leetcode.com/discuss/interview-question/2704068/Netflix-or-Phone-screen

        public void RunTest()
        {
            Console.WriteLine("Test 1:");
            var db = new TransactionalKeyValueDatabase();
            Console.WriteLine(db.Begin());
            Console.WriteLine(db.Set("foo", "bar"));
            Console.WriteLine(db.Get("foo"));
            Console.WriteLine(db.Begin());
            Console.WriteLine(db.Get("foo"));
            Console.WriteLine(db.Set("foo", "baz"));
            Console.WriteLine(db.Get("foo"));
            Console.WriteLine(db.Rollback());
            Console.WriteLine(db.Get("foo"));
            Console.WriteLine(db.Commit());
            Console.WriteLine(db.Get("foo"));
        }

        internal class TransactionalKeyValueDatabase
        {
            private readonly Stack<(string,string,string)> _transactions = new();
            private readonly Dictionary<string, string> _store = new();

            public string Begin()
            {
                _transactions.Push(("begin", null, null));
                return "begin transaction";
            }

            public string Commit()
            {
                if (_transactions.Count == 0) { return "commit error"; }

                while ( _transactions.Count > 0 )
                {
                    var (command, _, _) = _transactions.Pop();
                    if (command == "begin") break;
                }

                return "commit transaction";
            }

            public string Rollback()
            {
                if (_transactions.Count == 0) { return "rollback error"; }

                while (_transactions.Count > 0)
                {
                    var (command, key, val) = _transactions.Pop();
                    if (command == "begin") break;

                    switch (command)
                    {
                        case "set":
                            _store[key] = val;
                            break;
                        case "delete":
                            _store.Remove(key);
                            break;
                        default:
                            break;
                    }
                }

                return "rollback transaction";
            }

            public string Get(string key)
            {
                if (!_store.ContainsKey(key)) return $"get error: '{key}'";

                return $"get: ['{key}'] -> '{_store[key]}'";
            }

            public string Set(string key, string value)
            {
                if (_transactions.Count > 0)
                {
                    if (_store.ContainsKey(key))
                    {
                        var currValue = _store[key];
                        _transactions.Push(("set", key, currValue));
                    }
                    else
                    {
                        _transactions.Push(("delete", key, null));
                    }
                }

                _store[key] = value;

                return $"set: ['{key}'] <- '{value}'";
            }

            public string Delete(string key)
            {
                if (_transactions.Count > 0)
                {
                    if (_store.ContainsKey(key))
                    {
                        var currValue = _store[key];
                        _transactions.Push(("set", key, currValue));
                    }
                    else
                    {
                        _transactions.Push(("delete", key, null));
                    }
                }

                if (!_store.ContainsKey(key)) return "delete no-op";
                _store.Remove(key);
                return $"delete: ['{key}']";
            }
        }
    }
}
