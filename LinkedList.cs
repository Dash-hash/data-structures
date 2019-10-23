using System;

namespace DataStructures
{
    class LinkedList<T>
    {
        private class Node
        {
            public T Value { get; }
            public Node Next { get; set; }

            public Node(T value)
            {
                Value = value;
            }
        }

        private Node _first = null;
        private Node _last;

        public LinkedList()
        {
            Length = 0;
        }

        public int Length { get; private set; }

        public T this[int index] {
            get {
                var temp = _first;

                for (int i = 0; i < index; i++)
                    temp = temp.Next;

                return temp.Value;
            }
        }

        public void Add(T value)
        {
            var node = new Node(value);

            if (_first == null)
            {
                _first = node;
                _last = _first;
            }
            else
            {
                _last.Next = node;
                _last = node;
            }

            Length++;
        }

        public void Remove(T value)
        {
            Node previous = null;
            var temp = _first;

            for (int i = 0; i < Length; i++)
            {
                if (i == 1)
                    previous = _first;

                if (temp.Value.Equals(value))
                {
                    if (previous == null)
                        _first = temp.Next;
                    else
                        previous.Next = temp.Next;

                    Length--;
                }

                temp = temp.Next;

                if (previous != null)
                    previous = previous.Next;
            }
        }
        public void RemoveAt(int index)
        {
            Node previous = null;
            var temp = _first;

            if (0 > index || index >= Length)
                throw new IndexOutOfRangeException();

            for (int i = 0; i < index; i++)
            {
                if (i == 1)
                    previous = _first;

                temp = temp.Next;

                if (previous != null)
                    previous = previous.Next;
            }

            if (previous == null)
            {
                _first = temp.Next;
                Length--;
            }
            else
            {
                previous.Next = temp.Next;
                Length--;
            }
        }
    }
}
