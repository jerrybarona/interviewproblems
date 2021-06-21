using System;
using System.Linq;

namespace InterviewProblems.Microsoft
{
	/*
	 ===== OneNote linked pages! ======
	OneNote has an ability to "link pages together" in where order doesn't matter.
	Our task for today is to find out how to do this effectively.
	We are going to number our pages from 0...n and they can be connected in any way.
	We don't care too much about order.

	Our crew is tasked with implementing the following interface
	create(n)
	link(a,b)
	isConnected(a,b)
	
	Example:
	create(8)
	+-+ +-+ +-+ +-+
	|0| |1| |2| |3|
	+-+ +-+ +-+ +-+
	+-+ +-+ +-+ +-+
	|4| |5| |6| |7|
	+-+ +-+ +-+ +-+
	link (0,1)
	link (2,1)
	+-+ +-+ +-+ +-+
	|0|-|1|-|2| |3|
	+-+ +-+ +-+ +-+
	+-+ +-+ +-+ +-+
	|4| |5| |6| |7|
	+-+ +-+ +-+ +-+
	isConnected(0,2) => True (Because 0 is connected to 1 and 1 is connected to 2)
	link (0,5)
	link (6,3)
	+-+ +-+ +-+ +-+
	|0|-|1|-|2| |3|
	+-+ +-+ +-+ +-+
	   \       / 
	+-+ +-+ +-+ +-+
	|4| |5| |6| |7|
	+-+ +-+ +-+ +-+
	isConnected(3,5) => False
	Can you determine whether two pages are linked together?
	 */

	public class P041OneNotePageLinking
	{
		public void OneNotePagesTest()
		{
			var oneNote = new OneNotePages(8);
			oneNote.Link(0, 1);
			oneNote.Link(2, 1);
			Console.WriteLine(oneNote.IsConnected(0, 2));
			oneNote.Link(0, 5);
			oneNote.Link(6, 3);
			Console.WriteLine(oneNote.IsConnected(3, 5));
		}
		
		// Enclosing class to encapsulate logic
		// This solution is based on the Union-Find algorithm
		// The Union-By-Rank and Path-Compression optimizations are also implemented

		/* INTUITION */
		// Essentially, each page is a node in a graph.
		// As pages are linked together, they form 'groups' of nodes.
		// Each page starts off as a group on its own.
		// When two or more nodes are linked together, a representative or 'parent' for that group is designated.
		// Therefore, two pages are connected if they have the same parent

		class OneNotePages
		{
			private readonly int[] _rank;
			private readonly int[] _parent;

			// The class contructor encapsulates the 'Create' functionality
			// It takes the number 'n' of pages to create
			public OneNotePages(int n)
			{
				_rank = new int[n];
				_parent = Enumerable.Range(0, n).ToArray();
			}

			public void Link(int a, int b) =>
				Union(a, b);

			public bool IsConnected(int a, int b) => 
				Parent(a) == Parent(b);

			// Find with Path Compresion optimization
			// i.e. the parent for each node is stored
			private int Parent(int node)
			{
				if (node == _parent[node]) return node;
				_parent[node] = Parent(_parent[node]);

				return _parent[node];
			}

			// Union with Rank optimization
			// i.e. the rank value is stored and used to
			// reduce the parent assignments
			private void Union(int x, int y)
			{
				var xparent = Parent(x);
				var yparent = Parent(y);

				if (xparent == yparent) return;

				if (_rank[xparent] >= _rank[yparent])
				{
					if (_rank[xparent] == _rank[yparent]) ++_rank[xparent];
					_parent[yparent] = xparent;
				}
				else _parent[xparent] = yparent;				
			}
		}
	}
}
