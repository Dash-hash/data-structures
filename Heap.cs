using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class Heap<T> : IEnumerable<T> where T : IComparable<T>
    {
        private T[] _items;
        private int _capacity;

        public Heap(int capacity = 1024)
        {
            _items = new T[capacity];
            _capacity = capacity;
            Size = 0;
        }

        public int Size { get; private set; }

        public void Add(T item)
        {
            if (Size == _capacity)
                Expand(_items);

            _items[Size++] = item;
  
            HeapifyUp();
        }

        public T Pop()
        {
            if (Size == 0)
                throw new InvalidOperationException();

            var item = _items[0];

            _items[0] = _items[--Size];
            HeapifyDown();

            return item;
        }

        public T Peek()
        {
            if (Size == 0)
                throw new InvalidOperationException();

            return _items[0];
        }

        public IEnumerator<T> GetEnumerator()
        {
            var index = 0;

            while (index != Size)
            {
                yield return _items[index];
                index++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void HeapifyUp()
        {
            var index = Size - 1;

            while (HasParent(index) && Parent(index).CompareTo(_items[index]) < 0)
            {
                var parentIndex = GetParentIndex(index);

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }
        private void HeapifyDown()
        {
            var index = 0;

            while (HasLeftChild(index))
            {
                var indexMax = GetLeftChildIndex(index);

                if (HasRightChild(index) && RightChild(index).CompareTo(LeftChild(index)) > 0)
                    indexMax = GetRightChildIndex(index);

                if (_items[index].CompareTo(_items[indexMax]) < 0)
                {
                    Swap(index, indexMax);
                    index = indexMax;
                }
                else
                    break;
            }
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }
        private int GetLeftChildIndex(int index)
        {
            return index * 2 + 1;
        }
        private int GetRightChildIndex(int index)
        {
            return index * 2 + 2;
        }

        private bool HasParent(int index)
        {
            return GetParentIndex(index) >= 0;
        }
        private bool HasLeftChild(int index)
        {
            return GetLeftChildIndex(index) < Size;
        }
        private bool HasRightChild(int index)
        {
            return GetRightChildIndex(index) < Size;
        }

        private T Parent(int index)
        {
            return _items[GetParentIndex(index)];
        }
        private T LeftChild(int index)
        {
            return _items[GetLeftChildIndex(index)];
        }
        private T RightChild(int index)
        {
            return _items[GetRightChildIndex(index)];
        }

        private void Expand(T[] array)
        {
            Array.Copy(array, _items, _capacity * 2);

            _capacity *= 2;
        }

        private void Swap(int firstIndex, int secondIndex)
        {
            var temp = _items[firstIndex];

            _items[firstIndex] = _items[secondIndex];
            _items[secondIndex] = temp;
        }
    }
}
