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
            Queue<Sequence> remainingSequences = new Queue<Sequence>();
            remainingSequences.Enqueue(startSequence);

            while(remainingSequences.Count > 0)
            {
                Sequence source = remainingSequences.Dequeue();
                _nodes.Add(new Node(source));
                for(int i=0; i < source.Containers.Length; i++)
                {
                    Sequence target = new Sequence(source);
                    int r = target.FillContainer(i);
                    if(r >= 0 && !remainingSequences.Contains(target) && !_nodes.Where(n => n.Equals(target)).Any())
                    {
                        remainingSequences.Enqueue(target);
                    }

                    target = new Sequence(source);
                    r = target.EmptyContainer(i);
                    if (r >= 0 && !remainingSequences.Contains(target) && !_nodes.Where(n => n.Equals(target)).Any())
                    {
                        remainingSequences.Enqueue(target);
                    }

                    for(int j = 0; j < source.Containers.Length; j++)
                    {
                        if (i == j) continue;

                        target = new Sequence(source);
                        r = target.MoveContent(i, j);
                        if (r >= 0 && !remainingSequences.Contains(target) && !_nodes.Where(n => n.Equals(target)).Any())
                        {
                            remainingSequences.Enqueue(target);
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return String.Join('\n', _nodes);
        }

        private class Node : IEquatable<Node>, IEquatable<Sequence>
        {
            public Sequence Sequence { get; }
            public List<Node> Adjacents { get; }

            public Node(Sequence sequence)
            {
                Sequence = sequence;
                Adjacents = new List<Node>();
            }

            public override string ToString()
            {
                return Sequence.ToString();
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
