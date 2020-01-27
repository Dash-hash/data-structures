using System;

namespace DataStructures
{
    public class LinkedList<T>
    {
        #region Inner Types
        public class Node
        {
            public T Value { get; }
            public Node Next { get; set; }

            public Node(T value) =>
                Value = value;
        }
        #endregion

        private Node _last;

        public Node First { get; private set; }
        public int Length { get; private set; }

        public T Last() => 
            _last.Value;

        public T this[int index] 
        {
            get 
            {
                var temp = First;

                for (int i = 0; i < index; i++)
                    temp = temp.Next;

                return temp.Value;
            }
        }

        public void Add(T value)
        {
            var node = new Node(value);

            if (First == null)
            {
                First = node;
                _last = First;
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
            var temp = First;

            for (int i = 0; i < Length; i++)
            {
                if (i == 1)
                    previous = First;

                if (temp.Value.Equals(value))
                {
                    if (previous == null)
                        First = temp.Next;
                    else
                    {
                        previous.Next = temp.Next;

                        if (temp.Equals(_last))
                            _last = previous;
                    }

                    Length--;
                    return;
                }

                temp = temp.Next;

                if (previous != null)
                    previous = previous.Next;
            }
        }

        public void RemoveAt(int index)
        {
            Node previous = null;
            var temp = First;

            if (0 > index || index >= Length)
                throw new IndexOutOfRangeException();

            for (int i = 0; i < index; i++)
            {
                if (i == 1)
                    previous = First;

                temp = temp.Next;

                if (previous != null)
                    previous = previous.Next;
            }

            if (previous == null)
            {
                First = temp.Next;
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
