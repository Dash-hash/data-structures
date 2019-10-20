using System;

namespace DataStructures.Stack
{
    class Stack<T>
    {
        private Node<T> _first = null;
        private Node<T> _last;

        public int Count { get; private set; }

        public Stack()
        {
            Count = 0;
        }

        public void Push(T value)
        {
            var node = new Node<T>(value);

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
