using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
    public class BinaryHeap<T>
    {
        readonly Node<T> zero = null;
        private class Node<TT>
        {
            public TT Value { get; private set; }
            public int QueuePosition { get; set; }

            public Node(TT value, int queuePosition)
            {
                Value = value;
                QueuePosition = queuePosition;
            }
        }

        private readonly List<Node<T>> data;
        private readonly List<double> priorities;

        public BinaryHeap()
        {
            data = new List<Node<T>>();
            priorities = new List<double>();
            data.AddRange(Enumerable.Repeat(zero, 10));
            priorities.AddRange(Enumerable.Repeat(0d, 10));
            Count = 0;
        }

        public void Enqueue(T item, double priority)
        {
            int position = Count++;
            if (Count > data.Count - 1)
            {
                data.AddRange(Enumerable.Repeat(zero, data.Count));
                priorities.AddRange(Enumerable.Repeat(0d, data.Count));
            }
            data[position] = new Node<T>(item, position);
            priorities[position] = priority;
            MoveUp(position);

        }

        public T Dequeue() 
        {
            if (Count == 0)
            {
                throw new Exception("Getting value of empty queue");
            }
            T minNode = data[0].Value;
            Swap(0, Count - 1);
            Count--;
            MoveDown(0);
            return minNode;
        }

        private void MoveUp(int position)
        {
            while ((position > 0) && (priorities[Parent(position)] > priorities[position]))
            {
                int original_parent_pos = Parent(position);
                Swap(position, original_parent_pos);
                position = original_parent_pos;
            }
        }

        private void MoveDown(int position)
        {
            int lchild = LeftChild(position);
            int rchild = RightChild(position);
            int largest;
            if ((lchild < Count) && (priorities[lchild] < priorities[position]))
            {
                largest = lchild;
            }
            else
            {
                largest = position;
            }
            if ((rchild < Count) && (priorities[rchild] < priorities[largest]))
            {
                largest = rchild;
            }
            if (largest != position)
            {
                Swap(position, largest);
                MoveDown(largest);
            }
        }

        public int Count { get; private set; }

        private void Swap(int position1, int position2)
        {
            Node<T> temp = data[position1];
            data[position1] = data[position2];
            data[position2] = temp;
            data[position1].QueuePosition = position1;
            data[position2].QueuePosition = position2;
            double temp2 = priorities[position1];
            priorities[position1] = priorities[position2];
            priorities[position2] = temp2;
        }

        private static int Parent(int position)
        {
            return (position - 1) / 2;
        }

        private static int LeftChild(int position)
        {
            return 2 * position + 1;
        }

        private static int RightChild(int position)
        {
            return 2 * position + 2;
        }

    }
}
