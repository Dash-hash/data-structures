using System;

namespace DataStructures.Queue
{
    class Queue<T>
    {
        private Node<T> _first = null;
        private Node<T> _last;

        public int Count { get; private set; }

        public Queue()
        {
            Count = 0;
        }

        public void Enqueue(T value)
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
                _last = node;
            }

            Count++;
        }

        public T Dequeue()
        {
            if (_first == null)
                throw new InvalidOperationException();

            var first = _first.Value;
            _first = _first.Next;
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
