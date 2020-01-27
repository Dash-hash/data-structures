using System;

namespace DataStructures
{
    class Queue<T>
    {
        private class Node
        {
            public Node Next { get; set; }
            public T Value { get; }

            public Node(T value) =>
                Value = value;
        }

        private Node _first;
        private Node _last;

        public int Count { get; private set; }

        public void Enqueue(T value)
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
                _last = _last.Next;
            }

            Count++;
        }

        public T Dequeue()
        {
            if (_first == null)
                throw new InvalidOperationException();

            var first = _first.Value;
            _first = _first.Next;
            Count--;

            return first;
        }

        public T Peek()
        {
            if (_first == null)
                throw new InvalidOperationException();

            return _first.Value;
        }
    }
}
