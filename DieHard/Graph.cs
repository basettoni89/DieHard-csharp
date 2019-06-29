using System;
using System.Collections.Generic;
using System.Text;

namespace DieHard
{
    public class Graph
    {
        private readonly List<Node> _nodes = new List<Node>();


        public void Generate(Sequence startSequence)
        {
        }

        private class Node
        {
            public Container[] Sequence { get; }
            public List<Node> Adjacents { get; }

            public Node(Container[] sequence)
            {
                Sequence = sequence;
                Adjacents = new List<Node>();
            }
        }
    }
}
