using System.Collections.Generic;
using System.Linq;

class Dictionary<TKey, TValue>
{
    private Entry<TKey, TValue>[] _entries;
    private int _capacity;

    public Dictionary(int capacity = 1024)
    {
        _entries = new Entry<TKey, TValue>[capacity];
        _capacity = capacity;
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

        entry = new Entry<TKey, TValue>(key, value);
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

    private int Hash(TKey key)
    {
        var hash = key.GetHashCode() % _capacity;

        if (hash < 0)
            return hash + _capacity;
        else
            return hash;
    }

    private IEnumerable<Entry<TKey, TValue>> Entries()
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

    public IEnumerable<TKey> Keys()
    {
        return Entries().Select(x => x.Key);
    }
    public IEnumerable<TValue> Values()
    {
        return Entries().Select(x => x.Value);
    }
}
