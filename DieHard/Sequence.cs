using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DieHard
{
    public class Sequence : IEquatable<Sequence>
    {
        public Container[] Containers { get; }

        public Sequence(int[] capacities)
        {
            Containers = new Container[capacities.Length];

            for (int i = 0; i < capacities.Length; i++)
            {
                Container container = new Container(capacities[i]);
                Containers[i] = container;
            }
        }

        public Sequence(Sequence other, int[] levels = null)
        {
            Containers = new Container[other.Containers.Length];

            for(int i = 0; i < other.Containers.Length; i++)
            {
                Container container = new Container(other.Containers[i]);

                if(levels != null && levels.Length > i)
                    container.Level = levels[i];

                Containers[i] = container;
            }
        }

        public int FillContainer(int index)
        {
            if (index > Containers.Length - 1)
                return -2;

            if (Containers[index].IsFull)
                return -1;
            else
                return Containers[index].Fill();
        }

        public int EmptyContainer(int index)
        {
            if (index > Containers.Length - 1)
                return -2;

            if (Containers[index].IsEmpty)
                return -1;
            else
                return Containers[index].Empty();
        }

        public int MoveContent(int fromIndex, int toIndex)
        {
            if (fromIndex > Containers.Length - 1 || toIndex > Containers.Length - 1)
                return -2;

            Container from = Containers[fromIndex];
            Container to = Containers[toIndex];

            if (from.IsEmpty || to.IsFull)
                return -1;

            int originalFromLevel = from.Level;
            from.Level = to.Increase(from.Level);

            return originalFromLevel - from.Level;
        }

        public bool Contains(int level)
        {
            bool found = false;

            foreach(Container container in Containers)
            {
                found = container.Level == level;

                if (found) break;
            }

            return found;
        }

        public Sequence GetCopy()
        {
            return new Sequence(this);
        }

        public override string ToString()
        {
            return $"({String.Join<Container>(',', Containers)})";
        }

        public bool Equals(Sequence other)
        {
            if (other == null)
                return false;
            return Enumerable.SequenceEqual(this.Containers, other.Containers);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Container);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 7;
                foreach(var container in Containers)
                    hashCode = (hashCode * 613) ^ container.GetHashCode();

                return hashCode;
            }
        }
    }
}
