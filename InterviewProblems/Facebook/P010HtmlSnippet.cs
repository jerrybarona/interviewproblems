using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Facebook
{
    public class P010HtmlSnippet
    {
        // https://leetcode.com/discuss/interview-question/437701/Facebook-or-Phone-or-HTML-Snippet

        //private HashSet<string> _tags = new HashSet<string> { "div", "b", "p", "i" };

        //private enum ElementType
        //{
        //    OpeningTag,
        //    ClosingTag,
        //    SelfClosingTag,
        //    Character
        //};

        //public string TakeSnippet(string str, int k)
        //{
        //    var idx = 0;
        //    var frontStack = new Stack<Node>();
        //    var backStack = new Stack<Node>();
        //    var count = 0;

        //    void Parse()
        //    {
        //        if (str[idx] != '<')
        //        {
        //            frontStack.Push(new Node(ElementType.Character, $"{str[idx]}", 1));
        //            ++idx;
        //            return;
        //        }

        //        var i = idx;
        //        var text = new StringBuilder();
        //        var slashFound = false;
        //        var closingFound = false;
        //        var isSelfClosing = false;
        //        var isClosing = false;

        //        while (i < str.Length && str[i] != '>')
        //        {
        //            if (str[i] == '/')
        //            {
        //                if (slashFound)
        //                {
        //                    frontStack.Push(new Node(ElementType.Character, $"{str[idx]}", 1));
        //                    ++idx;
        //                    return;
        //                }

        //                slashFound = true;
        //                if (text.Length == 0) isClosing = true;
        //                else isSelfClosing = true;
        //            }
        //            else if 
        //        }

        //    }
        //}

        //class Node
        //{
        //    public ElementType ElementType { get; set; }
        //    public string Text { get; set; }
        //    public int Length { get; set; }

        //    public Node(ElementType elementType, string text, int length)
        //    {
        //        ElementType = elementType;
        //        Text = text;
        //        Length = length;
        //    }
        //}
    }
}
