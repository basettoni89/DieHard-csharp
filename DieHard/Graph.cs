using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DieHard
{
    public class Graph
    {
        private readonly List<Node> _nodes = new List<Node>();

        public void Generate(Sequence startSequence)
        {
            Queue<Node> remainingNodes = new Queue<Node>();
            remainingNodes.Enqueue(new Node(startSequence));

            while(remainingNodes.Count > 0)
            {
                Node sourceNode = remainingNodes.Dequeue();
                _nodes.Add(sourceNode);

                for(int i=0; i < sourceNode.Sequence.Containers.Length; i++)
                {
                    Node targetNode = new Node(sourceNode.Sequence.GetCopy());
                    int r = targetNode.Sequence.FillContainer(i);
                    if(r >= 0)
                    {
                        if(!sourceNode.Adjacents.Contains(targetNode))
                            sourceNode.Adjacents.Add(targetNode);

                        if(!remainingNodes.Contains(targetNode) && !_nodes.Where(n => n.Equals(targetNode)).Any())
                        {
                            remainingNodes.Enqueue(targetNode);
                        }
                    }

                    targetNode = new Node(sourceNode.Sequence.GetCopy());
                    r = targetNode.Sequence.EmptyContainer(i);
                    if (r >= 0)
                    {
                        sourceNode.Adjacents.Add(targetNode);

                        if (!remainingNodes.Contains(targetNode) && !_nodes.Where(n => n.Equals(targetNode)).Any())
                        {
                            remainingNodes.Enqueue(targetNode);
                        }
                    }

                    for(int j = 0; j < sourceNode.Sequence.Containers.Length; j++)
                    {
                        if (i == j) continue;

                        targetNode = new Node(sourceNode.Sequence.GetCopy());
                        r = targetNode.Sequence.MoveContent(i, j);
                        if (r >= 0)
                        {
                            sourceNode.Adjacents.Add(targetNode);

                            if (!remainingNodes.Contains(targetNode) && !Exist(targetNode))
                            {
                                remainingNodes.Enqueue(targetNode);
                            }
                        }
                    }
                }
            }
        }

        private bool Exist(Node target)
        {
            return _nodes.Where(n => n.Equals(target)).Any();
        }

        public bool Exist(Sequence target)
        {
            return _nodes.Where(n => n.Equals(target)).Any();
        }

        public bool Exist(int level)
        {
            bool found = false;

            foreach(Node node in _nodes)
            {
                found = node.Sequence.Contains(level);

                if (found) break;
            }

            return found;
        }

        public override string ToString()
        {
            return String.Join('\n', _nodes);
        }

        private class Node : IEquatable<Node>, IEquatable<Sequence>
        {
            public Sequence Sequence { get; }
            public List<Node> Adjacents { get; } = new List<Node>();

            public Node(Sequence sequence)
            {
                Sequence = sequence;
                Adjacents = new List<Node>();
            }

            public override string ToString()
            {
                string adjacents = string.Empty;

                int i = 0;
                foreach(Node adj in Adjacents)
                {
                    adjacents += adj.Sequence.ToString();

                    if (i < Adjacents.Count - 1)
                        adjacents += ", ";

                    i++;
                }

                return $"{Sequence.ToString()}\t{adjacents}";
            }

            public bool Equals(Node other)
            {
                if (other == null)
                    return false;
                return this.Sequence.Equals(other.Sequence);
            }

            public bool Equals(Sequence other)
            {
                if (other == null)
                    return false;
                return Enumerable.SequenceEqual(this.Sequence.Containers, other.Containers);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() == GetType())
                    return Equals(obj as Node);
                if (obj.GetType() == typeof(Sequence))
                    return Equals(obj as Sequence);
                return false;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = 11;
                    hashCode = (hashCode * 229) ^ Sequence.GetHashCode();

                    return hashCode;
                }
            }
        }
    }
}
