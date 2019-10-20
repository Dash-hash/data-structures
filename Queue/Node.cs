namespace DataStructures.Queue
{
    class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Value { get; }

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }
}
