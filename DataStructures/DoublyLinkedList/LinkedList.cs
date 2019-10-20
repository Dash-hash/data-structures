using System;

namespace DataStructures.DoublyLinkedList
{
    class LinkedList<T>
    {
        private Node<T> _first = null;
        private Node<T> _last;

        public LinkedList()
        {
            Length = 0;
        }

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
