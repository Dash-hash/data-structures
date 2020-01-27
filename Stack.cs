using System;

namespace DataStructures
{
    class Stack<T>
    {
        private class Node
        {
            public T Value { get; }
            public Node Next { get; set; }
            public Node Previous { get; set; }

            public Node(T value) =>
                Value = value;
        }

        private Node _first;
        private Node _last;

        public int Count { get; private set; }

        public void Push(T value)
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
                node.Previous = _last;
                _last = node;
            }

            Count++;
        }

        public T Pop()
        {
            if (_last == null)
                throw new InvalidOperationException();

            var last = _last.Value;

            _last.Previous = _last;

            Count--;

            return last;
        }

        public T Peek()
        {
            if (_last == null)
                throw new InvalidOperationException();

            return _last.Value;
        }
    }
}
