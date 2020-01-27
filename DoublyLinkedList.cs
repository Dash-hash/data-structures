using System;

namespace DataStructures
{
    class DoublyLinkedList<T>
    {
        #region Inner Types
        private class Node
        {
            public T Value { get; }
            public Node Next { get; set; }
            public Node Previous { get; set; }

            public Node(T value) =>
                Value = value;
        }
        #endregion

        private Node _first;
        private Node _last;

        public int Length { get; private set; }

        public T this[int index] 
        {
            get 
            {
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
                node.Previous = _last;
                _last = node;
            }

            Length++;
        }

        public void Remove(T value)
        {
            var temp = _first;

            for (int i = 0; i < Length; i++)
            {
                if (temp.Value.Equals(value))
                {
                    if (temp.Previous == null)
                        _first = temp.Next;
                    else
                        temp.Previous.Next = temp.Next;

                    Length--;
                }

                temp = temp.Next;
            }
        }
        public void RemoveAt(int index)
        {
            var temp = _first;

            if (0 > index || index >= Length)
                throw new IndexOutOfRangeException();

            for (int i = 0; i < index; i++)
                temp = temp.Next;

            if (temp.Previous == null)
            {
                _first = temp.Next;
                Length--;
            }
            else
            {
                temp.Previous.Next = temp.Next;
                Length--;
            }
        }
    }
}
