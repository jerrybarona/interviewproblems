using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.InterviewIo;

public class SpendMoneyOnGiftCard : ITestable
{
    /*
Goal: Write an algorithm to spend all money on a gift card

Constraints/Hints:
- must use 100% of the gift card value
- any item can be purchased an unlimited number of times

Inputs:
- $0 < all money values <= $1,000
- 1 <= menu size <= 100
- menu names/prices are unique to each other
 
Part One:

- process a single gift card amount at a time
- returns an array/list of strings
  - if successful: lists all items that spent 100% of the gift card
  - if not successful: list is empty if you cannot spend 100%
- do not over-optimize, stop and return the first answer you find

Example:
    menu:
    full dinner: $15
    sandwich:    $ 4
    milkshake    $ 3

purchase_food(10.00, ...)
    ... would return ['sandwich', 'milkshake', 'milkshake']

purchase_food(5.00, ...)
    ... would return []

primary_menu = {
    'sandwich': 6.85,
    'toast': 2.20,
    'curry': 7.85,
    'egg': 3.20,
    'cheese': 1.25,
    'coffee': 1.40,
    'soup': 3.45,
    'soda': 2.05,
}

gift_card_amounts = [5.00, 14.00, 19.00, 25.00, 33.00, 45.00, 49.00, 99.00, 114.00, 199.00]


Part Two:

- refactor code from part one to return shortest list (by total count of items)

Smallest quantities / answers: (your output does not need to look like this)

    $5, 3 items: {'coffee': 2, 'toast': 1}
    $14, 4 items: {'curry': 1, 'soda': 3}
    $19, 4 items: {'cheese': 1, 'curry': 2, 'soda': 1}
    $25, 4 items: {'curry': 1, 'sandwich': 2, 'soup': 1}
    $33, 6 items: {'coffee': 1, 'curry': 2, 'sandwich': 2, 'toast': 1}
    $45, 8 items: {'cheese': 1, 'curry': 4, 'sandwich': 1, 'soda': 1, 'soup': 1}
    $49, 8 items: {'curry': 1, 'sandwich': 5, 'soup': 2}
    $99, 15 items: {'coffee': 1, 'curry': 11, 'sandwich': 1, 'toast': 2}
    $114, 16 items: {'curry': 14, 'soda': 2}
    $199, 28 items: {'cheese': 3, 'curry': 24, 'sandwich': 1}
     
     */

    public void RunTest()
    {
        // Part 1
        var menu1 = new Dictionary<string, double>
        {
            { "full dinner", 15 },
            { "sandwich", 4},
            { "milkshake", 3 }
        };

        Console.WriteLine("MENU - PART 1");
        foreach (var (item, price) in menu1)
        {
            Console.WriteLine($"{item}:\t${price}");
        }
        Console.WriteLine("\n");

        foreach (var amount in new double[] { 10, 5 })
        {
            Console.WriteLine($"${amount}: [{string.Join(", ", GetSpentItems1(amount, menu1).Select(i => $"'{i}'"))}]");
        }
        Console.WriteLine("\n");
        // Part 2
        var menu2 = new Dictionary<string, decimal>
        {
            { "sandwich", 6.85m },
            { "toast", 2.20m },
            { "curry", 7.85m },
            { "egg", 3.20m },
            { "cheese", 1.25m },
            { "coffee", 1.40m },
            { "soup", 3.45m },
            { "soda", 2.05m }
        };

        var giftCardAmounts = new decimal[] { 35.50m, 5.00m, 14.00m, 19.00m, 25.00m, 33.00m, 45.00m, 49.00m, 99.00m, 114.00m, 199.00m };

        Console.WriteLine("MENU - PART 2");
        foreach (var (item, price) in menu2)
        {
            Console.WriteLine($"{item}:\t${price}");
        }
        Console.WriteLine("\n");

        foreach (var (amount, quantity, items) in GetSpentItems2(giftCardAmounts, menu2))
        {
            Console.WriteLine($"${amount},\t{quantity} item(s):\t{GetItemQuantities(items)}");
        }

        string GetItemQuantities(IDictionary<string, int> items)
        {
            return $"[{string.Join(", ", items.Select(i => $"'{i.Key}': {i.Value}"))}]";
        }
    }

    public IEnumerable<(decimal, int, IDictionary<string, int>)> GetSpentItems2(IEnumerable<decimal> amounts, IDictionary<string, decimal> menu)
    {
        var memo = new Dictionary<decimal, (Dictionary<string,int>, int)>
        {
            { 0m, (new Dictionary<string,int>(), 0) }
        };

        foreach (var amount in amounts)
        {
            GetList(amount);
            var (items, quantity) = memo[amount];
            
            yield return (amount, quantity, items);
        }

        int GetList(decimal amount)
        {
            if (amount < 0m) return int.MaxValue;
            if (amount == 0m) return 0;
            if (memo.ContainsKey(amount))
            {
                var (_, q) = memo[amount];
                return q;
            }

            Dictionary<string, int> spentItems = null;
            int quantity = int.MaxValue;
            string nextMenuItem = null;

            foreach (var (menuItem, price) in menu.AsEnumerable())
            {
                if (amount - price >= 0m)
                {
                    var candQuantity = GetList(amount - price);
                    if (candQuantity < quantity)
                    {
                        nextMenuItem = menuItem;
                        quantity = candQuantity;
                        var (i, _) = memo[amount - price];
                        spentItems = i;
                    }
                }
            }

            if (spentItems != null && quantity < int.MaxValue && !string.IsNullOrEmpty(nextMenuItem))
            {
                spentItems = new Dictionary<string, int>(spentItems);
                if (spentItems.ContainsKey(nextMenuItem)) spentItems[nextMenuItem]++;
                else spentItems.Add(nextMenuItem, 1);

                ++quantity;
            }

            memo.Add(amount, (spentItems, quantity));

            return quantity;
        }
    }

    public IEnumerable<string> GetSpentItems1(double amount, IDictionary<string,double> menu)
    {
        var result = new List<string>();
        GetList(amount);

        return result;

        bool GetList(double left)
        {
            if (left == 0) return true;

            foreach (var (item,price) in menu)
            {
                if (left - price >= 0)
                {
                    result.Add(item);
                    if (GetList(left - price))
                    {
                        return true;
                    }

                    result.RemoveAt(result.Count - 1);
                }
            }

            return false;
        }
    }
}
