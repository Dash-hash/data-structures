using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    class HashSet<T>
    {
        #region Inner Types
        private class Entry
        {
            public Entry Next;
            public T Value { get; set; }

            public Entry(T value) =>
                Value = value;
        }
        #endregion

        private Entry[] _entries;
        private int _capacity;

        public HashSet(int capacity = 1024)
        {
            _entries = new Entry[capacity];
            _capacity = capacity;
        }

        public int Count { get; private set; }

        public IEnumerable<T> Values()
        {
            for (int i = 0; i < _capacity; i++)
            {
                var entry = _entries[i];

                while (entry != null)
                {
                    yield return entry.Value;

                    entry = entry.Next;
                }
            }
        }

        public bool Add(T value)
        {
            var hash = Hash(value);
            ref var entry = ref _entries[hash];

            while (entry != null)
            {
                if (entry.Value.Equals(value))
                    return false;

                entry = ref entry.Next;
            }

            entry = new Entry(value);
            Count++;

            return true;
        }

        public bool Remove(T value)
        {
            var hash = Hash(value);
            ref var entry = ref _entries[hash];

            while (entry != null)
            {
                if (entry.Value.Equals(value))
                { 
                    entry = entry.Next;
                    Count--;
                    return true;
                }

                entry = ref entry.Next;
            }

            return false;
        }

        public bool Contains(T value)
        {
            var hash = Hash(value);
            ref var entry = ref _entries[hash];

            while (entry != null)
            {
                if (entry.Value.Equals(value))
                    return true;

                entry = ref entry.Next;
            }

            return false;
        }

        public void Union(HashSet<T> hashset)
        {
            if (hashset == null)
                throw new ArgumentNullException();

            foreach (var value in hashset.Values())
                Add(value);
        }

        public void Difference(HashSet<T> hashset)
        {
            if (hashset == null)
                throw new ArgumentNullException();

            foreach (var value in hashset.Values())
                Remove(value);
        }

        public void Intersection(HashSet<T> hashset)
        {
            if (hashset == null)
                throw new ArgumentNullException();

            foreach (var value in Values())
            {
                if (!hashset.Contains(value))
                    Remove(value);
            }
        }

        public bool Subset(HashSet<T> hashset)
        {
            if (hashset == null)
                throw new ArgumentNullException();

            return Values().All(x => hashset.Contains(x));
        }

        private int Hash(T key)
        {
            var hash = key.GetHashCode() % _capacity;

            if (hash < 0)
                return hash + _capacity;
            else
                return hash;
        }
    }
}
