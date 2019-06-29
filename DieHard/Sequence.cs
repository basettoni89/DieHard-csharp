using System;
using System.Collections.Generic;
using System.Text;

namespace DieHard
{
    public class Sequence
    {
        public Container[] Containers { get; }

        public Sequence(params int[] capacities)
        {
            Containers = new Container[capacities.Length];

            for (int i = 0; i < capacities.Length; i++)
            {
                Container container = new Container(capacities[i]);
                Containers[i] = container;
            }
        }

        public int FillContainer(int v)
        {
            if (v > Containers.Length)
                return -2;

            int index = v - 1;
            if (Containers[index].IsFull)
                return -1;
            else
                return Containers[index].Fill();
        }

        public int EmptyContainer(int v)
        {
            if (v > Containers.Length)
                return -2;

            int index = v - 1;
            if (Containers[index].IsEmpty)
                return -1;
            else
                return Containers[index].Empty();
        }

        public int MoveContent(int fromIndex, int toIndex)
        {
            if (fromIndex > Containers.Length || toIndex > Containers.Length)
                return -2;

            Container from = Containers[fromIndex - 1];
            Container to = Containers[toIndex - 1];

            if (from.IsEmpty || to.IsFull)
                return -1;

            int originalFromLevel = from.Level;
            from.Level = to.Increase(from.Level);

            return originalFromLevel - from.Level;
        }

        public override string ToString()
        {
            return $"({String.Join<Container>(',', Containers)})";
        }
    }
}
