using System;
using System.Collections.Generic;
using System.Text;

namespace DieHard
{
    public class Container
    {
        private int _level;
        public int Level
        {
            get { return this._level; }
            set { this._level = value > this.Capacity ? this.Capacity : value; }
        }
        public int Capacity { get; }
        public bool IsFull
        {
            get
            {
                return this.Level == this.Capacity;
            }
        }
        public bool IsEmpty
        {
            get
            {
                return this.Level <= 0;
            }
        }
        public Container(int capacity)
        {
            this.Capacity = capacity;
        }

        public int Fill()
        {
            return this.Level = this.Capacity;
        }

        public int Empty()
        {
            return this.Level = 0;
        }

        public int Increase(int quantity)
        {
            int excess = this.Level + quantity - this.Capacity;
            excess = excess < 0 ? 0 : excess;

            this.Level += quantity;

            return excess;
        }

        public override string ToString()
        {
            return $"{this.Level}[{this.Capacity}]";
        }
    }
}
