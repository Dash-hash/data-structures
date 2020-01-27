using System;
using System.Collections;

namespace DataStructures
{
    public class Tree<T> : IEnumerable where T : IComparable
    {
        #region Inner Types
        public class Node
        {
            public T Value { get; internal set; }

            public Node Parent;
            public Node Left;
            public Node Right;

            public Node(T value) =>
                Value = value;

            public void SetParent(Node parent) =>
                Parent = parent;

            public void SetChild(Node child)
            {
                if (child.Value.CompareTo(Value) >= 0)
                    Right = child;
                else
                    Left = child;
            }
        }
        #endregion

        private Node _root;

        public Node Root() =>
            _root;

        public void Add(T value)
        {
            if (_root == null)
                _root = new Node(value);
            else
                Add(ref _root, _root, value);
        }

        public void Remove(T value) =>
            Remove(ref _root, value);

        public void PreOrder(Action<T> action) =>
            PreOrder(_root, action);

        public void InOrder(Action<T> action) =>
            InOrder(_root, action);

        public void PostOrder(Action<T> action) =>
            PostOrder(_root, action);

        public int Height() =>
            Height(_root);

        private void Add(ref Node node, Node parent, T value)
        {
            if (node == null)
                node = new Node(value)
                {
                    Parent = parent
                };
            else
            {
                if (value.CompareTo(node.Value) < 0)
                    Add(ref node.Left, node, value);
                else
                    Add(ref node.Right, node, value);
            }
        }

        private void Remove(ref Node node, T value)
        {
            if (node == null)
                return;

            if (value.CompareTo(node.Value) < 0)
                Remove(ref node.Left, value);

            else if (value.CompareTo(node.Value) > 0)
                Remove(ref node.Right, value);

            else if (value.CompareTo(node.Value) == 0)
            {
                Node temp;

                if (node.Left != null && node.Right == null)
                {
                    node.Left.SetParent(node.Parent);
                    node.Parent.SetChild(node.Left);
                }
                else if (node.Left == null && node.Right != null)
                {
                    node.Right.SetParent(node.Parent);
                    node.Parent.SetChild(node.Right);
                }
                else if (node.Left == null && node.Right == null)
                {
                    if (node.Value.CompareTo(node.Parent.Value) < 0)
                        node.Parent.Left = null;
                    else
                        node.Parent.Right = null;
                }
                else if (node.Left != null && node.Right != null)
                {
                    if (node.Right.Left != null)
                    {
                        temp = Leftmost(node.Right);
                        node.Value = temp.Value;
                        Remove(ref node.Right, temp.Value);
                    }
                    else
                    {
                        node.Right.Left = node.Left;
                        node.Left.Parent = node.Right;

                        if (node == _root)
                            _root = node.Right;
                        else
                        {
                            node.Right.SetParent(node.Parent);
                            node.Parent.SetChild(node.Right);
                        }
                    }
                }
            }
        }

        public Node Find(Node node, T value)
        {
            if (value.CompareTo(node.Value) == 0)
                return node;

            if (value.CompareTo(node.Value) > 0)
                if (node.Right != null)
                    return Find(node.Right, value);

            if (value.CompareTo(node.Value) < 0)
                if (node.Left != null)
                    return Find(node.Left, value);

            return null;
        }

        public Node Leftmost(Node node)
        {
            if (node.Left != null)
                return Leftmost(node.Left);

            return node;
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public IEnumerator GetEnumerator()
        {
            var nodes = new System.Collections.Generic.Stack<Node>();
            var current = _root;

            while (nodes.Count != 0 || current != null)
            {
                while (current != null)
                {
                    nodes.Push(current);
                    current = current.Left;
                }

                current = nodes.Pop();

                yield return current.Value;

                current = current.Right;
            }
        }

        private void PreOrder(Node node, Action<T> action)
        {
            action(node.Value);

            if (node.Left != null)
                PreOrder(node.Left, action);

            if (node.Right != null)
                PreOrder(node.Right, action);
        }
        private void InOrder(Node node, Action<T> action)
        {
            if (node.Left != null)
                InOrder(node.Left, action);

            action(node.Value);

            if (node.Right != null)
                InOrder(node.Right, action);
        }
        private void PostOrder(Node node, Action<T> action)
        {
            if (node.Left != null)
                PostOrder(node.Left, action);

            if (node.Right != null)
                PostOrder(node.Right, action);

            action(node.Value);
        }

        private static int Height(Node node)
        {
            if (node == null)
                return 0;

            int leftHeight = 0;
            int rightHeight = 0;

            if (node.Left != null)
                leftHeight = Height(node.Left);
            if (node.Right != null)
                rightHeight = Height(node.Right);

            if (leftHeight > rightHeight)
                return leftHeight + 1;
            else
                return rightHeight + 1;
        }
    }
}
