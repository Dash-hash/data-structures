using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    class Dictionary<TKey, TValue>
    {
        #region Inner Types
        private class Entry
        {
            public Entry Next;
            public TValue Value { get; set; }
            public TKey Key { get; set; }

            public Entry(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }
        #endregion

        private Entry[] _entries;
        private int _capacity;

        public Dictionary(int capacity = 1024)
        {
            _entries = new Entry[capacity];
            _capacity = capacity;
        }

        public IEnumerable<TKey> Keys() =>
            Entries().Select(x => x.Key);

        public IEnumerable<TValue> Values() =>
            Entries().Select(x => x.Value);

        public TValue this[TKey key] 
        {
            get 
            {
                var hash = Hash(key);
                var entry = _entries[hash];

                while (entry != null)
                {
                    if (entry.Key.Equals(key))
                        return entry.Value;

                    entry = entry.Next;
                }

                throw new KeyNotFoundException();
            }
        }

        public void Add(TKey key, TValue value)
        {
            var hash = Hash(key);
            ref var entry = ref _entries[hash];

            while (entry != null)
            {
                if (entry.Key.Equals(key))
                {
                    entry.Value = value;
                    return;
                }

                entry = ref entry.Next;
            }

            entry = new Entry(key, value);
        }

        public void Remove(TKey key)
        {
            var hash = Hash(key);
            ref var entry = ref _entries[hash];

            while (entry != null)
            {
                if (entry.Key.Equals(key))
                {
                    entry = entry.Next;
                    return;
                }

                entry = ref entry.Next;
            }
        }

        private int Hash(TKey key)
        {
            var hash = key.GetHashCode() % _capacity;

            if (hash < 0)
                return hash + _capacity;
            else
                return hash;
        }

        private IEnumerable<Entry> Entries()
        {
            for (int i = 0; i < _capacity; i++)
            {
                var entry = _entries[i];

                while (entry != null)
                {
                    yield return entry;

                    entry = entry.Next;
                }
            }
        }
    }
}
