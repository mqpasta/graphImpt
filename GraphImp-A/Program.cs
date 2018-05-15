using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphImp_A
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestGraph();

            BSTTest();
        }

        private static void TestGraph()
        {
            Graph g = new Graph(7);
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(1, 3);
            g.AddEdge(3, 4);
            g.AddEdge(3, 5);
            g.AddEdge(5, 4);

            Console.WriteLine("Adjacency Matrix of the graph");
            g.PrintMatrix();

            Console.WriteLine("Neighbours of vertex 0");
            int[] neg = g.GetNeighbours(2);

            foreach (int n in neg)
                Console.Write("{0} ", n);

            int source = 6;

            Console.WriteLine("Performing DFS on {0}", source);
            g.DFS(source);

            Console.WriteLine("Performing BFS on {0}", source);
            g.BFS(source);
        }

        static void BSTTest()
        {
            BSTree mytree = new BSTree();
            mytree.AddBinaryTree(10);
            mytree.AddBinaryTree(5);
            mytree.AddBinaryTree(12);
            mytree.AddBinaryTree(18);
            mytree.AddBinaryTree(45);
            mytree.AddBinaryTree(6);
            mytree.AddBinaryTree(9);

            mytree.Print();

            Console.WriteLine("Heigh to the tree is {0}",
                mytree.Height());

            Console.WriteLine("Height of an element");
            Console.WriteLine(mytree.Height(45));

        }
    }

    class BSTree
    {
        BNode root;

        public BSTree()
        {
            root = null;
        }

        public void Add(int e)
        {
            root = Add(e, root);
        }

        public void AddBinaryTree(int e)
        {
            root = AddBinaryTree(e, root);
        }

        private BNode Add(int e, BNode x)
        {
            if (x == null)
            {
                BNode newnode = new BNode(e);
                return newnode;
            }

            if (e < x.value)
                x.Left = Add(e, x.Left);
            else
                x.Right = Add(e, x.Right);

            return x;
        }

        private BNode AddBinaryTree(int e, BNode x)
        {
            if(x == null)
            {
                BNode newnode = new BNode(e);
                return newnode;
            }

            int lHeight = Height(x.Left);
            int rHeight = Height(x.Right);

            if (lHeight < rHeight)
                x.Left = Add(e, x.Left);
            else
                x.Right = Add(e, x.Right);

            return x;
        }

        public void Print()
        {
            InOrder(root);
        }

        private void InOrder(BNode x)
        {
            if (x == null)
                return;

            InOrder(x.Left);
            Console.WriteLine(x.value);
            InOrder(x.Right);
        }

        public int Height()
        {
            int height = 0;

            height = Height(root);

            return height;
        }

        public int Height(int element)
        {
            BNode node = Search(element, root);

            if (node != null)
                return Height(node);

            return -1;
        }
        private int Height(BNode x)
        {
            if (x == null)
                return -1;

            int lHeight = Height(x.Left);
            int rHeight = Height(x.Right);

            return Math.Max(lHeight, rHeight) + 1;
        }

        private BNode Search(int e, BNode x)
        {
            if (x == null)
                return null;

            if (e == x.value)
                return x;
            if (e < x.value)
                return Search(e, x.Left);
            else
                return Search(e, x.Right);
        }

    }

    class BNode
    {
        public int value;
        public BNode Left;
        public BNode Right;

        public BNode()
        {
            Left = null;
            Right = null;
        }

        public BNode(int val)
        {
            this.value = val;
            Left = null;
            Right = null;
        }
    }

    class Graph
    {
        // to stote the graph
        int[,] adjMatrix;
        int numberOfNodes;

        public Graph(int numOfNodes)
        {
            this.numberOfNodes = numOfNodes;
            adjMatrix = new int[numberOfNodes, numberOfNodes];
        }

        /// <summary>
        /// add a vertex in the graph
        /// </summary>
        public void AddVertex() { }

        /// <summary>
        /// Add an edge in the graph
        /// </summary>
        public void AddEdge(int source, int destination)
        {
            adjMatrix[source, destination] = 1;
            adjMatrix[destination, source] = 1;
        }

        /// <summary>
        /// return the neighbours of a node
        /// </summary>
        public int[] GetNeighbours(int vertex)
        {
            List<int> neg = new List<int>();

            for (int i = 0; i < numberOfNodes; i++)
            {
                if (adjMatrix[vertex, i] == 1)
                    neg.Add(i);
            }

            return neg.ToArray();
        }

        /// <summary>
        /// Print the adjacency matrix
        /// </summary>
        public void PrintMatrix()
        {
            for (int i = 0; i < numberOfNodes; i++)
            {
                for (int j = 0; j < numberOfNodes; j++)
                {
                    Console.Write("{0} ", adjMatrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void DFS(int source)
        {
            Stack<int> stack = new Stack<int>();
            List<int> visited = new List<int>();

            stack.Push(source);
            visited.Add(source);

            while (stack.Count > 0)
            {
                int thisvertex = stack.Pop();
                Console.WriteLine("Visited:{0}", thisvertex);

                int[] neg = GetNeighbours(thisvertex);

                foreach (int n in neg)
                {
                    if (!visited.Contains(n))
                    {
                        stack.Push(n);
                        visited.Add(n);
                    }
                }
            }

        }

        public void BFS(int source)
        {
            Queue<int> stack = new Queue<int>();
            List<int> visited = new List<int>();

            stack.Enqueue(source);
            visited.Add(source);

            while (stack.Count > 0)
            {
                int thisvertex = stack.Dequeue();
                Console.WriteLine("Visited:{0}", thisvertex);

                int[] neg = GetNeighbours(thisvertex);

                foreach (int n in neg)
                {
                    if (!visited.Contains(n))
                    {
                        stack.Enqueue(n);
                        visited.Add(n);
                    }
                }
            }

        }

    }
}
