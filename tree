using System;

namespace Tree
{
    class Tree<T> where T : IComparable
    {
        private T _value;
        private Tree<T> _parent = null;
        private Tree<T> _left = null;
        private Tree<T> _right = null;

        public Tree()
        {
        }

        public void Add(T value)
        { 
            if (value.CompareTo(_value) > 0)
            {
                if (_right == null)
                {
                    _right = new Tree<T>
                    {
                        _parent = this,
                        _value = value
                    };
                    return;
                }

                _right.Add(value);
            }     
            else if (value.CompareTo(_value) < 0)
            {
                if (_left == null)
                {
                    _left = new Tree<T>
                    {
                        _parent = this,
                        _value = value
                    };
                    return;
                }

                _left.Add(value);
            }
            else 
                _value = value;
        }

        public void Remove(T value)
        {
            Tree<T> temp;

            if (value.CompareTo(_value) < 0 && _left != null)
            {
                _left.Remove(value);
                return;
            }

            if (value.CompareTo(_value) > 0 && _right != null)
            {
                _right.Remove(value);
                return;
            }

            if (value.CompareTo(_value) == 0)
            {
                if (_left == null && _right == null)
                {
                    if (value.CompareTo(_parent._value) > 0)
                        _parent._right = null;
                    if (value.CompareTo(_parent._value) < 0)
                        _parent._left = null;

                    return;
                }
                if (_left != null && _right != null)
                {
                    if (_right._left != null)
                    {
                        temp = _right.Leftmost();
                        _value = temp._value;
                        _right.Remove(_value);
                        return;
                    }
                    else
                    {
                        _value = _right._value;
                        _right.Remove(_value);
                        return;
                    }
                }
                if (_left != null && _right == null)
                {
                    if (value.CompareTo(_parent._value) > 0)
                        _parent._right = _left;
                    if (value.CompareTo(_parent._value) < 0)
                        _parent._left = _left;

                    return;
                }
                if (_left == null && _right != null)
                {
                    if (value.CompareTo(_parent._value) > 0)
                        _parent._right = _right;
                    if (value.CompareTo(_parent._value) < 0)
                        _parent._left = _right;

                    return;
                }
            }
        }

        public Tree<T> Find(T value)
        {
            if (value.CompareTo(_value) == 0)
                return this;

            if (value.CompareTo(_value) > 0)
                if(_right != null)
                    return _right.Find(value);

            if (value.CompareTo(_value) < 0)
                if(_left != null)
                    return _left.Find(value);

            return null;
        }
        public Tree<T> Leftmost()
        {
            if (_left != null)
                return _left.Leftmost();

            return this;
        }

        public void PreOrder(Action<T> function)
        {
            function(_value);

            if (_left != null)
                _left.PreOrder(function);
                
            if (_right != null)
                _right.PreOrder(function);
        }
        public void InOrder(Action<T> function)
        {
            if (_left != null)
                _left.InOrder(function);

            function(_value);

            if (_right != null)
                _right.InOrder(function);
        }
        public void PostOrder(Action<T> function)
        {
            if (_left != null)
                _left.PostOrder(function);
                
            if (_right != null)
                _right.PostOrder(function);

            function(_value);
        }
    }
}
