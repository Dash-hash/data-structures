namespace DataStructures.Dictionary
{
    class Entry<TKey, TValue>
    {
        public Entry<TKey, TValue> Next;
        public TValue Value { get; set; }
        public TKey Key { get; set; }

        public Entry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Next = null;
        }
    }
}
